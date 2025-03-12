using System.Diagnostics;
using System.Xml.Linq;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddWaterConsumptionModel : PageModel
    {
        public string? MsgFileError { get; set; }
        [BindProperty]
        public WaterConsumption? NewWaterConsumption { get; set; }
        public IActionResult OnPost()
        {
            const string MsgDataError = "Error de càrrega de dades";
            const string FileName = "consum_aigua_cat_per_comarques.xml";
            const string FilePath = @"ModelData\" + FileName;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewWaterConsumption != null)
            {
                NewWaterConsumption.SetWaterConsumptionCalculation();
            }
            else
            {
                MsgFileError = MsgDataError;
                return Page();
            }

            if (SysIO.File.Exists(FilePath))
            {
                FilesHelper.WriteXMLWaterConsumption(FilePath, NewWaterConsumption);
            }
            else
            {
                MsgFileError = MsgDataError;
                return Page();
            }
            return RedirectToPage("WaterConsumption");
        }

    }
}
