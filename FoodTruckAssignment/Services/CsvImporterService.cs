using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace FoodTruckAssignment.Services
{
    public class CsvImporterService: ICsvImporterService
    {
        public List<Dictionary<string, string>> ReadCsvFileToDictionaryList(string csv_file_path)
        {
            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>();
            string[] fieldNames = null;
            using TextFieldParser csvReader = new TextFieldParser(csv_file_path);
            bool firstLine = true;
            csvReader.SetDelimiters(new string[] { "," });
            csvReader.HasFieldsEnclosedInQuotes = true;
            while (!csvReader.EndOfData)
            {
                string[] fields = csvReader.ReadFields();
                // Discard the header line
                if (firstLine)
                {
                    fieldNames = fields;
                    firstLine = false;
                    continue;
                }
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    keyValuePairs[fieldNames[i]] = fields[i];
                }
                csvData.Add(keyValuePairs);
            }
            return csvData;
        }

    }
}
