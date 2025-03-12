using EcoEnergyRazorPages.Model;

namespace EcoEnergyRazorPages.Tools
{
    public static class WaterConsumptionStatsHelper
    {
        public static List<WaterConsumption> CheckWaterConsumptionMostRecentYearList(List<WaterConsumption> waterConsumptions)
        {
            List<WaterConsumption> mostRecentYearWaterConsumptions = new List<WaterConsumption>();
            int year = CheckWaterConsumptionMostRecentYear(waterConsumptions);
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Year == year)
                {
                    mostRecentYearWaterConsumptions.Add(waterCons);
                }
            }
            return mostRecentYearWaterConsumptions;
        }
        public static int CheckWaterConsumptionMostRecentYear(List<WaterConsumption> waterConsumptions)
        {
            return waterConsumptions.Select(waterCons => waterCons.Year).Max();
        }
        public static List<WaterConsumption> CheckSusWaterConsumptionList(List<WaterConsumption> waterConsumptions, int digits)
        {
            List<WaterConsumption> susWaterConsumption = new List<WaterConsumption>();
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Total.ToString().Length > digits)
                {
                    susWaterConsumption.Add(waterCons);
                }
            }
            return susWaterConsumption;
        }
        public static List<WaterConsumption> CheckWaterConsumptionLast5YearsList(List<WaterConsumption> waterConsumptions)
        {
            List<WaterConsumption> waterConsumptionLast5Years = new List<WaterConsumption>();
            int lastYear = CheckWaterConsumptionMostRecentYear(waterConsumptions);
            foreach (WaterConsumption waterCons in waterConsumptions)
            {
                if (waterCons.Year > lastYear - 5)
                {
                    waterConsumptionLast5Years.Add(waterCons);
                }
            }
            return waterConsumptionLast5Years;
        }
        public static List<WaterConsumption> CheckWaterConsumptionIncreasingTrendList(List<WaterConsumption> waterConsumptionsLast5Years)
        {
            List<WaterConsumption> waterConsumptions = new List<WaterConsumption>();
            for (int i = 1; i <= CheckHigherRegionCode(waterConsumptionsLast5Years); i++)
            {
                int count = 0;
                List<WaterConsumption> regionWaterConsumptions = waterConsumptionsLast5Years
                    .FindAll(waterCons => waterCons.RegionCode == i);
                regionWaterConsumptions.Sort();
                for (int j = 0; j < regionWaterConsumptions.Count - 1; j++)
                {
                    if (regionWaterConsumptions[j].Total < regionWaterConsumptions[j + 1].Total)
                    {
                        count++;
                    }
                }
                if (count >= 4)
                {
                    waterConsumptions.Add(new WaterConsumption
                    {
                        RegionCode = regionWaterConsumptions[0].RegionCode,
                        RegionName = regionWaterConsumptions[0].RegionName
                    });
                }
            }
            return waterConsumptions;
        }
        public static int CheckHigherRegionCode(List<WaterConsumption> waterConsumptions)
        {
            int regionCode = 0;
            for (int i = 0; i < waterConsumptions.Count; i++)
            {
                WaterConsumption waterCons = waterConsumptions[i];
                regionCode = waterCons.RegionCode > regionCode ? waterCons.RegionCode : regionCode;
            }
            return regionCode;
        }
    }
}
