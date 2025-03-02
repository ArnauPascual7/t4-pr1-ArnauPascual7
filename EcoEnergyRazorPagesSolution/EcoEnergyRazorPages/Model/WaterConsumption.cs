namespace EcoEnergyRazorPages.Model
{
    public class WaterConsumption
    {
        public int Year { get; set; }
        public int CountyCode { get; set; }
        public string? County { get; set; }
        public int Population { get; set; }
        public int DomesticNetwork { get; set; }
        public int EconomicActivitiesOwnSources { get; set; }
        public int Total { get; set; }
        public float HouseholdConsumptionPerCapita { get; set; }
    }
}
