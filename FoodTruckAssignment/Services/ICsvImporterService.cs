using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckAssignment.Services
{
    public interface ICsvImporterService
    {
        List<Dictionary<string, string>> ReadCsvFileToDictionaryList(string csv_file_path);
    }
}
