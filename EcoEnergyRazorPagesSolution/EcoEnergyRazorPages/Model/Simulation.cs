using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public double ConfigPar { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        [Range(0,3, ErrorMessage = "El camp Rati ha de ser entre 0 i 3")]
        public double Ratio { get; set; }
        public double EnergyGen { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
        public decimal KWHCost { get; set; }
        [Required(ErrorMessage = "Aquest camp és obligatori")]
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

        public bool ValidConfigPar()
        {
            switch (SysType)
            {
                case SystemType.SolarSystem:
                    return ConfigPar > 1;
                case SystemType.WindSystem:
                    return ConfigPar >= 5;
                case SystemType.HydroelectricSystem:
                    return ConfigPar >= 20;
                default:
                    return false;
            }
        }

        public override string ToString()
        {
            return $"{Date}|{SysType}|{ConfigPar}|{Ratio}|{EnergyGen}|{KWHCost}|{KWHPrice}|{TotalCost}|{TotalPrice}";
        }
    }
}
