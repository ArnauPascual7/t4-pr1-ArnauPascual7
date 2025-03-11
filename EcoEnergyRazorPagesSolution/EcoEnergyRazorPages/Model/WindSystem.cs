namespace EcoEnergyRazorPages.Model
{
    public class WindSystem : EnergySystem
    {
        public double WindVelocity { get => WindVelocity; private set
            {
                if (value >= 5)
                {
                    WindVelocity = value;
                }
                else
                {
                    throw new ArgumentException("La Velocitat del Vent ha de ser Mínim de 5");
                }
            }
        }
        public WindSystem(double windvelocity, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            WindVelocity = windvelocity;
        }
        public override void SetSystemEnergyGen()
        {
            EnergyGen = WindVelocity * Ratio;
        }
    }
}
