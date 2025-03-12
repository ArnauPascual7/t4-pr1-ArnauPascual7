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
        public string? MsgFileError { get; set; }
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> RegionsWithMoreWaterInLastYear { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> AverageWaterConsumptionByRegion { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> SusWaterConsumption { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> RegionsWithWaterConsumptionIncreasingTrendInLast5Years { get; set; } = new List<WaterConsumption>();
        public void OnGet()
        {
            const string MsgDataError = "Error de càrrega de dades";
            const string CsvFileName = "consum_aigua_cat_per_comarques.csv";
            const string XmlFileName = "consum_aigua_cat_per_comarques.xml";
            const string CsvFilePath = @"ModelData\" + CsvFileName;
            const string XmlFilePath = @"ModelData\" + XmlFileName;
            const int NumCountRegionsWithMoreWater = 10;
            const int SusDigitsWaterConsumption = 6;

            if (SysIO.File.Exists(CsvFilePath) && SysIO.File.Exists(XmlFilePath))
            {
                WaterConsumptions = FilesHelper.ReadCsv<WaterConsumption>(CsvFilePath);
                List<WaterConsumption> xmlWaterConsumptions = FilesHelper.ReadXMLWaterConsumption(XmlFilePath);

                if (xmlWaterConsumptions.Any())
                {
                    WaterConsumptions.AddRange(xmlWaterConsumptions);
                }
                WaterConsumptions.Sort(new WaterConsumptionComparer().YearRegionCompare);

                RegionsWithMoreWaterInLastYear = Stats.CheckWaterConsumptionMostRecentYearList(WaterConsumptions);
                RegionsWithMoreWaterInLastYear.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                RegionsWithMoreWaterInLastYear.Reverse();
                if (RegionsWithMoreWaterInLastYear.Count >= NumCountRegionsWithMoreWater)
                {
                    RegionsWithMoreWaterInLastYear.RemoveRange(NumCountRegionsWithMoreWater - 1, RegionsWithMoreWaterInLastYear.Count - NumCountRegionsWithMoreWater);
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
                MsgFileError = MsgDataError;
            }
        }
    }
}
