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
        public string? MsgFileError { get; set; }
        public string? MsgParError { get; set; }
        public string? MsgRatioError { get; set; }
        public EnergySystem NewSystem { get; set; } = new SolarSystem();
        public IActionResult OnPost(string systemtype, double configpar, double ratio, decimal cost, decimal price)
        {
            const string MsgSelectedSystemError = "Error: El sistema seleccionat és erroni";
            const string MsgDataError = "Error de càrrega de dades";
            const string FileName = "simulacions_energia.csv";
            const string FilePath = @"ModelData\" + FileName;

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
                    MsgParError = MsgSelectedSystemError;
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

            if (SysIO.File.Exists(FilePath))
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

                FilesHelper.WriteCsv(FilePath, new Simulation
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
                MsgFileError = MsgDataError;
                return Page();
            }
            SysIO.File.AppendAllText(FilePath, Environment.NewLine);
            return RedirectToPage("Simulations");
        }
    }
}
