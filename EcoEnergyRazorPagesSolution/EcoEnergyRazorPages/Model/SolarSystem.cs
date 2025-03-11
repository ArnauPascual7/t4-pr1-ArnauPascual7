namespace EcoEnergyRazorPages.Model
{
    public class SolarSystem : EnergySystem
    {
        private double _sunhours;
        public double SunHours
        {
            get => _sunhours;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Les Hores de Sol han de ser Majors a 1");
                }
                else
                {
                    _sunhours = value;
                }
            }
        }
        public SolarSystem() { }
        public SolarSystem(double sunhours, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            SunHours = sunhours;
        }
        public override void SetConfigPar(double par)
        {
            SunHours = par;
        }
        public override double GetConfigPar() => SunHours;
        public override void SetSystemEnergyGen()
        {
            EnergyGen = double.Round(SunHours * Ratio, 2);
        }
    }
}
