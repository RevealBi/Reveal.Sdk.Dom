using Reveal.Sdk.Data.MySql;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;

namespace Sandbox.Helpers
{
    internal class DataSourceFactory
    {
        static IDataSourceItemFactory _factory = new DataSourceItemFactory();
        static DataSource _excelDataSource = new DataSource()
        {
            Id = "RestExcel",
            Title = "Excel Data Source",
            Subtitle = "The Data Source for Excel via REST",
        };

        static string _restExcelUri = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx";    

        internal static DataSourceItem GetMarketingDataSourceItem()
        {
            var excelDataSourceItem = new RestDataSourceItem("Marketing", _excelDataSource)
            {
                Subtitle = "Excel Data Source Item",
                Uri = _restExcelUri,
                IsAnonymous = true,
                Fields = GetMarketingDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Marketing");

            return excelDataSourceItem;
        }

        internal static DataSourceItem GetHealthcareDataSourceItem()
        {
            var excelDataSourceItem = new RestDataSourceItem("Healthcare", _excelDataSource)
            {
                Subtitle = "Excel Data Source Item",
                Uri = _restExcelUri,
                IsAnonymous = true,
                Fields = GetHealthcareDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Healthcare");

            return excelDataSourceItem;
        }

        internal static DataSourceItem GetManufacturingDataSourceItem()
        {
            var excelDataSourceItem = new RestDataSourceItem("Manufacturing", _excelDataSource)
            {
                Subtitle = "Excel Data Source Item",
                Uri = _restExcelUri,
                IsAnonymous = true,
                Fields = GetManufacturingDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Manufacturing");

            return excelDataSourceItem;
        }

        internal static DataSourceItem GetSalesDataSourceItem()
        {
            var excelDataSourceItem = new RestDataSourceItem("Sales", _excelDataSource)
            {
                Subtitle = "Excel Data Source Item",
                Uri = _restExcelUri,
                IsAnonymous = true,
                Fields = GetSalesDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Sales");

            return excelDataSourceItem;
        }

        internal static List<IField> GetRevenueDataSourceFields()
        {
            return new List<IField>()
            {
                new DateField("Date"),
                new TextField("State"),
                new NumberField("Revenue"),
                new NumberField("RevenueBudget"),
                new NumberField("RevenueBudgetLY"),
                new NumberField("GM"),
                new NumberField("GrossMarginLY"),
                new NumberField("GM%"),
                new NumberField("GrossMargin%LY"),
                new NumberField("RevenueperCompany"),
                new TextField("Company"),
                new TextField("Division")
            };
        }

        internal static List<IField> GetHealthcareDataSourceFields()
        {
            return new List<IField>
            {
                new DateField("Date"),
                new NumberField("Number of Inpatients"),
                new NumberField("Number of Outpatients"),
                new NumberField("Treatment Costs "),
                new NumberField("ER Wait Time"),
                new TextField("Divison"),
                new TextField("Satisfaction"),
                new NumberField("Length of Stay "),
                new NumberField("Charges per MD"),
                new NumberField("Current Paitents")
            };
        }

        internal static List<IField> GetManufacturingDataSourceFields()
        {
            return new List<IField>
            {
                new DateField("Date"),
                new NumberField("Units Lost"),
                new NumberField("Overall Plant Productivity "),
                new NumberField("Operators Available "),
                new TextField("Operators by Function"),
                new NumberField("Units Produced"),
                new TextField("Product"),
                new NumberField("Efficiency"),
                new TextField("Line"),
                new NumberField("Orders In"),
                new NumberField("Orders Shipped "),
                new NumberField("Cost of Labor "),
                new NumberField("Revenue")
            };
        }

        internal static List<IField> GetMarketingDataSourceFields()
        {
            return new List<IField>
            {
                new DateField("Date"),
                new NumberField("Spend"),
                new NumberField("Budget"),
                new NumberField("CTR"),
                new NumberField("Avg. CPC"),
                new NumberField("Traffic"),
                new NumberField("Paid Traffic"),
                new NumberField("Other Traffic"),
                new NumberField("Conversions"),
                new TextField("Territory"),
                new TextField("CampaignID"),
                new NumberField("New Seats"),
                new NumberField("Paid %"),
                new NumberField("Organic %")
            };
        }

        internal static List<IField> GetSalesDataSourceFields()
        {
            return new List<IField>
            {
                new TextField("Territory"),
                new DateField("Date"),
                new NumberField("Quota"),
                new NumberField("Leads"),
                new NumberField("Hot Leads"),
                new NumberField("New Seats"),
                new NumberField("New Sales"),
                new NumberField("Renewal Seats"),
                new NumberField("Renewal Sales "),
                new TextField("Employee"),
                new NumberField("Pipepline"),
                new NumberField("Forecasted"),
                new NumberField("Revenue"),
                new NumberField("Total Opportunites"),
                new TextField("Product")
            };
        }

        internal static List<IField> GetSalesByCategoryFields()
        {
            return new List<IField>
            {
                new NumberField("CategoryID"),
                new TextField("CategoryName"),
                new TextField("ProductName"),
                new NumberField("ProductSales"),
            };
        }

        internal static List<IField> GetCsvDataSourceFields()
        {
            return new List<IField>
            {
                new TextField("the_geom"),
                new NumberField("School_ID"),
                new TextField("School_Nm"),
                new TextField("Sch_Addr"),
                new TextField("Grade_Cat"),
                new TextField("Grades"),
                new TextField("Sch_Type"),
                new NumberField("X"),
                new NumberField("Y"),
            };
        }

        internal static List<IField> GetOHLCDataSourceFields()
        {
            return new List<IField>
            {
                new DateField("Date"),
                new NumberField("Open"),
                new NumberField("High"),
                new NumberField("Low"),
                new NumberField("Close"),
                new NumberField("Volume"),
            };
        }

        internal class MySQLDataSourceFactory
        {
            static DataSource _mysqlDataSource = new MySQLDataSource()
            {
                Id = "MySQL",
                Title = "MySQL Data Source",
                Subtitle = "The Data Source for MySQL",
                Host = "revealdb01.infragistics.local",
                Database = "northwind",
                Port = "3306"
            };

            internal static DataSourceItem GetEmployeeDSItem()
            {
                var employeesDSItem = new MySqlDataSourceItem("Employees", _mysqlDataSource)
                {
                    Title = "MySQL Employee",
                    Subtitle = "MySQL DS Item for Employee",
                    Database = "northwind",
                    Table = "employees",
                    Fields = GetEmployeeColumnFields()
                };

                return employeesDSItem;
            }

            internal static List<IField> GetEmployeeColumnFields()
            {
                return new List<IField>
                {
                    new NumberField("ReportsTo"),
                    new NumberField("EmployeeID"),
                    new TextField("Country"),
                };
            }
        }
    }
}
