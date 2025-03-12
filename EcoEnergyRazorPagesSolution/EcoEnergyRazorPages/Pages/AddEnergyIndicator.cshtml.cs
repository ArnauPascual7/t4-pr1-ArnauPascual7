using System;
using System.Diagnostics;
using System.Text.Json;
using EcoEnergyRazorPages.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
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
            string readFileName = "indicadors_energetics_defaultvalues.json";
            string readFilePath = @"ModelData\" + readFileName;

            if (SysIO.File.Exists(readFilePath))
            {
                string defaultJson = SysIO.File.ReadAllText(readFilePath);
                EnergyIndicator? deserializedJson = JsonSerializer.Deserialize<EnergyIndicator>(defaultJson);

                if (deserializedJson != null)
                {
                    NewEnergyIndicator = deserializedJson;
                }
                else
                {
                    MsgFileError = "Error de càrrega de dades";
                }
            }
            else
            {
                MsgFileError = "Error de càrrega de dades";
            }
        }
        public IActionResult OnPost()
        {
            string writeFileName = "indicadors_energetics_cat.json";
            string writeFilePath = @"ModelData\" + writeFileName;

            if (SysIO.File.Exists(writeFilePath))
            {
                string existingJson = SysIO.File.ReadAllText(writeFilePath);
                List<EnergyIndicator> registers;

                if (!string.IsNullOrEmpty(existingJson))
                {
                    registers = JsonSerializer.Deserialize<List<EnergyIndicator>>(existingJson) ?? new List<EnergyIndicator>();
                }
                else
                {
                    registers = new List<EnergyIndicator>();
                }
                registers.Add(NewEnergyIndicator);

                string newJson = JsonSerializer.Serialize(registers);

                SysIO.File.WriteAllText(writeFilePath, newJson);
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
