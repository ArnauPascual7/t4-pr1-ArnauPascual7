using System.ComponentModel.DataAnnotations;

namespace EcoEnergyRazorPages.Model
{
    public abstract class EnergySystem
    {
        public DateTime Date { get; set; } = DateTime.Now;
        private double _ratio;
        public double Ratio
        {
            get => _ratio;
            set
            {
                if (value > 0 && value <= 3)
                {
                    _ratio = value;
                }
                else
                {
                    throw new ArgumentException("El Rati ha d'estar entre 1 i 3");
                }
            }
        }
        public double EnergyGen { get; set; }
        public decimal KWHCost { get; set; }
        public decimal KWHPrice { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPrice { get; set; }

        public EnergySystem() { }
        public EnergySystem(double ratio, decimal kwhCost, decimal kwhPrice)
        {
            Ratio = ratio;
            KWHCost = kwhCost;
            KWHPrice = kwhPrice;
        }

        public abstract void SetConfigPar(double par);
        public abstract double GetConfigPar();
        public abstract void SetSystemEnergyGen();

        public void SetSystemCalculations()
        {
            SetSystemEnergyGen();
            TotalCost = decimal.Round(KWHCost * (decimal)EnergyGen, 2);
            TotalPrice = decimal.Round(KWHPrice * (decimal)EnergyGen, 2);
        }
    }
}
