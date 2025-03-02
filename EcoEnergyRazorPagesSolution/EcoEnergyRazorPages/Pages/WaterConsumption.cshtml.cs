using System.Collections.Immutable;
using System.Diagnostics;
using EcoEnergyRazorPages.Model;
using EcoEnergyRazorPages.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SysIO = System.IO;

namespace EcoEnergyRazorPages.Pages
{
    public class WaterConsumptionModel : PageModel
    {
        public const int NumListMunicipalitiesWithMoreWater = 10;
        public const int SusDigitsWaterConsumption = 3;
        public string MsgFileError;
        public List<WaterConsumption> WaterConsumptions { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> MunicipalitiesWithMoreWater { get; set; } = new List<WaterConsumption>();
        /*public List<WaterConsumption> AverageWaterConsumptionByRegion { get; set; } = new List<WaterConsumption>();
        public List<WaterConsumption> SusWaterConsumption { get; set; } = new List<WaterConsumption>();*/
        public void OnGet()
        {
            string fileName = "consum_aigua_cat_per_comarques.csv";
            string filePath = @"ModelData\" + fileName;
            if (SysIO.File.Exists(filePath))
            {
                WaterConsumptions = FilesHelper.ReadCsv<WaterConsumption>(filePath);
                MunicipalitiesWithMoreWater = CheckWaterConsumptioMostRecentYear(WaterConsumptions);
                MunicipalitiesWithMoreWater.Sort(new WaterConsumptionComparer().HouseholdConsumptionPerCapitaCompare);
                MunicipalitiesWithMoreWater.Reverse();
                MunicipalitiesWithMoreWater.RemoveRange(NumListMunicipalitiesWithMoreWater - 1, MunicipalitiesWithMoreWater.Count - NumListMunicipalitiesWithMoreWater);
                /*SusWaterConsumption = CheckSusWaterConsumption(WaterConsumptions, SusDigitsWaterConsumption);*/
            }
            else
            {
                MsgFileError = "Error en la càrrega de dades";
            }
        }
        public static List<WaterConsumption> CheckWaterConsumptioMostRecentYear(List<WaterConsumption> waterConsumptions)
        {
            List<WaterConsumption> MostRecentYearWaterConsumptions = new List<WaterConsumption>();
            int year = 0;
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                year = waterCons.Year > year ? waterCons.Year : year;
            }
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Year == year)
                {
                    MostRecentYearWaterConsumptions.Add(waterCons);
                }
            }
            return MostRecentYearWaterConsumptions;
        }
        /*public static List<WaterConsumption> CheckSusWaterConsumption(List<WaterConsumption> waterConsumptions, int digits)
        {
            List<WaterConsumption> SusWaterConsumption = new List<WaterConsumption>();
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (Convert.ToInt32(waterCons.HouseholdConsumptionPerCapita).ToString().Length > digits)
                {
                    SusWaterConsumption.Add(waterCons);
                }
            }
            return SusWaterConsumption;
        }*/
    }
}
