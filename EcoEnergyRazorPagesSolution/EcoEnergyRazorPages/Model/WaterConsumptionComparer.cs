namespace EcoEnergyRazorPages.Model
{
    public class WaterConsumptionComparer : IComparer<WaterConsumption>
    {
        public int Compare(WaterConsumption? x, WaterConsumption? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.CountyCode.CompareTo(y.CountyCode);
        }

        public int HouseholdConsumptionPerCapitaCompare(WaterConsumption? x, WaterConsumption? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return x.HouseholdConsumptionPerCapita.CompareTo(y.HouseholdConsumptionPerCapita);
        }
    }
}
