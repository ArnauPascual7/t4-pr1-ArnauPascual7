using CsvHelper;
using CsvHelper.Configuration;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Globalization;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddSimulationModel : PageModel
    {
        public string MsgFileError;
        public string MsgConfigParError;
        [BindProperty]
        public Simulation NewSimulation { get; set; }
        public List<SelectListItem> Systems { get; set; } =
            Enum.GetValues(typeof(SystemType)).Cast<SystemType>().Select(v => new SelectListItem(v.ToString(), v.ToString())).ToList();
        public void OnGet()
        {
        }
        public IActionResult OnPost(string systemtype)
        {
            Debug.WriteLine("Simulation System Type --> " + systemtype);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string fileName = "simulacions_energia.csv";
            string filePath = @"ModelData\" + fileName;
            //Debug.WriteLine("Simulations CSV File Path --> " + Path.GetFullPath(filePath));
            NewSimulation.SetSystemType(systemtype);
            if (!NewSimulation.ValidConfigPar())
            {
                switch(NewSimulation.SysType)
                {
                    case SystemType.SolarSystem:
                        MsgConfigParError = "El paràmetre configurat ha de ser major que 1";
                        break;
                    case SystemType.WindSystem:
                        MsgConfigParError = "El paràmetre configurat ha de ser mínim 5";
                        break;
                    case SystemType.HydroelectricSystem:
                        MsgConfigParError = "El paràmetre configurat ha de ser mínim 20";
                        break;
                    default:
                        MsgConfigParError = "El paràmetre configurat no és correcte";
                        break;
                }
                return Page();
            }
            NewSimulation.SetSimulationCalculations();
            Debug.WriteLine("?: New Simulation --> " + NewSimulation.ToString());
            if (SysIO.File.Exists(filePath))
            {
                FilesHelper.WriteCsv(filePath, NewSimulation);
                /*var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };
                using var stream = SysIO.File.Open(filePath, FileMode.Append);
                using var writer = new StreamWriter(stream);
                using var csvWriter = new CsvWriter(writer, config);
                csvWriter.WriteRecord(NewSimulation);*/
            }
            else
            {
                MsgFileError = "Error de càrrega de dades";
                return Page();
            }
            SysIO.File.AppendAllText(filePath, Environment.NewLine);
            return RedirectToPage("Simulations");
        }
    }
}
