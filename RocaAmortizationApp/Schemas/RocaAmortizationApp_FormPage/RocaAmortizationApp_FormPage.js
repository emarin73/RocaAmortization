define("RocaAmortizationApp_FormPage", /**SCHEMA_DEPS*/[]/**SCHEMA_DEPS*/, function/**SCHEMA_ARGS*/()/**SCHEMA_ARGS*/ {
	return {
		viewConfigDiff: /**SCHEMA_VIEW_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"name": "Tabs",
				"values": {
					"styleType": "default",
					"mode": "tab",
					"bodyBackgroundColor": "primary-contrast-500",
					"selectedTabTitleColor": "auto",
					"tabTitleColor": "auto",
					"underlineSelectedTabColor": "auto",
					"headerBackgroundColor": "auto"
				}
			},
			{
				"operation": "merge",
				"name": "GeneralInfoTab",
				"values": {
					"iconPosition": "only-text",
					"visible": true
				}
			},
			{
				"operation": "merge",
				"name": "CardToggleTabPanel",
				"values": {
					"styleType": "default",
					"bodyBackgroundColor": "primary-contrast-500",
					"selectedTabTitleColor": "auto",
					"tabTitleColor": "auto",
					"underlineSelectedTabColor": "auto",
					"headerBackgroundColor": "auto"
				}
			},
			{
				"operation": "merge",
				"name": "Feed",
				"values": {
					"dataSourceName": "PDS",
					"entitySchemaName": "RocaAmortParameters"
				}
			},
			{
				"operation": "merge",
				"name": "AttachmentList",
				"values": {
					"columns": [
						{
							"id": "5c280c63-2427-4fc1-bf98-57172f16e5c5",
							"code": "AttachmentListDS_Name",
							"caption": "#ResourceString(AttachmentListDS_Name)#",
							"dataValueType": 28,
							"width": 200
						}
					]
				}
			},
			{
				"operation": "insert",
				"name": "Button_Calculate",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_Calculate_caption)#",
					"color": "outline",
					"disabled": false,
					"size": "medium",
					"iconPosition": "left-icon",
					"visible": true,
					"icon": "calculator-icon",
					"clicked": {
						"request": "crt.RunBusinessProcessRequest",
						"params": {
							"processName": "RocaProcess_GenerateSchedule",
							"processRunType": "ForTheSelectedPage",
							"showNotification": true,
							"recordIdProcessParameterName": "ParameterId"
						}
					},
					"clickMode": "default"
				},
				"parentName": "CardToggleContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Button_Refresh",
				"values": {
					"type": "crt.Button",
					"caption": "#ResourceString(Button_Refresh_caption)#",
					"color": "default",
					"disabled": false,
					"size": "medium",
					"iconPosition": "only-icon",
					"visible": true,
					"clicked": {
						"request": "crt.LoadDataRequest",
						"params": {
							"config": {
								"loadType": "reload",
								"useLastLoadParameters": true
							},
							"dataSourceName": "DataGrid_vs1cy49DS"
						}
					},
					"clickMode": "default",
					"icon": "reload-icon"
				},
				"parentName": "CardToggleContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "RocaName",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.Input",
					"label": "$Resources.Strings.RocaName",
					"control": "$RocaName",
					"labelPosition": "auto"
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_ParentObject",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaAmortParent_qiozk2z",
					"labelPosition": "auto",
					"control": "$PDS_RocaAmortParent_qiozk2z",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": [],
					"visible": true,
					"readonly": false,
					"placeholder": "",
					"tooltip": "",
					"valueDetails": null
				},
				"parentName": "SideAreaProfileContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "addRecord_hd0c31j",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_hd0c31j_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_ParentObject",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_GPParam",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_72l2nd5_caption)#)#",
					"labelType": "headline-1",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "#0B8500",
					"labelBackgroundColor": "#CBF4DB",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "SideContainer",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridContainer_ofg5obi",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"padding": {
						"top": "medium",
						"right": "large",
						"bottom": "medium",
						"left": "large"
					},
					"color": "primary",
					"borderRadius": "medium",
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "SideContainer",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "FlexContainer_g5e04z4",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.FlexContainer",
					"direction": "column",
					"items": [],
					"fitContent": true
				},
				"parentName": "GridContainer_ofg5obi",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NumberInput_cz26pyv",
				"values": {
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.PDS_RocaGracePeriodInMonths_0bb4usx",
					"labelPosition": "auto",
					"control": "$PDS_RocaGracePeriodInMonths_0bb4usx"
				},
				"parentName": "FlexContainer_g5e04z4",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_GPIntMethod",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaGracePeriodIntMethod_diymhnu",
					"labelPosition": "auto",
					"control": "$PDS_RocaGracePeriodIntMethod_diymhnu",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": []
				},
				"parentName": "FlexContainer_g5e04z4",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "addRecord_kogwa7x",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_kogwa7x_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_GPIntMethod",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_GPCycle",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaGracePeriodCycle_ezjgatx",
					"labelPosition": "auto",
					"control": "$PDS_RocaGracePeriodCycle_ezjgatx",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": [],
					"visible": false,
					"readonly": false,
					"placeholder": "",
					"tooltip": ""
				},
				"parentName": "FlexContainer_g5e04z4",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "addRecord_5jmhtdy",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_5jmhtdy_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_GPCycle",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_CompCycle",
				"values": {
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaColumn1_1n7k163",
					"labelPosition": "auto",
					"control": "$PDS_RocaColumn1_1n7k163",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": [],
					"valueDetails": null
				},
				"parentName": "FlexContainer_g5e04z4",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "addRecord_r08jpsm",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_r08jpsm_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_CompCycle",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "Label_LoanParam",
				"values": {
					"type": "crt.Label",
					"caption": "#MacrosTemplateString(#ResourceString(Label_LoanParam_caption)#)#",
					"labelType": "headline-1",
					"labelThickness": "default",
					"labelEllipsis": false,
					"labelColor": "#0B8500",
					"labelBackgroundColor": "#CBF4DB",
					"labelTextAlign": "start",
					"visible": true
				},
				"parentName": "SideContainer",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "GridContainer_ueya5dp",
				"values": {
					"type": "crt.GridContainer",
					"columns": [
						"minmax(32px, 1fr)"
					],
					"rows": "minmax(max-content, 32px)",
					"gap": {
						"columnGap": "large",
						"rowGap": "none"
					},
					"items": [],
					"fitContent": true,
					"padding": {
						"top": "medium",
						"right": "large",
						"bottom": "medium",
						"left": "large"
					},
					"color": "primary",
					"borderRadius": "medium",
					"visible": true,
					"alignItems": "stretch"
				},
				"parentName": "SideContainer",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "NumberInput_q1e37aw",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.PDS_RocaLoanAmount_yt7qe6c",
					"labelPosition": "auto",
					"control": "$PDS_RocaLoanAmount_yt7qe6c"
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NumberInput_jhg4383",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 2,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.PDS_RocaInterestRate_rbm5suc",
					"labelPosition": "auto",
					"control": "$PDS_RocaInterestRate_rbm5suc"
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "ComboBox_y327x3b",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 3,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaLoanType_3lzh8p4",
					"labelPosition": "auto",
					"control": "$PDS_RocaLoanType_3lzh8p4",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": []
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "addRecord_8aaulyv",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_8aaulyv_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_y327x3b",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "NumberInput_4vlsks7",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 4,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.NumberInput",
					"label": "$Resources.Strings.PDS_RocaLoanTermInMonths_xoa7d4z",
					"labelPosition": "auto",
					"control": "$PDS_RocaLoanTermInMonths_xoa7d4z"
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "ComboBox_2q1z6nx",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 5,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaPaymentFrequency_jqy5msj",
					"labelPosition": "auto",
					"control": "$PDS_RocaPaymentFrequency_jqy5msj",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": []
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 4
			},
			{
				"operation": "insert",
				"name": "addRecord_28j3zov",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_28j3zov_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_2q1z6nx",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "ComboBox_r9s9g5y",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 6,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.ComboBox",
					"label": "$Resources.Strings.PDS_RocaCompundFrequency_m8xw23x",
					"labelPosition": "auto",
					"control": "$PDS_RocaCompundFrequency_m8xw23x",
					"listActions": [],
					"showValueAsLink": true,
					"controlActions": []
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 5
			},
			{
				"operation": "insert",
				"name": "addRecord_qsgrdf4",
				"values": {
					"code": "addRecord",
					"type": "crt.ComboboxSearchTextAction",
					"icon": "combobox-add-new",
					"caption": "#ResourceString(addRecord_qsgrdf4_caption)#",
					"clicked": {
						"request": "crt.CreateRecordFromLookupRequest",
						"params": {}
					}
				},
				"parentName": "ComboBox_r9s9g5y",
				"propertyName": "listActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DateTimePicker_w6sevj7",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 7,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.DateTimePicker",
					"pickerType": "date",
					"label": "$Resources.Strings.PDS_Roca1stDisbursementDate_h3037ew",
					"labelPosition": "auto",
					"control": "$PDS_Roca1stDisbursementDate_h3037ew"
				},
				"parentName": "GridContainer_ueya5dp",
				"propertyName": "items",
				"index": 6
			},
			{
				"operation": "insert",
				"name": "DateTimePicker_0rv32hv",
				"values": {
					"type": "crt.DateTimePicker",
					"pickerType": "date",
					"label": "$Resources.Strings.PDS_Roca1stRepaymentDate_iggl86o",
					"labelPosition": "auto",
					"control": "$PDS_Roca1stRepaymentDate_iggl86o",
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					}
				},
				"parentName": "GeneralInfoTabContainer",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_AmortizationTable",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": false
						},
						"editable": {
							"enable": false,
							"itemsCreation": false,
							"floatingEditPanel": false
						}
					},
					"items": "$DataGrid_vs1cy49",
					"activeRow": "$DataGrid_vs1cy49_ActiveRow",
					"selectionState": "$DataGrid_vs1cy49_SelectionState",
					"_selectionOptions": {
						"attribute": "DataGrid_vs1cy49_SelectionState"
					},
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_vs1cy49DS_Id",
					"columns": [
						{
							"id": "e28809af-ea9d-e723-ed9e-968edca4baf7",
							"code": "DataGrid_vs1cy49DS_RocaAmortParameter",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortParameter)#",
							"dataValueType": 10
						},
						{
							"id": "baa2561e-58c7-ce82-faeb-622794b27353",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationNumber",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationNumber)#",
							"dataValueType": 27,
							"width": 153.99998474121094
						},
						{
							"id": "1ddd220c-229f-f754-3178-e1248b624c6c",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationDate",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationDate)#",
							"dataValueType": 8
						},
						{
							"id": "e23cdb07-b514-0b1a-2b80-0aa40dbe6f21",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationBeginningBalance",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationBeginningBalance)#",
							"dataValueType": 6
						},
						{
							"id": "d1f6839d-0819-3ffd-e0d2-420292c9d2fe",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationAmount",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationAmount)#",
							"dataValueType": 6
						},
						{
							"id": "ed41f3af-b6ba-8c01-33f7-f718bccb1be0",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationPrincipal",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationPrincipal)#",
							"dataValueType": 6
						},
						{
							"id": "d30e0cde-3cf1-7666-d5ee-47127f9a8c48",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationInterest",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationInterest)#",
							"dataValueType": 6
						},
						{
							"id": "98689275-c3fe-f350-de43-4186a903d322",
							"code": "DataGrid_vs1cy49DS_RocaAmortizationEndingBalance",
							"caption": "#ResourceString(DataGrid_vs1cy49DS_RocaAmortizationEndingBalance)#",
							"dataValueType": 6
						}
					],
					"placeholder": false,
					"bulkActions": []
				},
				"parentName": "GeneralInfoTab",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_vs1cy49_AddTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Add tag",
					"icon": "tag-icon",
					"clicked": {
						"request": "crt.AddTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_vs1cy49DS",
							"filters": "$DataGrid_vs1cy49 | crt.ToCollectionFilters : 'DataGrid_vs1cy49' : $DataGrid_vs1cy49_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_vs1cy49_SelectionState"
						}
					},
					"items": []
				},
				"parentName": "DataGrid_AmortizationTable",
				"propertyName": "bulkActions",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_vs1cy49_RemoveTagsBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Remove tag",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.RemoveTagsInRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_vs1cy49DS",
							"filters": "$DataGrid_vs1cy49 | crt.ToCollectionFilters : 'DataGrid_vs1cy49' : $DataGrid_vs1cy49_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_vs1cy49_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_vs1cy49_AddTagsBulkAction",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_vs1cy49_ExportToExcelBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Export to Excel",
					"icon": "export-button-icon",
					"clicked": {
						"request": "crt.ExportDataGridToExcelRequest",
						"params": {
							"viewName": "DataGrid_AmortizationTable",
							"filters": "$DataGrid_vs1cy49 | crt.ToCollectionFilters : 'DataGrid_vs1cy49' : $DataGrid_vs1cy49_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_vs1cy49_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_AmortizationTable",
				"propertyName": "bulkActions",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "DataGrid_vs1cy49_DeleteBulkAction",
				"values": {
					"type": "crt.MenuItem",
					"caption": "Delete",
					"icon": "delete-button-icon",
					"clicked": {
						"request": "crt.DeleteRecordsRequest",
						"params": {
							"dataSourceName": "DataGrid_vs1cy49DS",
							"filters": "$DataGrid_vs1cy49 | crt.ToCollectionFilters : 'DataGrid_vs1cy49' : $DataGrid_vs1cy49_SelectionState | crt.SkipIfSelectionEmpty : $DataGrid_vs1cy49_SelectionState"
						}
					}
				},
				"parentName": "DataGrid_AmortizationTable",
				"propertyName": "bulkActions",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "TabContainer_Application",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_Amortization_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "Tabs",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "GridContainer_dn2wxvw",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": 0
					}
				},
				"parentName": "TabContainer_Application",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DateTimePicker_tb28a2l",
				"values": {
					"layoutConfig": {
						"column": 1,
						"row": 1,
						"colSpan": 1,
						"rowSpan": 1
					},
					"type": "crt.DateTimePicker",
					"pickerType": "date",
					"label": "$Resources.Strings.PDS_Roca1stRepaymentDate_un834lh",
					"labelPosition": "auto",
					"control": "$PDS_Roca1stRepaymentDate_un834lh"
				},
				"parentName": "GridContainer_dn2wxvw",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "DataGrid_ho3es9z",
				"values": {
					"type": "crt.DataGrid",
					"features": {
						"rows": {
							"selection": {
								"enable": true,
								"multiple": true
							}
						},
						"editable": {
							"enable": false,
							"itemsCreation": false,
							"floatingEditPanel": false
						}
					},
					"items": "$DataGrid_ho3es9z",
					"visible": true,
					"fitContent": true,
					"primaryColumnName": "DataGrid_ho3es9zDS_Id",
					"columns": [
						{
							"id": "2b76a63a-15e1-be35-bf09-53a5fe07e79c",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationNumber",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationNumber)#",
							"dataValueType": 27
						},
						{
							"id": "d4f87e3d-4207-7476-9524-60faf2cbd9ba",
							"code": "DataGrid_ho3es9zDS_RocaPeriodNumber",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaPeriodNumber)#",
							"dataValueType": 4
						},
						{
							"id": "dd35b5d5-9799-83cb-e77f-00767cdd4f55",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationNumber",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationNumber)#",
							"dataValueType": 27
						},
						{
							"id": "aaf5a652-8567-2bc2-db1a-465a5ca9bbd6",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationBeginningBalance",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationBeginningBalance)#",
							"dataValueType": 6
						},
						{
							"id": "53579a07-c4a4-9e74-98e9-a6338b944cd4",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationAmount",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationAmount)#",
							"dataValueType": 6
						},
						{
							"id": "dd8ab9a3-32db-352e-b27a-61a0bdad70cd",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationPrincipal",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationPrincipal)#",
							"dataValueType": 6
						},
						{
							"id": "b478d03e-e65a-7c0c-03dd-8943868b22ce",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationInterest",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationInterest)#",
							"dataValueType": 6
						},
						{
							"id": "30fea271-6d48-a4d2-ccd0-700bf2fd4531",
							"code": "DataGrid_ho3es9zDS_RocaAmortizationEndingBalance",
							"caption": "#ResourceString(DataGrid_ho3es9zDS_RocaAmortizationEndingBalance)#",
							"dataValueType": 6
						}
					],
					"placeholder": false
				},
				"parentName": "TabContainer_Application",
				"propertyName": "items",
				"index": 1
			},
			{
				"operation": "insert",
				"name": "TabContainer_Loan",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_r28zy26_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "Tabs",
				"propertyName": "items",
				"index": 2
			},
			{
				"operation": "insert",
				"name": "GridContainer_ap4wgka",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": 0
					}
				},
				"parentName": "TabContainer_Loan",
				"propertyName": "items",
				"index": 0
			},
			{
				"operation": "insert",
				"name": "TabContainer_Inquiry",
				"values": {
					"type": "crt.TabContainer",
					"items": [],
					"caption": "#ResourceString(TabContainer_c0k8f09_caption)#",
					"iconPosition": "only-text",
					"visible": true
				},
				"parentName": "Tabs",
				"propertyName": "items",
				"index": 3
			},
			{
				"operation": "insert",
				"name": "GridContainer_xwn59yr",
				"values": {
					"type": "crt.GridContainer",
					"items": [],
					"rows": "minmax(32px, max-content)",
					"columns": [
						"minmax(32px, 1fr)",
						"minmax(32px, 1fr)"
					],
					"gap": {
						"columnGap": "large",
						"rowGap": 0
					}
				},
				"parentName": "TabContainer_Inquiry",
				"propertyName": "items",
				"index": 0
			}
		]/**SCHEMA_VIEW_CONFIG_DIFF*/,
		viewModelConfigDiff: /**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [
					"attributes"
				],
				"values": {
					"RocaName": {
						"modelConfig": {
							"path": "PDS.RocaName"
						}
					},
					"PDS_RocaGracePeriodIntMethod_diymhnu": {
						"modelConfig": {
							"path": "PDS.RocaGracePeriodIntMethod"
						}
					},
					"PDS_RocaGracePeriodCycle_ezjgatx": {
						"modelConfig": {
							"path": "PDS.RocaGracePeriodCycle"
						}
					},
					"PDS_RocaLoanAmount_yt7qe6c": {
						"modelConfig": {
							"path": "PDS.RocaLoanAmount"
						}
					},
					"PDS_RocaPaymentFrequency_jqy5msj": {
						"modelConfig": {
							"path": "PDS.RocaPaymentFrequency"
						}
					},
					"PDS_RocaCompundFrequency_m8xw23x": {
						"modelConfig": {
							"path": "PDS.RocaCompundFrequency"
						}
					},
					"PDS_RocaInterestRate_rbm5suc": {
						"modelConfig": {
							"path": "PDS.RocaInterestRate"
						}
					},
					"PDS_RocaLoanTermInMonths_xoa7d4z": {
						"modelConfig": {
							"path": "PDS.RocaLoanTermInMonths"
						}
					},
					"PDS_RocaLoanType_3lzh8p4": {
						"modelConfig": {
							"path": "PDS.RocaLoanType"
						}
					},
					"PDS_RocaGracePeriodInMonths_0bb4usx": {
						"modelConfig": {
							"path": "PDS.RocaGracePeriodInMonths"
						}
					},
					"PDS_Roca1stRepaymentDate_iggl86o": {
						"modelConfig": {
							"path": "PDS.Roca1stRepaymentDate"
						}
					},
					"PDS_Roca1stDisbursementDate_h3037ew": {
						"modelConfig": {
							"path": "PDS.Roca1stDisbursementDate"
						}
					},
					"DataGrid_vs1cy49": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_vs1cy49DS",
							"sortingConfig": {
								"default": [
									{
										"direction": "desc",
										"columnName": "RocaAmortizationBeginningBalance"
									}
								]
							}
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_vs1cy49DS_RocaAmortParameter": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortParameter"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationNumber": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationNumber"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationDate": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationDate"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationBeginningBalance": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationBeginningBalance"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationAmount": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationAmount"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationPrincipal": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationPrincipal"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationInterest": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationInterest"
									}
								},
								"DataGrid_vs1cy49DS_RocaAmortizationEndingBalance": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.RocaAmortizationEndingBalance"
									}
								},
								"DataGrid_vs1cy49DS_Id": {
									"modelConfig": {
										"path": "DataGrid_vs1cy49DS.Id"
									}
								}
							}
						}
					},
					"PDS_RocaAmortParent_qiozk2z": {
						"modelConfig": {
							"path": "PDS.RocaAmortParent"
						}
					},
					"PDS_Roca1stRepaymentDate_un834lh": {
						"modelConfig": {
							"path": "PDS.Roca1stRepaymentDate"
						}
					},
					"DataGrid_ho3es9z": {
						"isCollection": true,
						"modelConfig": {
							"path": "DataGrid_ho3es9zDS"
						},
						"viewModelConfig": {
							"attributes": {
								"DataGrid_ho3es9zDS_RocaAmortizationNumber": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationNumber"
									}
								},
								"DataGrid_ho3es9zDS_RocaPeriodNumber": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaPeriodNumber"
									}
								},
								"DataGrid_ho3es9zDS_RocaAmortizationBeginningBalance": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationBeginningBalance"
									}
								},
								"DataGrid_ho3es9zDS_RocaAmortizationAmount": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationAmount"
									}
								},
								"DataGrid_ho3es9zDS_RocaAmortizationPrincipal": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationPrincipal"
									}
								},
								"DataGrid_ho3es9zDS_RocaAmortizationInterest": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationInterest"
									}
								},
								"DataGrid_ho3es9zDS_RocaAmortizationEndingBalance": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.RocaAmortizationEndingBalance"
									}
								},
								"DataGrid_ho3es9zDS_Id": {
									"modelConfig": {
										"path": "DataGrid_ho3es9zDS.Id"
									}
								}
							}
						}
					},
					"PDS_RocaColumn1_1n7k163": {
						"modelConfig": {
							"path": "PDS.RocaGracePeriodCompCycle"
						}
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"attributes",
					"Id",
					"modelConfig"
				],
				"values": {
					"path": "PDS.Id"
				}
			}
		]/**SCHEMA_VIEW_MODEL_CONFIG_DIFF*/,
		modelConfigDiff: /**SCHEMA_MODEL_CONFIG_DIFF*/[
			{
				"operation": "merge",
				"path": [],
				"values": {
					"primaryDataSourceName": "PDS",
					"dependencies": {
						"DataGrid_vs1cy49DS": [
							{
								"attributePath": "RocaAmortParameter",
								"relationPath": "PDS.Id"
							}
						],
						"DataGrid_ho3es9zDS": [
							{
								"attributePath": "RocaAmortizationApplicationNumber",
								"relationPath": "PDS.Id"
							}
						]
					}
				}
			},
			{
				"operation": "merge",
				"path": [
					"dataSources"
				],
				"values": {
					"PDS": {
						"type": "crt.EntityDataSource",
						"config": {
							"entitySchemaName": "RocaAmortParameters"
						},
						"scope": "page"
					},
					"DataGrid_vs1cy49DS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "RocaLoanAmortizationTable",
							"attributes": {
								"RocaAmortParameter": {
									"path": "RocaAmortParameter"
								},
								"RocaAmortizationNumber": {
									"path": "RocaAmortizationNumber"
								},
								"RocaAmortizationDate": {
									"path": "RocaAmortizationDate"
								},
								"RocaAmortizationBeginningBalance": {
									"path": "RocaAmortizationBeginningBalance"
								},
								"RocaAmortizationAmount": {
									"path": "RocaAmortizationAmount"
								},
								"RocaAmortizationPrincipal": {
									"path": "RocaAmortizationPrincipal"
								},
								"RocaAmortizationInterest": {
									"path": "RocaAmortizationInterest"
								},
								"RocaAmortizationEndingBalance": {
									"path": "RocaAmortizationEndingBalance"
								}
							}
						}
					},
					"DataGrid_ho3es9zDS": {
						"type": "crt.EntityDataSource",
						"scope": "viewElement",
						"config": {
							"entitySchemaName": "RocaLoanAmortizationTable",
							"attributes": {
								"RocaAmortizationNumber": {
									"path": "RocaAmortizationNumber"
								},
								"RocaPeriodNumber": {
									"path": "RocaPeriodNumber"
								},
								"RocaAmortizationBeginningBalance": {
									"path": "RocaAmortizationBeginningBalance"
								},
								"RocaAmortizationAmount": {
									"path": "RocaAmortizationAmount"
								},
								"RocaAmortizationPrincipal": {
									"path": "RocaAmortizationPrincipal"
								},
								"RocaAmortizationInterest": {
									"path": "RocaAmortizationInterest"
								},
								"RocaAmortizationEndingBalance": {
									"path": "RocaAmortizationEndingBalance"
								}
							}
						}
					}
				}
			}
		]/**SCHEMA_MODEL_CONFIG_DIFF*/,
		handlers: /**SCHEMA_HANDLERS*/[]/**SCHEMA_HANDLERS*/,
		converters: /**SCHEMA_CONVERTERS*/{}/**SCHEMA_CONVERTERS*/,
		validators: /**SCHEMA_VALIDATORS*/{}/**SCHEMA_VALIDATORS*/
	};
});