using EcoEnergyRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddSimulationModel : PageModel
    {
        public string MsgFileError;
        public Simulation NewSimulation { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string fileName = "simulacions_energia.csv";
            string filePath = @"ModelData\" + fileName;
            Debug.WriteLine("Simulations CSV File Path --> " + Path.GetFullPath(filePath));
            /*if (SysIO.File.Exists(filePath))
            {
                SysIO.File.AppendAllText(filePath, NewSimulation.ToString() + Environment.NewLine);
            }
            else
            {
                MsgFileError = "Error de càrrega de dades";
            }*/
            return RedirectToPage("Simulations");
        }
    }
}
