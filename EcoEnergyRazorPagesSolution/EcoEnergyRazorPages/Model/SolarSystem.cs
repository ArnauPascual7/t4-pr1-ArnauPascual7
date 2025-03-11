namespace EcoEnergyRazorPages.Model
{
    public class SolarSystem : EnergySystem
    {
        public double SunHours { get => SunHours; private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Les Hores de Sol han de ser Majors a 1");
                }
                else
                {
                    SunHours = value;
                }
            }
        }
        public SolarSystem(double sunhours, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            SunHours = sunhours;
        }
        public override void SetSystemEnergyGen()
        {
            EnergyGen = SunHours * Ratio;
        }
    }
}
