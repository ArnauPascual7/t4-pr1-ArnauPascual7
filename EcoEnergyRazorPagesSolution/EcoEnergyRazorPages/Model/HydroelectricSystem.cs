namespace EcoEnergyRazorPages.Model
{
    public class HydroelectricSystem : EnergySystem
    {
        private double _waterflow;
        public double WaterFlow
        {
            get => _waterflow;
            private set
            {
                const string Msg = "El Cabal d'aigua ha de ser Mínim 20";
                if (value >= 20)
                {
                    _waterflow = value;
                }
                else
                {
                    throw new ArgumentException(Msg);
                }
            }
        }
        public HydroelectricSystem() { }
        public HydroelectricSystem(double waterflow, double ratio, decimal kwhCost, decimal kwhPrice)
            : base(ratio, kwhCost, kwhPrice)
        {
            WaterFlow = waterflow;
        }
        public override void SetConfigPar(double par)
        {
            WaterFlow = par;
        }
        public override double GetConfigPar() => WaterFlow;
        public override void SetSystemEnergyGen()
        {
            EnergyGen = double.Round(WaterFlow * 9.8 * Ratio, 2);
        }
    }
}
