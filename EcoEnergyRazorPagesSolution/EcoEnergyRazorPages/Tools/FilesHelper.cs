using CsvHelper;
using CsvHelper.Configuration;
using EcoEnergyRazorPages.Model;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Tools
{
    public static class FilesHelper
    {
        public static List<T> ReadCsv<T>(string filePath)
        {
            Debug.WriteLine("?: CSV File Path --> " + Path.GetFullPath(filePath));

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            List<T> registers = csv.GetRecords<T>().ToList();
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
        public static List<T> ReadJsonList<T>(string filePath)
        {
            Debug.WriteLine("?: JSON File Path --> " + Path.GetFullPath(filePath));

            string json = SysIO.File.ReadAllText(filePath);
            List<T> registers = new List<T>();

            if (json != null && json != "")
            {
                var jsonIndicators = JsonSerializer.Deserialize<List<T>>(json);

                if (jsonIndicators != null)
                {
                    registers.AddRange(jsonIndicators);
                }
            }
            return registers;
        }
        public static void WriteJson<T>(string filePath, T newRegister)
        {
            string existingJson = SysIO.File.ReadAllText(filePath);
            List<T> registers;

            if (!string.IsNullOrEmpty(existingJson))
            {
                registers = JsonSerializer.Deserialize<List<T>>(existingJson) ?? new List<T>();
            }
            else
            {
                registers = new List<T>();
            }
            registers.Add(newRegister);

            string newJson = JsonSerializer.Serialize(registers);

            SysIO.File.WriteAllText(filePath, newJson);

            Debug.WriteLine("?: Registers added to " + filePath);
        }
        public static List<WaterConsumption> ReadXMLWaterConsumption(string filePath)
        {
            Debug.WriteLine("?: XML File Path --> " + Path.GetFullPath(filePath));

            List<WaterConsumption> waterConsumptions = new List<WaterConsumption>();
            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                if (xmlDoc.Root != null)
                {
                    foreach (XElement element in xmlDoc.Root.Elements())
                    {
                        WaterConsumption waterConsumption = new WaterConsumption
                        {
                            Year = int.Parse(element.Element("Year")?.Value ?? "0"),
                            RegionCode = int.Parse(element.Element("RegionCode")?.Value ?? "0"),
                            RegionName = element.Element("RegionName")?.Value ?? string.Empty,
                            Population = int.Parse(element.Element("Population")?.Value ?? "0"),
                            DomesticNetwork = int.Parse(element.Element("DomesticNetwork")?.Value ?? "0"),
                            EconomicActivitiesOwnSources = int.Parse(element.Element("EconomicActivitiesOwnSources")?.Value ?? "0"),
                            Total = int.Parse(element.Element("Total")?.Value ?? "0"),
                            HouseholdConsumptionPerCapita = float.Parse(element.Element("HouseholdConsumptionPerCapita")?.Value ?? "0")
                        };
                        waterConsumptions.Add(waterConsumption);
                    }
                }
            }
            catch (XmlException ex)
            {
                Debug.WriteLine($"?: Error en la lectura del fitxer XML: {ex}");
                return new List<WaterConsumption>();
            }
            return waterConsumptions;
        }
        public static void WriteXMLWaterConsumption(string filePath, WaterConsumption waterConsumption)
        {
            Debug.WriteLine("?: XML File Path --> " + Path.GetFullPath(filePath));

            XDocument xmlDoc = XDocument.Load(filePath);
            XElement? root = xmlDoc.Root;
            if (root == null)
            {
                root = new XElement("registers");
                xmlDoc.Add(root);
            }
            XDocument newXmlDoc = new XDocument(
                new XElement("reg",
                    new XElement("Year", waterConsumption.Year),
                    new XElement("RegionCode", waterConsumption.RegionCode),
                    new XElement("RegionName", waterConsumption.RegionName),
                    new XElement("Population", waterConsumption.Population),
                    new XElement("DomesticNetwork", waterConsumption.DomesticNetwork),
                    new XElement("EconomicActivitiesOwnSources", waterConsumption.EconomicActivitiesOwnSources),
                    new XElement("Total", waterConsumption.Total),
                    new XElement("HouseholdConsumptionPerCapita", waterConsumption.HouseholdConsumptionPerCapita)
                    )
                );
            root.Add(newXmlDoc.Root);
            xmlDoc.Save(filePath);
        }
    }
}
