//using OfficeOpenXml;
//using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLibrary.Ultilities
{
	public static class ExcelExtension
	{
		//public static ExcelPackage CreateExcelFile(this Excel excel)
		//{
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//	string excelName = $"{Guid.NewGuid()}";
		//	string filePath = $@"C:\Projects\Personal\Sharepoint\Sharepoint\{excelName}.xlsx";

		//	ExcelPackage excelPackage = new ExcelPackage(new FileInfo(filePath));
		//	return excelPackage;
		//}

		//public static ExcelPackage CreateWorksheets(this ExcelPackage tempFile, object data)
		//{
		//	foreach (var prop in data.GetType().GetProperties())
		//	{
		//		var values = (IEnumerable<object>)prop.GetValue(data);
		//		if (values == null || !values.Any()) continue;

		//		var value = values.Cast<object>().ToList();
		//		tempFile.AddWorkSheet(value);
		//	}

		//	return tempFile;
		//}

		//public static void AddWorkSheet(this ExcelPackage excel, List<object> objects)
		//{
		//	try
		//	{
		//		if (!objects.Any()) return;

		//		var workSheet = excel.Workbook.Worksheets.Add(objects[0].GetType().Name);
		//		workSheet.TabColor = Color.Black;
		//		workSheet.DefaultRowHeight = 12;
		//		//Header of table
		//		workSheet.Row(1).Height = 20;
		//		workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		workSheet.Row(1).Style.Font.Bold = true;

		//		var firstObject = objects[0];
		//		var properties = firstObject.GetType().GetProperties().Select(p => p.Name).ToList();
		//		for (int i = 0; i <= properties.Count - 1; i++)
		//		{
		//			workSheet.Cells[1, i + 1].Value = firstObject.GetType().GetProperties()[i].Name;
		//			int recordIndex = 2;
		//			foreach (var item in objects)
		//			{
		//				workSheet.Cells[recordIndex, i + 1].Value = item.GetType().GetProperties()[i].GetValue(item, null);
		//				recordIndex++;
		//			}
		//			workSheet.Column(i + 1).AutoFit();
		//		}

		//		excel.Save();
		//	}
		//	catch (Exception ex)
		//	{

		//		throw;
		//	}
		//}
	}
}
