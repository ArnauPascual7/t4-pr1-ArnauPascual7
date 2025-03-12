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
        public string? MsgFileError { get; set; }
        public List<Simulation> Simulations { get; set; } = new List<Simulation>();
        public void OnGet()
        {
            const string MsgDataError = "Error de càrrega de dades";
            const string FileName = "simulacions_energia.csv";
            const string FilePath = @"ModelData\" + FileName;

            if (SysIO.File.Exists(FilePath))
            {
                Simulations = FilesHelper.ReadCsv<Simulation>(FilePath);
            }
            else
            {
                MsgFileError = MsgDataError;
            }
        }
    }
}
