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
        public void OnGet()
        {
            string fileName = "indicadors_energetics_cat.csv";
            string filePath = @"ModelData\" + fileName;
            if (SysIO.File.Exists(filePath))
            {
                EnergyIndicators = FilesHelper.ReadCsv<EnergyIndicator>(filePath);
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
