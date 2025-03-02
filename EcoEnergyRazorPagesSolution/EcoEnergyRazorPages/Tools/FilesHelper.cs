using CsvHelper;
using CsvHelper.Configuration;
using EcoEnergyRazorPages.Model;
using System.Diagnostics;
using System.Globalization;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Tools
{
    public static class FilesHelper
    {
        public static List<T> ReadCsv<T>(string filePath)
        {
            Debug.WriteLine("?: CSV File Path --> " + Path.GetFullPath(filePath));
            List<T> registers = new List<T>();
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            registers = csv.GetRecords<T>().ToList();
            return registers;
        }
        public static void WriteCsv<T>(string filePath, T register)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            using var stream = SysIO.File.Open(filePath, FileMode.Append);
            using var writer = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(writer, config);
            csvWriter.WriteRecord(register);
            Debug.WriteLine("?: Register added to " + filePath);
        }
    }
}
