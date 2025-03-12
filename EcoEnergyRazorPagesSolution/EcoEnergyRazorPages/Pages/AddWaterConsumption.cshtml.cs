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
        public string MsgFileError;
        [BindProperty]
        public WaterConsumption NewWaterConsumption { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewWaterConsumption.SetWaterConsumptionCalculation();

            string fileName = "consum_aigua_cat_per_comarques.xml";
            string filePath = @"ModelData\" + fileName;

            if (SysIO.File.Exists(filePath))
            {
                FilesHelper.WriteXMLWaterConsumption(filePath, NewWaterConsumption);
            }
            else
            {
                MsgFileError = "Error de càrrega de dades";
                return Page();
            }
            return RedirectToPage("WaterConsumption");
        }

    }
}
