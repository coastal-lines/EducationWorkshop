using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using ExcelDataReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverForExcelTests
{
    [TestClass]
    public class ExcelTests
    {
        static WindowsDriver<WindowsElement> session;
        static DataSet mTestData;

        [ClassInitialize]
        public static void BeforeAllTests(TestContext testContext)
        {
            using (var stream = File.Open(@"c:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\WinAppDriverForExcelTests\Data\TestConfigData.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    mTestData = reader.AsDataSet(
                        new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        }
                        );
                }
            }
        }

        private string ReadValueFromTestDataSource(string tableName, string columnName, string dataKey)
        {
            var returnRow = mTestData.Tables[tableName].Select($"DataKey='{dataKey}'");

            Assert.IsFalse(returnRow.Length <= 0, "Data was not found");

            return Convert.ToString(returnRow[0][columnName]);
        }

        [TestMethod]
        public void ReadDataFromXlsxTest()
        {
            string valueFromSheet = ReadValueFromTestDataSource(tableName: "ReadValuesFromExcel", columnName: "InputData1",dataKey: "arrange");

            Debug.WriteLine($"Value from sheet: {valueFromSheet}");

            //InputData2	ExpectedResults
            Debug.WriteLine($@"Value from sheet: {ReadValueFromTestDataSource(tableName: "ReadValuesFromExcel", columnName: "InputData2", dataKey: "arrange")}");
            Debug.WriteLine($@"Value from sheet: {ReadValueFromTestDataSource(tableName: "ReadValuesFromExcel", columnName: "ExpectedResults", dataKey: "arrange")}");
        }

        [TestMethod]
        public void OpenExcelWithoutSplashScreen()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Excel");
            appiumOptions.AddAdditionalCapability("appArguments", "/e");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }

        [TestMethod]
        public void OpenExcelFileWithoutSplashScreen()
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Excel");
            appiumOptions.AddAdditionalCapability("appArguments", @"/e c:\Work2\Projects\GIT\EducationWorkshop\WinAppdriver_automation\WinAppDriverForExcelTests\Data\TaskReport.csv");
            session = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        }
    }
}
