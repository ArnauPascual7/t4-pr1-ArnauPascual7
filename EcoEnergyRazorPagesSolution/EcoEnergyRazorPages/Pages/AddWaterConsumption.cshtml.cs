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
            string fileName = "consum_aigua_cat_per_comarques.xml";
            string filePath = @"ModelData\" + fileName;
            NewWaterConsumption.SetWaterConsumptionCalculation();
            if (SysIO.File.Exists(filePath))
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                XElement root = xmlDoc.Root;
                XDocument newXmlDoc = new XDocument(
                    new XElement("reg", 
                        new XElement("Year", NewWaterConsumption.Year),
                        new XElement("CountyCode", NewWaterConsumption.CountyCode),
                        new XElement("County", NewWaterConsumption.County),
                        new XElement("Population", NewWaterConsumption.Population),
                        new XElement("DomesticNetwork", NewWaterConsumption.DomesticNetwork),
                        new XElement("EconomicActivitiesOwnSources", NewWaterConsumption.EconomicActivitiesOwnSources),
                        new XElement("Total", NewWaterConsumption.Total),
                        new XElement("HouseholdConsumptionPerCapita", NewWaterConsumption.HouseholdConsumptionPerCapita)
                        )
                    );
                root.Add(newXmlDoc.Root);
                xmlDoc.Save(filePath);
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
