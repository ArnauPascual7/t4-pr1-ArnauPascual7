using System.ComponentModel.DataAnnotations;

namespace EcoEnergyRazorPages.Model
{
    public abstract class EnergySystem
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public double Ratio { get; }
        public double EnergyGen { get; set; }
        public decimal KWHCost { get; set; }
        public decimal KWHPrice { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPrice { get; set; }

        public EnergySystem(double ratio, decimal kwhCost, decimal kwhPrice)
        {
            Ratio = ratio;
            KWHCost = kwhCost;
            KWHPrice = kwhPrice;
        }

        public abstract void SetSystemEnergyGen();

        public void SetSystemCalculations()
        {
            SetSystemEnergyGen();
            TotalCost = KWHCost * (decimal)EnergyGen;
            TotalPrice = KWHPrice * (decimal)EnergyGen;
        }
    }
}
