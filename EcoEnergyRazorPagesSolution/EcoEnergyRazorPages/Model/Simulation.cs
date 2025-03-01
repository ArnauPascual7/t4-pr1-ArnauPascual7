namespace EcoEnergyRazorPages.Model
{
    public enum SystemType
    {
        SolarSystem = 1, WindSystem = 2, HydroelectricSystem = 3
    }
    public class Simulation
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public SystemType SysType { get; set; }
        public double ConfigPar { get; set; }
        public double Ratio { get; set; }
        public double EnergyGen { get; set; }
        public decimal KWHCost { get; set; }
        public decimal KWHPrice { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPrice { get; set; }

        public void SetSimulationCalculations()
        {
            switch(SysType)
            {
                case SystemType.SolarSystem:
                    EnergyGen = ConfigPar * Ratio;
                    break;
                case SystemType.WindSystem:
                    EnergyGen = Math.Pow(ConfigPar, 3) * Ratio;
                    break;
                case SystemType.HydroelectricSystem:
                    EnergyGen = ConfigPar * 9.8 * Ratio;
                    break;
                default:
                    EnergyGen = 0;
                    break;
            }
            /*if (SysType is SystemType.SolarSystem)
            {
                EnergyGen = ConfigPar * Ratio;
            }
            else if (SysType is SystemType.WindSystem)
            {
                EnergyGen = Math.Pow(ConfigPar, 3) * Ratio;
            }
            else if (SysType is SystemType.HydroelectricSystem)
            {
                EnergyGen = ConfigPar * 9.8 * Ratio;
            }
            else
            {
                EnergyGen = 0;
            }*/
            TotalCost = KWHCost * (decimal)EnergyGen;
            TotalPrice = KWHPrice * (decimal)EnergyGen;
        }

        public void SetSystemType(string system)
        {
            switch(system)
            {
                case "SolarSystem":
                    SysType = SystemType.SolarSystem;
                    break;
                case "WindSystem":
                    SysType = SystemType.WindSystem;
                    break;
                case "HydroelectricSystem":
                    SysType = SystemType.HydroelectricSystem;
                    break;
                default:
                    SysType = 0;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Date}|{SysType}|{ConfigPar}|{Ratio}|{EnergyGen}|{KWHCost}|{KWHPrice}|{TotalCost}|{TotalPrice}";
        }
    }
}
