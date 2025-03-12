using EcoEnergyRazorPages.Model;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace xUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void EnergyIndicatorGetDateMonthYearOnlyTrue()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            string result = indicator.GetDateMonthYearOnly();
            string exp = "02/2021";

            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorGetDateMonthYearOnlyFalse()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 1, 1)
            };
            string result = indicator.GetDateMonthYearOnly();
            string exp = "1/1/2021";

            //Assert
            Assert.NotEqual(exp, result);
        }
        [Fact]
        public void EnergyIndicatorCompareToTrue()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            int result = indicator.CompareTo(indicator2);
            int exp = 0;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorCompareToFalse()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = new EnergyIndicator
            {
                Data = new DateOnly(2021, 1, 1)
            };
            int result = indicator.CompareTo(indicator2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorCompareToNull()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = null;
            int result = indicator.CompareTo(indicator2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorEqualsTrue()
        {
            // Arrange & Act
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            bool result = indicator.Equals(indicator2);
            bool exp = true;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorEqualsFalse()
        {
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = new EnergyIndicator
            {
                Data = new DateOnly(2021, 1, 1)
            };
            bool result = indicator.Equals(indicator2);
            bool exp = false;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void EnergyIndicatorEqualsNull()
        {
            EnergyIndicator indicator = new EnergyIndicator
            {
                Data = new DateOnly(2021, 2, 1)
            };
            EnergyIndicator indicator2 = null;
            bool result = indicator.Equals(indicator2);
            bool exp = false;
            //Assert
            Assert.Equal(exp, result);
        }
        [Theory]
        [InlineData(20)]
        [InlineData(30)]
        public void HydroelectricSystemWaterFlowTrue(double par)
        {
            // Arrange & Act
            HydroelectricSystem hydro = new HydroelectricSystem();
            hydro.SetConfigPar(par);
            double result = hydro.WaterFlow;
            double exp = par;
            //Assert
            Assert.Equal(exp, result);
        }
        [Theory]
        [InlineData(10)]
        [InlineData(19.9)]
        public void HydroelectricSystemWaterFlowFalse(double par)
        {
            // Arrange & Act
            HydroelectricSystem hydro = new HydroelectricSystem();
            try
            {
                hydro.SetConfigPar(par);
            }
            catch (ArgumentException)
            {
                // ignored
            }
            double result = hydro.WaterFlow;
            double exp = par;
            //Assert
            Assert.NotEqual(exp, result);
        }
        [Theory]
        [InlineData(1.1)]
        [InlineData(10)]
        public void SolarSystemWaterSunHoursTrue(double par)
        {
            // Arrange & Act
            SolarSystem solar = new SolarSystem();
            solar.SetConfigPar(par);
            double result = solar.SunHours;
            double exp = par;
            //Assert
            Assert.Equal(exp, result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void SolarSystemSunHoursFalse(double par)
        {
            // Arrange & Act
            SolarSystem solar = new SolarSystem();
            try
            {
                solar.SetConfigPar(par);
            }
            catch (ArgumentException)
            {
                // ignored
            }
            double result = solar.SunHours;
            double exp = par;
            //Assert
            Assert.NotEqual(exp, result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        public void WindSystemWindVelocityTrue(double par)
        {
            // Arrange & Act
            WindSystem wind = new WindSystem();
            wind.SetConfigPar(par);
            double result = wind.WindVelocity;
            double exp = par;
            //Assert
            Assert.Equal(exp, result);
        }
        [Theory]
        [InlineData(4.9)]
        [InlineData(-1)]
        public void WindSystemWindVelocityFalse(double par)
        {
            // Arrange & Act
            WindSystem wind = new WindSystem();
            try
            {
                wind.SetConfigPar(par);
            }
            catch (ArgumentException)
            {
                // ignored
            }
            double result = wind.WindVelocity;
            double exp = par;
            //Assert
            Assert.NotEqual(exp, result);
        }
        [Fact]
        public void WaterConsumptionCompareToTrue()
        {
            // Arrange & Act
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021
            };
            int result = water.CompareTo(water2);
            int exp = 0;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionCompareToFalse()
        {
            // Arrange & Act
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2020
            };
            int result = water.CompareTo(water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionEqualsTrue()
        {
            // Arrange & Act
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            bool result = water.Equals(water2);
            bool exp = true;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionEqualsFalse()
        {
            // Arrange & Act
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 2
            };
            bool result = water.Equals(water2);
            bool exp = false;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionEqualsNull()
        {
            // Arrange & Act
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = null;
            bool result = water.Equals(water2);
            bool exp = false;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerCompareTrue()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021
            };
            int result = comparer.Compare(water, water2);
            int exp = 0;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerCompareFalse()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2020
            };
            int result = comparer.Compare(water, water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerCompareNull()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021
            };
            WaterConsumption water2 = null;
            int result = comparer.Compare(water, water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerYearRegionCompareTrue()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            int result = comparer.YearRegionCompare(water, water2);
            int exp = 0;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerYearRegionCompareFalse()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 2
            };
            int result = comparer.YearRegionCompare(water, water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerYearRegionCompareNull()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                Year = 2021,
                RegionCode = 1
            };
            WaterConsumption water2 = null;
            int result = comparer.YearRegionCompare(water, water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerHouseholdConsumptionPerCapitaCompareTrue()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                HouseholdConsumptionPerCapita = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                HouseholdConsumptionPerCapita = 1
            };
            int result = comparer.HouseholdConsumptionPerCapitaCompare(water, water2);
            int exp = 0;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerHouseholdConsumptionPerCapitaCompareFalse()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                HouseholdConsumptionPerCapita = 1
            };
            WaterConsumption water2 = new WaterConsumption
            {
                HouseholdConsumptionPerCapita = 2
            };
            int result = comparer.HouseholdConsumptionPerCapitaCompare(water, water2);
            int exp = -1;
            //Assert
            Assert.Equal(exp, result);
        }
        [Fact]
        public void WaterConsumptionComparerHouseholdConsumptionPerCapitaCompareNull()
        {
            // Arrange & Act
            WaterConsumptionComparer comparer = new WaterConsumptionComparer();
            WaterConsumption water = new WaterConsumption
            {
                HouseholdConsumptionPerCapita = 1
            };
            WaterConsumption water2 = null;
            int result = comparer.HouseholdConsumptionPerCapitaCompare(water, water2);
            int exp = 1;
            //Assert
            Assert.Equal(exp, result);
        }
    }
}
