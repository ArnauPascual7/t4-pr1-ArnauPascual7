using System;
using System.Diagnostics;
using System.Text.Json;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddEnergyIndicatorModel : PageModel
    {
        const string MsgDataError = "Error de càrrega de dades";

        public string? MsgFileError { get; set; }
        [BindProperty]
        public EnergyIndicator? NewEnergyIndicator { get; set; }
        public void OnGet()
        {
            const string ReadFileName = "indicadors_energetics_defaultvalues.json";
            const string ReadFilePath = @"ModelData\" + ReadFileName;

            if (SysIO.File.Exists(ReadFilePath))
            {
                string defaultJson = SysIO.File.ReadAllText(ReadFilePath);
                EnergyIndicator? deserializedJson = JsonSerializer.Deserialize<EnergyIndicator>(defaultJson);

                if (deserializedJson != null)
                {
                    NewEnergyIndicator = deserializedJson;
                }
                else
                {
                    MsgFileError = MsgDataError;
                }
            }
            else
            {
                MsgFileError = MsgDataError;
            }
        }
        public IActionResult OnPost()
        {
            const string WriteFileName = "indicadors_energetics_cat.json";
            const string WriteFilePath = @"ModelData\" + WriteFileName;

            if (SysIO.File.Exists(WriteFilePath))
            {
                FilesHelper.WriteJson(WriteFilePath, NewEnergyIndicator);
            }
            else
            {
                MsgFileError = MsgDataError;
                return Page();
            }
            return RedirectToPage("EnergyIndicators");
        }

    }
}
