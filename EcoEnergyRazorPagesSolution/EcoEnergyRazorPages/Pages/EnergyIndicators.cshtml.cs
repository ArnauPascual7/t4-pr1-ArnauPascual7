using System.Diagnostics;
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
            string fileName = "indicadors_energetics_cat.csv";
            string filePath = @"ModelData\" + fileName;
            if (SysIO.File.Exists(filePath))
            {
                EnergyIndicators = FilesHelper.ReadCsv<EnergyIndicator>(filePath);
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
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
