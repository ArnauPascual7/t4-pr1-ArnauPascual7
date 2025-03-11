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
        public double ConfigPar { get; set; }
        public double Ratio { get; set; }
        public double EnergyGen { get; set; }
        public decimal KWHCost { get; set; }
        public decimal KWHPrice { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
