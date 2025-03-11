namespace EcoEnergyRazorPages.Model
{
    public class HydroelectricSystem : EnergySystem
    {
        public double WaterFlow { get => WaterFlow; private set
            {
                if (value >= 20)
                {
                    WaterFlow = value;
                }
                else
                {
                    throw new ArgumentException("El Cabal d'aigua ha de ser Mínim 20");
                }
            }
        }
        public HydroelectricSystem(double waterflow, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            WaterFlow = waterflow;
        }
        public override void SetSystemEnergyGen()
        {
            EnergyGen = WaterFlow * Ratio;
        }
    }
}
