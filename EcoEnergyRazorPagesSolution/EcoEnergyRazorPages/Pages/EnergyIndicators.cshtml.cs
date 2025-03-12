using System.Diagnostics;
using System.Text.Json;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class EnergyIndicatorsModel : PageModel
    {
        public string? MsgFileError { get; set; }
        public List<EnergyIndicator> EnergyIndicators { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> NetProductionIndicators { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> GasolineConsumptionIndicators { get; set; } = new List<EnergyIndicator>();
        public List<KeyValuePair<int, float>> AverageNetProductionForEachYearIndicators { get; set; } = new List<KeyValuePair<int, float>>();
        public List<EnergyIndicator> ElectricalDemandAvailableProductionIndicators { get; set; } = new List<EnergyIndicator>();
        public void OnGet()
        {
            const string MsgDataError = "Error de càrrega de dades";
            const string CsvFileName = "indicadors_energetics_cat.csv";
            const string JsonFileName = "indicadors_energetics_cat.json";
            const string CsvFilePath = @"ModelData\" + CsvFileName;
            const string JsonFilePath = @"ModelData\" + JsonFileName;
            const int MinNetProduction = 3000;
            const int MinGasolineConsumption = 100;
            const int MinElectricalDemand = 4000;
            const int MaxAvailableProduction = 3000;

            if (SysIO.File.Exists(CsvFilePath) && SysIO.File.Exists(JsonFilePath))
            {
                EnergyIndicators = FilesHelper.ReadCsv<EnergyIndicator>(CsvFilePath);
                List<EnergyIndicator> jsonEnergyIndicators = FilesHelper.ReadJsonList<EnergyIndicator>(JsonFilePath);

                if (jsonEnergyIndicators.Any())
                {
                    EnergyIndicators.AddRange(jsonEnergyIndicators);
                }
                EnergyIndicators.Sort();

                NetProductionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CDEEBC_ProdNeta > MinNetProduction)
                    .Select(indicator => indicator)
                    .OrderBy(indicator => indicator.CDEEBC_ProdNeta)
                    .ToList();

                GasolineConsumptionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CCAC_GasolinaAuto > MinGasolineConsumption)
                    .Select(indicator => indicator)
                    .OrderByDescending(indicator => indicator.CCAC_GasolinaAuto)
                    .ToList();

                AverageNetProductionForEachYearIndicators = EnergyIndicators
                    .GroupBy(indicator => indicator.Data.Year)
                    .ToDictionary(g => g.Key, g => g.Average(indicator => indicator.CDEEBC_ProdNeta))
                    .OrderBy(g => g.Key)
                    .ToList();

                ElectricalDemandAvailableProductionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CDEEBC_DemandaElectr > MinElectricalDemand && indicator.CDEEBC_ProdDisp < MaxAvailableProduction)
                    .Select(indicator => indicator)
                    .ToList();
            }
            else
            {
                MsgFileError = MsgDataError;
            }
        }
    }
}
