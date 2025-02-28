using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace DbAutomationTests;

public static class ReadExcelHelper
{
    public static void Read()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        string filePath = "E:\\GitHub-PD\\Nunit-Oracle\\DbAutomationTests\\DbAutomationTests\\TestData\\Book1.xlsx";

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var workbook = package.Workbook;

            // Check if the workbook has any worksheets
            if (workbook.Worksheets.Count == 0)
            {
                Console.WriteLine("The Excel file has no worksheets.");
                return;
            }

            // Get the worksheet by name
            var worksheet = workbook.Worksheets["Sheet1"];

            if (worksheet == null)
            {
                Console.WriteLine("Sheet1 not found in the Excel file.");
                return;
            }

            int rowCount = worksheet.Dimension?.Rows ?? 0;
            int colCount = worksheet.Dimension?.Columns ?? 0;

            if (rowCount == 0 || colCount == 0)
            {
                Console.WriteLine("The worksheet is empty.");
                return;
            }

            // Read data
            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= colCount; col++)
                {
                    Console.Write(worksheet.Cells[row, col].Text + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}


