using System.ComponentModel.DataAnnotations;

namespace EcoEnergyRazorPages.Model
{
    public class WaterConsumption : IComparable<WaterConsumption>
    {
        public int Year { get; set; } = DateTime.Now.Year;
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int CountyCode { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public string? County { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int Population { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int DomesticNetwork { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public int EconomicActivitiesOwnSources { get; set; }
        public int Total { get; set; }
        public float HouseholdConsumptionPerCapita { get; set; }

        public void SetWaterConsumptionCalculation()
        {
            Total = DomesticNetwork + EconomicActivitiesOwnSources;
            HouseholdConsumptionPerCapita = float.Round((float)Total / Population, 2);
        }
        public int CompareTo(WaterConsumption other)
        {
            return Year.CompareTo(other.Year);
        }
    }
}
