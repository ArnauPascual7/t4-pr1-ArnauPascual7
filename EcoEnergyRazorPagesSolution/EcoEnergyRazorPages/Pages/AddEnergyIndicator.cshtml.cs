using System;
using System.Text.Json;
using EcoEnergyRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddEnergyIndicatorModel : PageModel
    {
        public string MsgFileError;
        [BindProperty]
        public EnergyIndicator NewEnergyIndicator { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string fileName = "indicadors_energetics_cat.json";
            string filePath = @"ModelData\" + fileName;
            if (SysIO.File.Exists(filePath))
            {
                string existingJson = SysIO.File.ReadAllText(filePath);
                List<EnergyIndicator> registers;
                if (!string.IsNullOrEmpty(existingJson))
                {
                    registers = JsonSerializer.Deserialize<List<EnergyIndicator>>(existingJson);
                }
                else
                {
                    registers = new List<EnergyIndicator>();
                }
                registers.Add(NewEnergyIndicator);
                string newJson = JsonSerializer.Serialize(registers);
                SysIO.File.WriteAllText(filePath, newJson);
            }
            else
            {
                MsgFileError = "Error de càrrega de dades";
                return Page();
            }
            return RedirectToPage("EnergyIndicators");
        }

    }
}
