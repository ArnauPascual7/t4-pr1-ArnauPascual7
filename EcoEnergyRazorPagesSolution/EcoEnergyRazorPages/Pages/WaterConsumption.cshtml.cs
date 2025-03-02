using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class WaterConsumptionModel : PageModel
    {
        public string MsgFileError;
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public void OnGet()
        {
            string fileName = "consum_aigua_cat_per_comarques.csv";
            string filePath = @"ModelData\" + fileName;
            if (SysIO.File.Exists(filePath))
            {
                WaterConsumptions = FilesHelper.ReadCsv<WaterConsumption>(filePath);
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
