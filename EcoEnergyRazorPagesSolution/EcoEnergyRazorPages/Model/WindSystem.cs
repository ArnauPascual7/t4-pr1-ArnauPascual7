namespace EcoEnergyRazorPages.Model
{
    public class WindSystem : EnergySystem
    {
        private double _windvelocity;
        public double WindVelocity
        { 
            get => _windvelocity;
            private set
            {
                if (value >= 5)
                {
                    _windvelocity = value;
                }
                else
                {
                    throw new ArgumentException("La Velocitat del Vent ha de ser Mínim de 5");
                }
            }
        }
        public WindSystem() { }
        public WindSystem(double windvelocity, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            WindVelocity = windvelocity;
        }
        public override void SetConfigPar(double par)
        {
            WindVelocity = par;
        }
        public override double GetConfigPar() => WindVelocity;
        public override void SetSystemEnergyGen()
        {
            EnergyGen = double.Round(Math.Pow(WindVelocity, 3) * Ratio, 2);
        }
    }
}
