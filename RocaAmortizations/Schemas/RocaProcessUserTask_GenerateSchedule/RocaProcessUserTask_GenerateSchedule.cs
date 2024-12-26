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
                parameterId
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
            Guid parameterId
        )
        {
            var schema = userConnection.EntitySchemaManager.GetInstanceByName("RocaLoanAmortizationTable");

            // Delete existing entries for this parameter
            var delete = new Delete(userConnection)
                .From("RocaLoanAmortizationTable")
                .Where("RocaAmortParameterId").IsEqual(Column.Parameter(parameterId)) as Delete;
            
            delete.Execute();
            // Continue with generating new schedule
            var amortizationTable = new EntityCollection(userConnection, schema);


            // Calculate payment frequency in months
            int paymentIntervalMonths = GetPaymentIntervalInMonths(paymentFrequency);
            int numberOfPayments = termsInMonths / paymentIntervalMonths;
            
            // Calculate effective interest rate per payment period
            decimal effectiveRate = CalculateEffectiveRate(rate, compoundFrequency, paymentFrequency);
            
            // Get loan type code
            string loanTypeCode = GetLoanTypeCode(loanType);
            
            decimal paymentAmount = 0m;
            decimal principalPayment = 0m;
            decimal previousBalance = loanAmount;
            
            // Calculate initial payment based on loan type
            if (loanTypeCode == "1. Amortized")
            {
                // For Amortized loans, we need the installment amount

                    // PMT formula: PMT = P * (r * (1 + r)^n) / ((1 + r)^n - 1)
                    double ratePerPeriod = (double)effectiveRate;
                    double principal = (double)loanAmount;
                    
                    double temp = Math.Pow(1 + ratePerPeriod, numberOfPayments);
                    paymentAmount = (decimal)(principal * (ratePerPeriod * temp) / (temp - 1));
                    installment = paymentAmount;
                    fixedPrincipal = 0m;
           }     
             
            else if (loanTypeCode == "2. Fixed Principal")
            {
                // For Fixed Principal loans, we need the principal payment amount
                    fixedPrincipal = loanAmount / numberOfPayments;
                    installment = 0m;
                
                // Log or debug the calculated principal amount
                var message = $"Fixed Principal Loan Calculation:\nLoan Amount: {loanAmount}\nPayments: {numberOfPayments}\nCalculated Principal: {principalPayment}";

            }
            else
            {
                throw new ArgumentException($"Unsupported loan type: {loanTypeCode}");
            }

            // Generate schedule entries
            for (int i = 0; i < numberOfPayments; i++)
            {
                var scheduleEntry = schema.CreateEntity(userConnection);
                
                // Set required fields
                scheduleEntry.SetDefColumnValues();
                scheduleEntry.PrimaryColumnValue = Guid.NewGuid();
                scheduleEntry.SetColumnValue("RocaAmortizationNumber", "A" + (i + 1).ToString("00000"));
                
                // 5. Calculate payment date
                DateTime paymentDate = firstDisbursementDate.AddMonths(i * paymentIntervalMonths);
                paymentDate = GetEndOfMonth(paymentDate);
                
                if (loanTypeCode == "1. Amortized")
                {
                    // for amortized loans, 
                    // 1. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 2. Principal is the fixed installment minus interestPayment
                    principalPayment = paymentAmount - interestPayment;
                    
                    // 3. Total payment (Amortization Amount) is the principal payment plus interestPayment = installment
                    decimal amortizationAmount = (decimal)installment;

                    // 4. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;
                    
                    // 6. Populate schedule entry
                    scheduleEntry.SetColumnValue("RocaAmortParameterId", parameterId);
                    scheduleEntry.SetColumnValue("RocaAmortizationDate", paymentDate);
                    scheduleEntry.SetColumnValue("RocaPeriodNumber", i + 1);
                    scheduleEntry.SetColumnValue("RocaAmortizationBeginningBalance", previousBalance);
                    scheduleEntry.SetColumnValue("RocaAmortizationAmount", amortizationAmount);
                    scheduleEntry.SetColumnValue("RocaAmortizationPrincipal", principalPayment);
                    scheduleEntry.SetColumnValue("RocaAmortizationInterest", interestPayment);
                    scheduleEntry.SetColumnValue("RocaAmortizationEndingBalance", newBalance);

                    scheduleEntry.Save();
                    amortizationTable.Add(scheduleEntry);
                    previousBalance = newBalance;   
                }
                else if (loanTypeCode == "2. Fixed Principal")
                {
                    // For Fixed Principal loans:
                    // 1. Principal is fixed
                    principalPayment = fixedPrincipal ?? (loanAmount /numberOfPayments);
                    // 2. Calculate interest on remaining balance
                    decimal interestPayment = previousBalance * effectiveRate;

                    // 3. Total payment (Amortization Amount) is the principal payment plus interestPayment = installment
                    decimal amortizationAmount = principalPayment + interestPayment;

                    // 4. Calculate new ending balance
                    decimal newBalance = previousBalance - principalPayment;        

                    // 5. Populate schedule entry
                    scheduleEntry.SetColumnValue("RocaAmortParameterId", parameterId);
                    scheduleEntry.SetColumnValue("RocaAmortizationDate", paymentDate);      
                    scheduleEntry.SetColumnValue("RocaPeriodNumber", i + 1);
                    scheduleEntry.SetColumnValue("RocaAmortizationBeginningBalance", previousBalance);
                    scheduleEntry.SetColumnValue("RocaAmortizationAmount", amortizationAmount);      
                    scheduleEntry.SetColumnValue("RocaAmortizationPrincipal", principalPayment);
                    scheduleEntry.SetColumnValue("RocaAmortizationInterest", interestPayment);
                    scheduleEntry.SetColumnValue("RocaAmortizationEndingBalance", newBalance);   

                    scheduleEntry.Save();
                    amortizationTable.Add(scheduleEntry);
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

        private string GetLoanTypeCode(Guid loanType)
        {
            var select = new Select(UserConnection)
                .Column("Name")
                .From("RocaLoanType")
                .Where("Id").IsEqual(Column.Parameter(loanType)) as Select;
            
            return select.ExecuteScalar<string>();
        }

        #endregion
    }

    #endregion
}