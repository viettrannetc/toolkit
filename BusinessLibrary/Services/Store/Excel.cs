using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BusinessLibrary.Services.Store
{
	public class Excel
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename">"path\\to\\file.csv"</param>
		/// <param name="records"></param>
		/// <returns></returns>
		public bool Write<T>(string filename, List<T> records) where T : class
		{
			try
			{
				if (!File.Exists(filename))                 // Write to a file.
				{
					using (var writer = new StreamWriter(filename))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(records);
					}
				}
				else                                        // Append to the file.
				{
					var config = new CsvConfiguration(CultureInfo.InvariantCulture)
					{
						// Don't write the header again.
						HasHeaderRecord = false,
					};
					using (var stream = File.Open(filename, FileMode.Append))
					using (var writer = new StreamWriter(stream))
					using (var csv = new CsvWriter(writer, config))
					{
						csv.WriteRecords(records);
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename">"path\\to\\file.csv"</param>
		/// <returns></returns>
		public List<T> Get<T>(string filename) where T : class
		{
			try
			{
				if (File.Exists(filename))
				{
					using (var reader = new StreamReader(filename))
					using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
					{
						var records = csv.GetRecords<T>().ToList();
						return records;
					}
				}
				return new List<T>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new List<T>();
			}
		}
	}
}
