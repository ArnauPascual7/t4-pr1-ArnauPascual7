using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class WaterConsumptionModel : PageModel
    {
        public const int NumCountMunicipalitiesWithMoreWater = 10;
        public const int SusDigitsWaterConsumption = 8;
        public string MsgFileError;
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> MunicipalitiesWithMoreWater { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> AverageWaterConsumptionByRegion { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> SusWaterConsumption { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years { get; set; } = new List<WaterConsumption>();
        public void OnGet()
        {
            string csvFileName = "consum_aigua_cat_per_comarques.csv";
            string xmlFileName = "consum_aigua_cat_per_comarques.xml";
            string csvFilePath = @"ModelData\" + csvFileName;
            string xmlFilePath = @"ModelData\" + xmlFileName;
            if (SysIO.File.Exists(csvFilePath) && SysIO.File.Exists(xmlFilePath))
            {
                WaterConsumptions = FilesHelper.ReadCsv<WaterConsumption>(csvFilePath);
                try
                {
                    XDocument xmlDoc = XDocument.Load(xmlFilePath);
                    foreach (XElement element in xmlDoc.Root.Elements())
                    {
                        WaterConsumption waterConsumption = new WaterConsumption
                        {
                            Year = int.Parse(element.Element("Year").Value),
                            CountyCode = int.Parse(element.Element("CountyCode").Value),
                            County = element.Element("County").Value,
                            Population = int.Parse(element.Element("Population").Value),
                            DomesticNetwork = int.Parse(element.Element("DomesticNetwork").Value),
                            EconomicActivitiesOwnSources = int.Parse(element.Element("EconomicActivitiesOwnSources").Value),
                            Total = int.Parse(element.Element("Total").Value),
                            HouseholdConsumptionPerCapita = float.Parse(element.Element("HouseholdConsumptionPerCapita").Value)
                        };
                        WaterConsumptions.Add(waterConsumption);
                    }
                }
                catch (XmlException ex)
                {
                    Debug.WriteLine($"?: Error en la lectura del fitxer XML: {ex}");
                }
                WaterConsumptions.Sort();

                MunicipalitiesWithMoreWater = CheckWaterConsumptionMostRecentYearList(WaterConsumptions);
                MunicipalitiesWithMoreWater.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                MunicipalitiesWithMoreWater.Reverse();
                if (MunicipalitiesWithMoreWater.Count >= NumCountMunicipalitiesWithMoreWater)
                {
                    MunicipalitiesWithMoreWater.RemoveRange(NumCountMunicipalitiesWithMoreWater - 1, MunicipalitiesWithMoreWater.Count - NumCountMunicipalitiesWithMoreWater);
                }

                AverageWaterConsumptionByRegion = WaterConsumptions
                    .GroupBy(waterCons => waterCons.CountyCode)
                    .Select(g => new WaterConsumption
                    {
                        CountyCode = g.Key,
                        County = g.First().County,
                        HouseholdConsumptionPerCapita = float.Round(g.Average(waterCons => waterCons.HouseholdConsumptionPerCapita), 2)
                    })
                    .ToList();
                AverageWaterConsumptionByRegion.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                AverageWaterConsumptionByRegion.Reverse();

                SusWaterConsumption = CheckSusWaterConsumptionList(WaterConsumptions, SusDigitsWaterConsumption);

                /*MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years = CheckWaterConsumptionLast5YearsList(WaterConsumptions);
                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years.Sort();
                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years = MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years
                    .GroupBy(waterCons => waterCons.CountyCode)
                    .Select(g => new WaterConsumption
                    {
                        CountyCode = g.Key,
                        County = g.First().County,
                        Total = g.First().Total - g.Last().Total
                    })
                    .Where(waterCons => waterCons.Total > 0)
                    .ToList();
                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years.Sort(new WaterConsumptionComparer());*/
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
        public static List<WaterConsumption> CheckWaterConsumptionMostRecentYearList(List<WaterConsumption> waterConsumptions)
        {
            List<WaterConsumption> mostRecentYearWaterConsumptions = new List<WaterConsumption>();
            int year = CheckWaterConsumptionMostRecentYear(waterConsumptions);
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Year == year)
                {
                    mostRecentYearWaterConsumptions.Add(waterCons);
                }
            }
            return mostRecentYearWaterConsumptions;
        }
        public static int CheckWaterConsumptionMostRecentYear(List<WaterConsumption> waterConsumptions)
        {
            int year = 0;
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                year = waterCons.Year > year ? waterCons.Year : year;
            }
            return year;
        }
        public static List<WaterConsumption> CheckSusWaterConsumptionList(List<WaterConsumption> waterConsumptions, int digits)
        {
            List<WaterConsumption> susWaterConsumption = new List<WaterConsumption>();
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (Convert.ToInt32(waterCons.Total).ToString().Length > digits)
                {
                    susWaterConsumption.Add(waterCons);
                }
            }
            return susWaterConsumption;
        }
        public static List<WaterConsumption> CheckWaterConsumptionLast5YearsList(List<WaterConsumption> waterConsumptions)
        {
            List<WaterConsumption> waterConsumptionLast5Years = new List<WaterConsumption>();
            int lastYear = CheckWaterConsumptionMostRecentYear(waterConsumptions);
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Year >= lastYear - 5)
                {
                    waterConsumptionLast5Years.Add(waterCons);
                }
            }
            return waterConsumptionLast5Years;
        }
    }
}
