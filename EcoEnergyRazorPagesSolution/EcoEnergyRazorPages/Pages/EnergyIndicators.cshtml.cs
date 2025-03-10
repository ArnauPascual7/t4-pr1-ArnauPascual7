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
        public string MsgFileError;
        public List<EnergyIndicator> EnergyIndicators { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> NetProductionIndicators { get; set; } = new List<EnergyIndicator>();
        public List<EnergyIndicator> GasolineConsumptionIndicators { get; set; } = new List<EnergyIndicator>();
        public List<KeyValuePair<int, float>> AverageNetProductionForEachYearIndicators { get; set; } = new List<KeyValuePair<int, float>>();
        public List<EnergyIndicator> ElectricalDemandAvailableProductionIndicators { get; set; } = new List<EnergyIndicator>();
        public void OnGet()
        {
            string csvFileName = "indicadors_energetics_cat.csv";
            string jsonFileName = "indicadors_energetics_cat.json";
            string csvFilePath = @"ModelData\" + csvFileName;
            string jsonFilePath = @"ModelData\" + jsonFileName;
            if (SysIO.File.Exists(csvFilePath) && SysIO.File.Exists(jsonFilePath))
            {
                string json = SysIO.File.ReadAllText(jsonFilePath);
                EnergyIndicators = FilesHelper.ReadCsv<EnergyIndicator>(csvFilePath);
                if (json != null && json != "")
                {
                    EnergyIndicators.AddRange(JsonSerializer.Deserialize<List<EnergyIndicator>>(json));
                }
                EnergyIndicators.Sort();
                NetProductionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CDEEBC_ProdNeta > 3000)
                    .Select(indicator => indicator)
                    .OrderBy(indicator => indicator.CDEEBC_ProdNeta)
                    .ToList();
                GasolineConsumptionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CDEEBC_ConsumAux > 100)
                    .Select(indicator => indicator)
                    .OrderByDescending(indicator => indicator.CDEEBC_ConsumAux)
                    .ToList();
                AverageNetProductionForEachYearIndicators = EnergyIndicators
                    .GroupBy(indicator => indicator.Data.Year)
                    .ToDictionary(g => g.Key, g => g.Average(indicator => indicator.CDEEBC_ProdNeta))
                    .OrderBy(g => g.Key)
                    .ToList();
                ElectricalDemandAvailableProductionIndicators = EnergyIndicators
                    .Where(indicator => indicator.CDEEBC_DemandaElectr > 4000 && indicator.CDEEBC_ProdDisp < 3000)
                    .Select(indicator => indicator)
                    .ToList();
            }
            else
            {
                MsgFileError = "Error en la c�rrega de dades";
            }
        }
    }
}
