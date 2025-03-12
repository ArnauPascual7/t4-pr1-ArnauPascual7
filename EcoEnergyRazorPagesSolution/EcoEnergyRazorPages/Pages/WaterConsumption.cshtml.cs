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
using Stats = EcoEnergyRazorPages.Tools.WaterConsumptionStatsHelper;

namespace EcoEnergyRazorPages.Pages
{
    public class WaterConsumptionModel : PageModel
    {
        public const int NumCountMunicipalitiesWithMoreWater = 10;
        public const int SusDigitsWaterConsumption = 6;
        public string MsgFileError;
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> RegionsWithMoreWaterInLastYear { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> AverageWaterConsumptionByRegion { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> SusWaterConsumption { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> RegionsWithWaterConsumptionIncreasingTrendInLast5Years { get; set; } = new List<WaterConsumption>();
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

                RegionsWithMoreWaterInLastYear = Stats.CheckWaterConsumptionMostRecentYearList(WaterConsumptions);
                RegionsWithMoreWaterInLastYear.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                RegionsWithMoreWaterInLastYear.Reverse();
                if (RegionsWithMoreWaterInLastYear.Count >= NumCountMunicipalitiesWithMoreWater)
                {
                    RegionsWithMoreWaterInLastYear.RemoveRange(NumCountMunicipalitiesWithMoreWater - 1, RegionsWithMoreWaterInLastYear.Count - NumCountMunicipalitiesWithMoreWater);
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

                SusWaterConsumption = Stats.CheckSusWaterConsumptionList(WaterConsumptions, SusDigitsWaterConsumption);
                SusWaterConsumption.Sort(new WaterConsumptionComparer().YearRegionCompare);

                RegionsWithWaterConsumptionIncreasingTrendInLast5Years = Stats.CheckWaterConsumptionLast5YearsList(WaterConsumptions);
                RegionsWithWaterConsumptionIncreasingTrendInLast5Years.Sort(new WaterConsumptionComparer());
                RegionsWithWaterConsumptionIncreasingTrendInLast5Years = Stats.CheckWaterConsumptionIncreasingTrendList(RegionsWithWaterConsumptionIncreasingTrendInLast5Years);
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
