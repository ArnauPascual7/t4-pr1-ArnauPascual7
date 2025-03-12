using CsvHelper;
using CsvHelper.Configuration;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class AddSimulationModel : PageModel
    {
        public string MsgFileError;
        public string MsgParError;
        public string MsgRatioError;
        public EnergySystem NewSystem { get; set; } = new SolarSystem();
        public IActionResult OnPost(string systemtype, double configpar, double ratio, decimal cost, decimal price)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            switch (systemtype)
            {
                case "1":
                    NewSystem = new SolarSystem();
                    break;
                case "2":
                    NewSystem = new WindSystem();
                    break;
                case "3":
                    NewSystem = new HydroelectricSystem();
                    break;
                default:
                    MsgParError = "Error: El sistema seleccionat és erroni";
                    return Page();
            }
            Debug.WriteLine("?: New System --> " + NewSystem.GetType());

            try
            {
                NewSystem.SetConfigPar(configpar);
            }
            catch (System.Exception ex)
            {
                MsgParError = ex.Message;
                return Page();
            }

            try
            {
                NewSystem.Ratio = ratio;
            }
            catch (System.Exception ex)
            {
                MsgRatioError = ex.Message;
                return Page();
            }

            NewSystem.KWHCost = cost;
            NewSystem.KWHPrice = price;
            NewSystem.SetSystemCalculations();

            string fileName = "simulacions_energia.csv";
            string filePath = @"ModelData\" + fileName;

            if (SysIO.File.Exists(filePath))
            {
                SystemType sysType;
                if (NewSystem.GetType() == typeof(SolarSystem))
                {
                    sysType = SystemType.SolarSystem;
                }
                else if (NewSystem.GetType() == typeof(WindSystem))
                {
                    sysType = SystemType.WindSystem;
                }
                else if (NewSystem.GetType() == typeof(HydroelectricSystem))
                {
                    sysType = SystemType.HydroelectricSystem;
                }
                else
                {
                    sysType = 0;
                }

                FilesHelper.WriteCsv(filePath, new Simulation
                {
                    SysType = sysType,
                    ConfigPar = NewSystem.GetConfigPar(),
                    Ratio = NewSystem.Ratio,
                    EnergyGen = NewSystem.EnergyGen,
                    KWHCost = NewSystem.KWHCost,
                    KWHPrice = NewSystem.KWHPrice,
                    TotalCost = NewSystem.TotalCost,
                    TotalPrice = NewSystem.TotalPrice
                });
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
