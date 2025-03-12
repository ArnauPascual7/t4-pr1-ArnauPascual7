using CsvHelper;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Globalization;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class SimulationsModel : PageModel
    {
        public string MsgFileError;
        public List<Simulation> Simulations { get; set; } = new List<Simulation>();
        public void OnGet()
        {
            string fileName = "simulacions_energia.csv";
            string filePath = @"ModelData\" + fileName;

            if (SysIO.File.Exists(filePath))
            {
                Simulations = FilesHelper.ReadCsv<Simulation>(filePath);
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
