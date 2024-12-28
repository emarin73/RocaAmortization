namespace Terrasoft.Core.Process.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using Terrasoft.Common;
    using Terrasoft.Core;
    using Terrasoft.Core.Configuration;
    using Terrasoft.Core.DB;
    using Terrasoft.Core.Entities;
    using Terrasoft.Core.Process;

    #region Class: RocaProcessUserTask_GenerateSchedule

    /// <exclude/>
    public partial class RocaProcessUserTask_GenerateSchedule
    {
        #region Properties: Public

        #endregion

        #region Methods: Protected

        protected override bool InternalExecute(ProcessExecutingContext context)
        {
            // Get the parameter ID from the process parameters
           Guid parameterId = RocaParameterId;

            // Generate the amortization schedule using functional programming
            var amortizationSchedule = GenerateAmortizationSchedule(
                Roca1stDisbursementDate,
                RocaLoanAmount,
                RocaRate,
                RocaLoanType,
                RocaCompoundFrequency,
                RocaPaymentFrequency,
                RocaTermsInMonths,
                RocaFixedPrincipal,
                RocaInstallment,
                context.UserConnection,
                parameterId,
                RocaGracePeriodInMonths,
                RocaGracePeriodIntMethod,
                RocaGracePeriodCycle,
                RocaParentObject
            );

           if (amortizationSchedule.Count > 0)
          {
            RocaAmortizationTable = amortizationSchedule[0].PrimaryColumnValue;
          }

            return true;
        }

        #endregion

        #region Methods: Public

        public override bool CompleteExecuting(params object[] parameters)
        {
            return base.CompleteExecuting(parameters);
        }

        public override void CancelExecuting(params object[] parameters)
        {
            base.CancelExecuting(parameters);
        }

        public override string GetExecutionData()
        {
            return string.Empty;
        }

        public override ProcessElementNotification GetNotificationData()
        {
            return base.GetNotificationData();
        }

        #endregion

        #region Methods: Private

        private decimal CalculateDailyInterest(DateTime startDate, DateTime endDate, decimal balance, decimal annualRate)
        {
            int daysInPeriod = (endDate - startDate).Days + 1; // +1 to include both start and end dates
            decimal dailyRate = annualRate / 36500m; // 365 days and convert from percentage
            return balance * dailyRate * daysInPeriod;
        }

        private string GetGracePeriodIntMethodCode(Guid methodId)
        {
            var select = new Select(UserConnection)
                .Column("Name")
                .From("RocaGracePeriodIntMethod")
                .Where("Id").IsEqual(Column.Parameter(methodId)) as Select;
            
            return select.ExecuteScalar<string>();
        }

        private void GenerateInitialGracePeriod(
            EntitySchema schema,
            DateTime firstDisbursementDate,
            decimal loanAmount,
            decimal rate,
            Guid gracePeriodIntMethod,
            Guid parameterId,
            Guid parentObjectTypeId)
        {
            // Calculate interest for partial first month
            DateTime endOfMonth = GetEndOfMonth(firstDisbursementDate);
            decimal initialInterest = CalculateDailyInterest(firstDisbursementDate, endOfMonth, loanAmount, rate);

            string gracePeriodInterestMethod = GetGracePeriodInterestMethod(UserConnection, gracePeriodIntMethod);
          
            // Create grace period entry
            var gracePeriodEntry = schema.CreateEntity(UserConnection);
            gracePeriodEntry.SetDefColumnValues();
            gracePeriodEntry.PrimaryColumnValue = Guid.NewGuid();

            var parentObjectTypeName = GetParentObjectTypeName(UserConnection, parentObjectTypeId);

            // Set the parameterId in the appropriate column based on parentObjectType      
            switch (parentObjectTypeName)
            {
                case "Test Parameters":
                    gracePeriodEntry.SetColumnValue(RocaAmortParameterId, parameterId);
                    break;
                case "Loan":
                    gracePeriodEntry.SetColumnValue(RocaAmortizationLoanId, parameterId);
                    break;
                case "Application":
                    gracePeriodEntry.SetColumnValue(RocaAmortizationApplicationNumberId, parameterId);
                    break;
                case "Inquiry":
                    gracePeriodEntry.SetColumnValue(RocaLoanInquiryId, parameterId);
                    break;
            }

            gracePeriodEntry.SetColumnValue(RocaAmortizationNumber, "GPD0000");
            gracePeriodEntry.SetColumnValue(RocaPeriodNumber, 0);
            gracePeriodEntry.SetColumnValue(RocaAmortizationDate, endOfMonth);
            gracePeriodEntry.SetColumnValue(RocaAmortizationBeginningBalance, loanAmount);
            gracePeriodEntry.SetColumnValue(RocaAmortizationPrincipal, 0);
            if (gracePeriodInterestMethod == "1. Due On Cycle From Disbursement Date" || gracePeriodInterestMethod == "2. Accrual Of Interest For Balloon Payment")
            {
                gracePeriodEntry.SetColumnValue("RocaAmortizationInterest", initialInterest);
            }
            else
            {
                gracePeriodEntry.SetColumnValue(RocaAmortizationInterest, 0);
            }
            gracePeriodEntry.SetColumnValue(RocaAmortizationAmount, 0m);
            gracePeriodEntry.SetColumnValue(RocaAmortizationEndingBalance, loanAmount);
            
            gracePeriodEntry.Save();
        }

        private EntityCollection GenerateAmortizationSchedule(
            DateTime firstDisbursementDate,
            decimal loanAmount,
            decimal rate,
            Guid loanType,
            Guid compoundFrequency,
            Guid paymentFrequency,
            int termsInMonths,
            decimal? fixedPrincipal,
            decimal? installment,
            UserConnection userConnection,
            Guid parameterId,
            int gracePeriodMonths,
            Guid gracePeriodIntMethod,
            Guid gracePeriodCycle,
            Guid parentObjectTypeId)
        {
            // Make sure parameterId exists in the parent table
            if (parameterId == Guid.Empty)
            {
                throw new InvalidOperationException("Invalid Parameter ID");
            }
            // Make sure parentObjectTypeId exists in the parent table
            if (parentObjectTypeId == Guid.Empty)
            {
                throw new InvalidOperationException("Invalid Parent Object Type ID");
            }
            
            var schema = userConnection.EntitySchemaManager.GetInstanceByName("RocaLoanAmortizationTable");

            // Delete existing entries
            var delete = new Delete(userConnection)
                .From("RocaLoanAmortizationTable")
                .Where("RocaAmortParameterId").IsEqual(Column.Parameter(parameterId)) as Delete;
            delete.Execute();

            var amortizationTable = new EntityCollection(userConnection, schema);
            string gracePeriodMethod = GetGracePeriodIntMethodCode(gracePeriodIntMethod);
            
            // Handle grace period based on method
            if (gracePeriodMonths > 0)
            {
                switch (gracePeriodMethod)
                {
                    case "1. Due On Cycle From Disbursement Date":
                        // Generate initial grace period (GPD0000)
                        GenerateInitialGracePeriod(schema, firstDisbursementDate, loanAmount, rate, gracePeriodIntMethod, parameterId, parentObjectTypeId);
                        // Handle interest payments during grace period based on cycle
                        for (int gpIndex = 1; gpIndex <= gracePeriodMonths; gpIndex++)
                        {
                            DateTime cyclePaymentDate = GetEndOfMonth(firstDisbursementDate.AddMonths(gpIndex));
                            decimal interestPayment = loanAmount * (rate / 1200m); // Monthly rate
                            
                            var interestEntry = CreateScheduleEntry(
                                schema,             // schema
                                userConnection,     // userConnection
                                parameterId,        // parameterId
                                $"GP{gpIndex:00000}",     // amortizationNo
                                gpIndex,            // periodNumber
                                cyclePaymentDate,   // paymentDate
                                loanAmount,         // beginningBalance
                                0,                  // principal
                                interestPayment,    // interest
                                0,                  // amount
                                loanAmount,         // endingBalance
                                parentObjectTypeId);      // parent Object GUID
                            
                            amortizationTable.Add(interestEntry);
                        }
                        break;

                    case "2. Accrual Of Interest For Balloon Payment":
                        // Accumulate interest during grace period
                        int bpIndex = 1;
                        decimal accumulatedInterest = 0;
                        DateTime bpPaymentDate = firstDisbursementDate;

                        for (; bpIndex <= gracePeriodMonths; bpIndex++)
                        {
                            decimal monthlyInterest = loanAmount * (rate / 1200m);
                            accumulatedInterest += monthlyInterest;
                            bpPaymentDate = GetEndOfMonth(firstDisbursementDate.AddMonths(bpIndex));
                        }    
                        
                        DateTime endOfFirstMonth = GetEndOfMonth(firstDisbursementDate);
                        decimal initialInterest = CalculateDailyInterest(firstDisbursementDate, endOfFirstMonth, loanAmount, rate);    
                            
                            var ballonEntry = CreateScheduleEntry(
                                schema,             // schema
                                userConnection,     // userConnection
                                parameterId,        // parameterId
                                "GPBP001",          // amortizationNo
                                bpIndex,            // periodNumber
                                bpPaymentDate,      // paymentDate
                                loanAmount,         // beginningBalance
                                0,                  // principal
                                accumulatedInterest + initialInterest,    // interest
                                0,                  // amount
                                loanAmount,         // endingBalance
                                parentObjectTypeId);      // parent Object GUID
                            
                            amortizationTable.Add(ballonEntry);
                        break;

                    case "3. No Calculation Of Interest During Grace Period":
                        // Generate initial grace period (GPD0000)
                        GenerateInitialGracePeriod(schema, firstDisbursementDate, loanAmount, rate, gracePeriodIntMethod, parameterId, parentObjectTypeId);
                        // Handle interest payments during grace period based on cycle  
                        for (int noIntIndex = 1; noIntIndex <= gracePeriodMonths; noIntIndex++)
                        {
                            DateTime noIntPaymentDate = GetEndOfMonth(firstDisbursementDate.AddMonths(noIntIndex));
                                                    
                            var noInterestEntry = CreateScheduleEntry(
                                schema,             // schema
                                userConnection,      // userConnection
                                parameterId,        // parameterId
                                $"GP{(noIntIndex+1):00000}", // amortizationNo
                                noIntIndex,         // periodNumber
                                noIntPaymentDate,   // paymentDate
                                loanAmount,         // beginningBalance
                                0,                  // principal
                                0,                  // interest
                                0,                  // amount
                                loanAmount,         // endingBalance   
                                parentObjectTypeId);      // parent Object GUID
                            
                            amortizationTable.Add(noInterestEntry);
                        }
                        break;
                }
            }
            
            // Calculate payment frequency in months
            int paymentIntervalMonths = GetPaymentIntervalInMonths(paymentFrequency);
            int numberOfPayments = termsInMonths / paymentIntervalMonths;
            
            // Calculate effective interest rate per payment period
            decimal effectiveRate = CalculateEffectiveRate(rate, compoundFrequency, paymentFrequency);
            
            // Get loan type code
            string loanTypeCode = GetLoanTypeCode(userConnection, loanType);
            
            decimal paymentAmount = 0m;
            decimal principalPayment = 0m;
            decimal previousBalance = loanAmount;
            
            var scheduleInitialEntry = schema.CreateEntity(userConnection);
            scheduleInitialEntry.SetDefColumnValues();
             scheduleInitialEntry.PrimaryColumnValue = Guid.NewGuid();
          
            // Calculate initial payment based on loan type
            if (loanTypeCode == "1. Amortized")
            {
                    // PMT formula: PMT = P * (r * (1 + r)^n) / ((1 + r)^n - 1)
                    double ratePerPeriod = (double)effectiveRate;
                    double principal = (double)loanAmount;
                    double temp = Math.Pow(1 + ratePerPeriod, numberOfPayments);
                    paymentAmount = (decimal)(principal * (ratePerPeriod * temp) / (temp - 1));
                    installment = paymentAmount;
                    fixedPrincipal = 0;
                    
                    // 1. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 2. Principal is the fixed installment minus interestPayment
                    principalPayment = paymentAmount - interestPayment;

                    // 3. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;

                   DateTime paymentDate = GetEndOfMonth(firstDisbursementDate.AddMonths(gracePeriodMonths));
                    // Add first amortization entry
                    var scheduleInitialEntryAmortized   = CreateScheduleEntry(
                        schema,             // schema
                        userConnection,     // userConnection
                        parameterId,        // parameterId
                        "A00001",           // amortizationNo
                        gracePeriodMonths + 1, // periodNumber
                        paymentDate,        // paymentDate
                        previousBalance,    // beginningBalance
                        principalPayment,   // principal
                        interestPayment,    // interest
                        paymentAmount,      // amount
                        newBalance,         // endingBalance
                        parentObjectTypeId);      // parent Object GUID 
                    amortizationTable.Add(scheduleInitialEntryAmortized);
                    previousBalance = newBalance;   

            } 
            else if (loanTypeCode == "2. Fixed Principal")
            {
                // Fixed Principal Loan Calculation:
                // For Fixed Principal loans:
                    // 1. Principal is fixed
                    principalPayment = loanAmount /numberOfPayments;
                    // 2. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 3. Total payment (Amortization Amount) is the principal payment plus interestPayment = installment
                    decimal amortizationAmount = principalPayment + interestPayment;

                    // 4. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;        
                    
                    DateTime paymentDate = GetEndOfMonth(firstDisbursementDate.AddMonths(gracePeriodMonths));
                    // 5. Populate schedule entry
                    var scheduleInitialEntryFixedPrincipal = CreateScheduleEntry(
                        schema,             // schema
                        userConnection,     // userConnection
                        parameterId,        // parameterId
                        "A00001",           // amortizationNo
                        gracePeriodMonths + 1, // periodNumber
                        paymentDate,        // paymentDate
                        previousBalance,    // beginningBalance
                        principalPayment,   // principal
                        interestPayment,    // interest
                        amortizationAmount,      // amount
                        newBalance,         // endingBalance
                        parentObjectTypeId);      // parent Object GUID
                    
                    amortizationTable.Add(scheduleInitialEntryFixedPrincipal);
                    previousBalance = newBalance;   
                    installment = 0;
         
                // Log or debug the calculated principal amount
                var message = $"Fixed Principal Loan Calculation:\nLoan Amount: {loanAmount}\nPayments: {numberOfPayments}\nCalculated Principal: {principalPayment}";
            }
            else
            {
                throw new ArgumentException($"Unsupported loan type: {loanTypeCode}");
            }

            // Generate schedule entries. Continue with count starting from grace period
           int startingPeriod = gracePeriodMonths + 2;
           int n = 1;
            for (int m = startingPeriod; m < (numberOfPayments + gracePeriodMonths +1); m++)
            {
                // 5. Calculate payment date
                DateTime paymentDate = firstDisbursementDate.AddMonths(m * paymentIntervalMonths);
                paymentDate = GetEndOfMonth(paymentDate);
                
                if (loanTypeCode == "1. Amortized")
                {
                    // for amortized loans, 
                    // 1. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 2. Principal is the fixed installment minus interestPayment
                    principalPayment = paymentAmount - interestPayment;
                    
                    // 3. Total payment (Amortization Amount) is the principal payment plus interestPayment = installment
                    decimal amortizationAmount = paymentAmount;

                    // 4. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;
                                        
                    // 6. Populate schedule entry
                    var scheduleEntryAmortized = CreateScheduleEntry(
                        schema,             // schema
                        userConnection,     // userConnection
                        parameterId,        // parameterId
                        "A" + (n + 1).ToString("00000"), // amortizationNo
                        m, // periodNumber
                        paymentDate, // paymentDate
                        previousBalance, // beginningBalance
                        principalPayment, // principal
                        interestPayment, // interest
                        paymentAmount, // amount
                        newBalance, // endingBalance
                        parentObjectTypeId); // parent Object GUID
                    n++;
                    amortizationTable.Add(scheduleEntryAmortized);
                    previousBalance = newBalance;   
                }
                else if (loanTypeCode == "2. Fixed Principal")
                {
                    // For Fixed Principal loans:
                    // 1. Principal is fixed
                    principalPayment = loanAmount /numberOfPayments;
                    // 2. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 3. Total payment (Amortization Amount) is the principal payment plus interestPayment = installment
                    decimal amortizationAmount = principalPayment + interestPayment;

                    // 4. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;        

                    // 5. Populate schedule entry
                    var scheduleEntryFixedPrincipal = CreateScheduleEntry(
                        schema,             // schema
                        userConnection,     // userConnection
                        parameterId,        // parameterId
                        "A" + (n + 1).ToString("00000"), // amortizationNo
                        m, // periodNumber
                        paymentDate, // paymentDate
                        previousBalance, // beginningBalance
                        principalPayment, // principal
                        interestPayment, // interest
                        amortizationAmount, // amount
                        newBalance, // endingBalance
                        parentObjectTypeId); // parent Object GUID
                    n++;
                    amortizationTable.Add(scheduleEntryFixedPrincipal);
                    previousBalance = newBalance;   
                }
            }

            return amortizationTable;
        }

        private DateTime GetEndOfMonth(DateTime date)

        {
           return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        private int GetPaymentIntervalInMonths(Guid paymentFrequency)
        {
            // Add lookup logic for payment frequency
            // Example: Monthly = 1, Quarterly = 3, Semi-annual = 6, Annual = 12
            return 1; // Default to monthly
        }

        private decimal CalculateEffectiveRate(decimal annualRate, Guid compoundFrequency, Guid paymentFrequency)
        {
            // Convert annual rate from percentage to decimal (e.g., 12% becomes 0.12)
            decimal rateAsDecimal = annualRate / 100m;

            // Get compound and payment frequencies from lookups
            int compoundPerYear = GetCompoundFrequencyPerYear(compoundFrequency);
            int paymentsPerYear = 12 / GetPaymentIntervalInMonths(paymentFrequency);

            // Step 1: Calculate Effective Annual Rate (EAR)
            // Formula: EAR = (1 + r/m)^m - 1
            // where r is nominal annual rate and m is number of compounding periods per year
            double nominalRate = (double)rateAsDecimal;
            double compoundingsPerYear = compoundPerYear;
            
            double effectiveAnnualRate = Math.Pow(1 + (nominalRate / compoundingsPerYear), compoundingsPerYear) - 1;

            // Step 2: Convert EAR to payment period rate
            // Formula: (1 + EAR)^(1/p) - 1
            // where p is number of payments per year
            double paymentPeriodRate = Math.Pow(1 + effectiveAnnualRate, 1.0 / paymentsPerYear) - 1;

            return (decimal)paymentPeriodRate;
        }

        private int GetCompoundFrequencyPerYear(Guid compoundFrequency)
        {
            var select = new Select(UserConnection)
                .Column("RocaCompFreqValue")
                .From("RocaCompoundFrequency")
                .Where("Id").IsEqual(Column.Parameter(compoundFrequency)) as Select;
            
            var frequencyValue = select.ExecuteScalar<int>();
            
            if (frequencyValue <= 0)
            {
                throw new ArgumentException($"Invalid compound frequency value: {frequencyValue}", nameof(compoundFrequency));
            }
            
            return frequencyValue;
        }

        private decimal GetPreviousBalance(Entity amortizationTable, int previousIndex)
        {
            // Implement logic to get the ending balance from the previous period
            // You might need to query the database or maintain a collection
            return 0m;
        }

        private string GetLoanTypeCode(UserConnection userConnection, Guid loanTypeId)
        {
            return (new Select(userConnection)
                .Column("Name")
                .From("RocaLoanType")
                .Where("Id").IsEqual(Column.Parameter(loanTypeId)) as Select).ExecuteScalar<string>();
        }
        // Helper method to get grace period interest method name from GUID
        private string GetGracePeriodInterestMethod(UserConnection userConnection, Guid gracePeriodIntMethod)
        {
            return (new Select(userConnection)
                .Column("Name")
                .From("RocaGracePeriodIntMethod")
                .Where("Id").IsEqual(Column.Parameter(gracePeriodIntMethod)) as Select).ExecuteScalar<string>();
        }

        // Helper method to get parent object type name from GUID
        private string GetParentObjectTypeName(UserConnection userConnection, Guid parentObjectTypeId)
        {
            return (new Select(userConnection)
                .Column("Name")
                .From("RocaAmortParentObject")
                .Where("Id").IsEqual(Column.Parameter(parentObjectTypeId)) as Select).ExecuteScalar<string>();
        }   
        private Entity CreateScheduleEntry(
        EntitySchema schema,
        UserConnection userConnection,
        Guid parameterId,
        string amortizationNo,
        int periodNumber,
        DateTime paymentDate,
        decimal beginningBalance,
        decimal principal,
        decimal interest,
        decimal amount,
        decimal endingBalance,
        Guid parentObjectTypeId)
        {
        var entry = schema.CreateEntity(userConnection);
        entry.SetDefColumnValues();
        entry.PrimaryColumnValue = Guid.NewGuid();

        string parentObjectTypeName = GetParentObjectTypeName(userConnection, parentObjectTypeId);

         // Set the parameterId in the appropriate column based on parentObjectType
        switch (parentObjectTypeName)
        {
            case "Test Parameters":
                entry.SetColumnValue(RocaAmortParameterId, parameterId);
                // entry.SetColumnValue("RocaAmortizationLoan", null);
                // entry.SetColumnValue("RocaAmortizationApplicationNumber", null);
                // entry.SetColumnValue("RocaLoanInquiry", null);
                break;

            case "Loan":
                entry.SetColumnValue(RocaAmortizationLoanId, parameterId);
                // entry.SetColumnValue("RocaAmortParameter", null);
                // entry.SetColumnValue("RocaAmortizationApplicationNumber", null);
                // entry.SetColumnValue("RocaLoanInquiry", null);
                break;

            case "Application":
                entry.SetColumnValue(RocaAmortizationApplicationNumberId, parameterId);
                //  entry.SetColumnValue("RocaAmortParameter", null);
                // entry.SetColumnValue("RocaAmortizationLoan", null);
                // entry.SetColumnValue("RocaLoanInquiry", null);
                break;

            case "Inquiry":
                entry.SetColumnValue(RocaLoanInquiryId, parameterId);
                // entry.SetColumnValue("RocaAmortParameter", null);
                // entry.SetColumnValue("RocaAmortizationLoan", null);
                // entry.SetColumnValue("RocaAmortizationApplicationNumber", null);
                break;

            default:
                throw new ArgumentException($"Debug - Parent Object Type received: '{parentObjectTypeName}', ID: {parentObjectTypeId}");
        }
      
        entry.SetColumnValue(RocaAmortizationNumber, amortizationNo);
        entry.SetColumnValue(RocaPeriodNumber, periodNumber);
        entry.SetColumnValue(RocaAmortizationDate, paymentDate);
        entry.SetColumnValue(RocaAmortizationBeginningBalance, beginningBalance);
        entry.SetColumnValue(RocaAmortizationPrincipal, principal);
        entry.SetColumnValue(RocaAmortizationInterest, interest);
        entry.SetColumnValue(RocaAmortizationAmount, amount);
        entry.SetColumnValue(RocaAmortizationEndingBalance, endingBalance);
        
        entry.Save();
        
        return entry;
        }         

        #endregion
    }

    #endregion
}