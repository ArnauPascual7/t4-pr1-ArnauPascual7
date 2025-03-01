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
            //Debug.WriteLine("Simulations CSV File Path --> " + Path.GetFullPath(filePath));
            if (SysIO.File.Exists(filePath))
            {
                Simulations = FilesHelper.ReadCsv<Simulation>(filePath);
                /*using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                Simulations = csv.GetRecords<Simulation>().ToList();*/
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
    }
}
