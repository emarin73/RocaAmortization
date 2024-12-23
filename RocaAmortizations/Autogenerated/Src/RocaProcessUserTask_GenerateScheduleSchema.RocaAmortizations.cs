﻿namespace Terrasoft.Core.Process.Configuration
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

	[DesignModeProperty(Name = "RocaCompoundFrequency", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaCompoundFrequency.Caption", DescriptionResourceItem = "Parameters.RocaCompoundFrequency.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaTermsInMonths", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaTermsInMonths.Caption", DescriptionResourceItem = "Parameters.RocaTermsInMonths.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaFixedPrincipal", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaFixedPrincipal.Caption", DescriptionResourceItem = "Parameters.RocaFixedPrincipal.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaInstallment", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaInstallment.Caption", DescriptionResourceItem = "Parameters.RocaInstallment.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaPaymentFrequency", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaPaymentFrequency.Caption", DescriptionResourceItem = "Parameters.RocaPaymentFrequency.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "Roca1stDisbursementDate", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.Roca1stDisbursementDate.Caption", DescriptionResourceItem = "Parameters.Roca1stDisbursementDate.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaRate", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaRate.Caption", DescriptionResourceItem = "Parameters.RocaRate.Caption", UseSolutionStorage = true)]
	[DesignModeProperty(Name = "RocaAmortizationTable", Group = "", ValuesProvider = "ProcessSchemaParameterValueProvider", Editor="xtype=processschemaparametervalueedit;dataProvider=processschemaparametervalueprovider", ResourceManager = "0cfa8cc95a344f14a514fe116ae9f504", CaptionResourceItem = "Parameters.RocaAmortizationTable.Caption", DescriptionResourceItem = "Parameters.RocaAmortizationTable.Caption", UseSolutionStorage = true)]
	/// <exclude/>
	public partial class RocaProcessUserTask_GenerateSchedule : ProcessUserTask
	{

		#region Constructors: Public

		public RocaProcessUserTask_GenerateSchedule(UserConnection userConnection)
			: base(userConnection) {
			SchemaUId = new Guid("0cfa8cc9-5a34-4f14-a514-fe116ae9f504");
		}

		#endregion

		#region Properties: Public

		public virtual Guid RocaCompoundFrequency {
			get;
			set;
		}

		public virtual int RocaTermsInMonths {
			get;
			set;
		}

		public virtual Decimal RocaFixedPrincipal {
			get;
			set;
		}

		public virtual Decimal RocaInstallment {
			get;
			set;
		}

		public virtual Guid RocaPaymentFrequency {
			get;
			set;
		}

		public virtual DateTime Roca1stDisbursementDate {
			get;
			set;
		}

		public virtual Decimal RocaRate {
			get;
			set;
		}

		private Entity _rocaAmortizationTable;
		public virtual Entity RocaAmortizationTable {
			get {
				return _rocaAmortizationTable ?? (_rocaAmortizationTable = new Entity(UserConnection));
			}
			set {
				_rocaAmortizationTable = value;
			}
		}

		#endregion

		#region Methods: Public

		public override void WritePropertiesData(DataWriter writer) {
			writer.WriteStartObject(Name);
			base.WritePropertiesData(writer);
			if (Status == Core.Process.ProcessStatus.Inactive) {
				writer.WriteFinishObject();
				return;
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaCompoundFrequency")) {
					writer.WriteValue("RocaCompoundFrequency", RocaCompoundFrequency, Guid.Empty);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaTermsInMonths")) {
					writer.WriteValue("RocaTermsInMonths", RocaTermsInMonths, 0);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaFixedPrincipal")) {
					writer.WriteValue("RocaFixedPrincipal", RocaFixedPrincipal, 0m);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaInstallment")) {
					writer.WriteValue("RocaInstallment", RocaInstallment, 0m);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaPaymentFrequency")) {
					writer.WriteValue("RocaPaymentFrequency", RocaPaymentFrequency, Guid.Empty);
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("Roca1stDisbursementDate")) {
					writer.WriteValue("Roca1stDisbursementDate", Roca1stDisbursementDate, DateTime.ParseExact("01.01.0001 0:00", "dd.MM.yyyy H:mm", CultureInfo.InvariantCulture));
				}
			}
			if (UseFlowEngineMode) {
				if (!HasMapping("RocaRate")) {
					writer.WriteValue("RocaRate", RocaRate, 0m);
				}
			}
			if (UseFlowEngineMode) {
				if (RocaAmortizationTable != null && RocaAmortizationTable.Schema != null) {
					if (UseFlowEngineMode) {
						RocaAmortizationTable.Write(writer, "RocaAmortizationTable");
					} else {
						string serializedEntity = Entity.SerializeToJson(RocaAmortizationTable);
						writer.WriteValue("RocaAmortizationTable", serializedEntity, null);
					}
				}
			}
			writer.WriteFinishObject();
		}

		#endregion

		#region Methods: Protected

		protected override void ApplyPropertiesDataValues(DataReader reader) {
			base.ApplyPropertiesDataValues(reader);
			switch (reader.CurrentName) {
				case "RocaCompoundFrequency":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaCompoundFrequency = reader.GetGuidValue();
				break;
				case "RocaTermsInMonths":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaTermsInMonths = reader.GetIntValue();
				break;
				case "RocaFixedPrincipal":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaFixedPrincipal = reader.GetValue<System.Decimal>();
				break;
				case "RocaInstallment":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaInstallment = reader.GetValue<System.Decimal>();
				break;
				case "RocaPaymentFrequency":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaPaymentFrequency = reader.GetGuidValue();
				break;
				case "Roca1stDisbursementDate":
					if (!UseFlowEngineMode) {
						break;
					}
					Roca1stDisbursementDate = reader.GetDateTimeValue();
				break;
				case "RocaRate":
					if (!UseFlowEngineMode) {
						break;
					}
					RocaRate = reader.GetValue<System.Decimal>();
				break;
				case "RocaAmortizationTable":
					if (!UseFlowEngineMode) {
						break;
					}
					if (UseFlowEngineMode) {
						RocaAmortizationTable = reader.GetValue<Entity>();
					} else {
						RocaAmortizationTable = Entity.DeserializeFromJson(UserConnection, reader.GetValue<System.String>());
					}
				break;
			}
		}

		#endregion

	}

	#endregion

}

