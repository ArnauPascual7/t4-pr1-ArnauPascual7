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
        public const int SusDigitsWaterConsumption = 6;
        public string MsgFileError;
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> MunicipalitiesWithMoreWaterInLastYear { get; set; } = new List<WaterConsumption>();
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
                List<WaterConsumption> xmlWaterConsumptions = FilesHelper.ReadXMLWaterConsumption(xmlFilePath);

                if (xmlWaterConsumptions.Any())
                {
                    WaterConsumptions.AddRange(xmlWaterConsumptions);
                }
                WaterConsumptions.Sort(new WaterConsumptionComparer().YearRegionCompare);

                MunicipalitiesWithMoreWaterInLastYear = CheckWaterConsumptionMostRecentYearList(WaterConsumptions);
                MunicipalitiesWithMoreWaterInLastYear.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                MunicipalitiesWithMoreWaterInLastYear.Reverse();
                if (MunicipalitiesWithMoreWaterInLastYear.Count >= NumCountMunicipalitiesWithMoreWater)
                {
                    MunicipalitiesWithMoreWaterInLastYear.RemoveRange(NumCountMunicipalitiesWithMoreWater - 1, MunicipalitiesWithMoreWaterInLastYear.Count - NumCountMunicipalitiesWithMoreWater);
                }

                AverageWaterConsumptionByRegion = WaterConsumptions
                    .GroupBy(waterCons => waterCons.RegionCode)
                    .Select(g => new WaterConsumption
                    {
                        RegionCode = g.Key,
                        RegionName = g.First().RegionName,
                        HouseholdConsumptionPerCapita = float.Round(g.Average(waterCons => waterCons.HouseholdConsumptionPerCapita), 2)
                    })
                    .OrderByDescending(waterCons => waterCons.HouseholdConsumptionPerCapita)
                    .ToList();

                SusWaterConsumption = CheckSusWaterConsumptionList(WaterConsumptions, SusDigitsWaterConsumption);
                SusWaterConsumption.Sort(new WaterConsumptionComparer().YearRegionCompare);

                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years = CheckWaterConsumptionLast5YearsList(WaterConsumptions);
                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years.Sort(new WaterConsumptionComparer());
                MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years = CheckWaterConsumptionIncreasingTrendList(MunicipalitiesWithWaterConsumptionIncreasingTrendInLast5Years);
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
            return waterConsumptions.Select(waterCons => waterCons.Year).Max();
        }
        public static List<WaterConsumption> CheckSusWaterConsumptionList(List<WaterConsumption> waterConsumptions, int digits)
        {
            List<WaterConsumption> susWaterConsumption = new List<WaterConsumption>();
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Total.ToString().Length > digits)
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
                if (waterCons.Year > lastYear - 5)
                {
                    waterConsumptionLast5Years.Add(waterCons);
                }
            }
            return waterConsumptionLast5Years;
        }
        public static List<WaterConsumption> CheckWaterConsumptionIncreasingTrendList(List<WaterConsumption> waterConsumptionsLast5Years)
        {
            List<WaterConsumption> waterConsumptions = new List<WaterConsumption>();
            for (int i = 1; i <= CheckHigherRegionCode(waterConsumptionsLast5Years); i++)
            {
                int count = 0;
                List<WaterConsumption> regionWaterConsumptions = waterConsumptionsLast5Years
                    .FindAll(waterCons => waterCons.RegionCode == i);
                regionWaterConsumptions.Sort();
                for (int j = 0; j < regionWaterConsumptions.Count - 1; j++)
                {
                    if (regionWaterConsumptions[j].Total < regionWaterConsumptions[j + 1].Total)
                    {
                        count++;
                    }
                }
                if (count >= 4)
                {
                    waterConsumptions.Add(new WaterConsumption
                    {
                        RegionCode = regionWaterConsumptions[0].RegionCode,
                        RegionName = regionWaterConsumptions[0].RegionName
                    });
                }
            }
            return waterConsumptions;
        }
        public static int CheckHigherRegionCode(List<WaterConsumption> waterConsumptions)
        {
            int regionCode = 0;
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                regionCode = waterCons.RegionCode > regionCode ? waterCons.RegionCode : regionCode;
            }
            return regionCode;
        }
    }
}
