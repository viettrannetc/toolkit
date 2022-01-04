//using ExcelDataReader;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Text;
//using System.Linq;
//using OfficeOpenXml;
//using System.Drawing;
//using OfficeOpenXml.Style;

//namespace BusinessLibrary.Ultilities
//{
//	public class Excel
//	{
//		public List<DataTable> Read(string filePath)
//		{
//			List<DataTable> resultObject = new List<DataTable>();
//			System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

//			using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
//			using (var reader = ExcelReaderFactory.CreateReader(stream))
//			{
//				var result = reader.AsDataSet(new ExcelDataSetConfiguration()
//				{
//					ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
//					{
//						//UseHeaderRow = true
//						FilterRow = rowReader => rowReader.Depth > 1
//					}
//				});

//				foreach (DataTable item in result.Tables)
//				{
//					resultObject.Add(item);
//				}

//				return resultObject;
//			}
//		}

//		private DateTime EndDate = new DateTime(2021, 11, 30);

//		//public ExcelPackage CreateExcelFile()
//		//{
//		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//		//	string excelName = $"{Guid.NewGuid()}";
//		//	string filePath = $@"C:\Projects\Personal\Sharepoint\Sharepoint\{excelName}.xlsx";

//		//	ExcelPackage excel = new ExcelPackage(new FileInfo(filePath));
//		//	return excel;
//		//}


//	}
//}
