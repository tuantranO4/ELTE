using Microsoft.VisualStudio.TestTools.UnitTesting;
using Planting;
using System.Collections.Generic;

namespace PlantingTests
{
    [TestClass]
    public class PlantSimulatorTests
    {
        private Simulator simulator;
        private List<Plant> plants;

        [TestInitialize]
        public void Setup()
        {
            plants = new List<Plant>();
            simulator = new Simulator(plants);
        }

        [TestMethod]
        public void TestZeroPlants()
        {
            simulator.Processing(5);
            Assert.AreEqual(0, plants.Count, "There should be no plants processed.");
        }

        [TestMethod]
        public void TestSinglePlantSurvival()
        {
            plants.Add(new Wombleroot("SinglePlant", 5));
            simulator.Processing(10);
            Assert.AreEqual(false,plants[0].Alive, "The single plant won't survive.");
        }



        [TestMethod]
        public void TestZeroDaySimulation()
        {
            plants.Add(new Wombleroot("Plant1", 10));
            simulator.Processing(0);
            Assert.AreEqual(10, plants[0].Nutrient, "Nutrient levels should remain unchanged with zero simulation days.");
        }

        [TestMethod]
        public void TestOneDaySimulation()
        {
            plants.Add(new Wombleroot("Plant1", 10));
            simulator.Processing(1);
            Assert.AreEqual(true,plants[0].Nutrient != 10, "Nutrient should change after one day of radiation.");
        }

        [TestMethod]

        public void TestPlantResponsesToRadiationTypes()
        {
            plants.Add(new Wombleroot("RadiationTestPlant", 5));
            foreach (var radType in new[] { RadiationType.Alpha, RadiationType.Delta, RadiationType.None })
            {
                plants[0].reactToRadType(radType);
                simulator.Processing(1);

            }
        }

        [TestMethod]
        public void TestExtremeConditions()
        {
            plants.Add(new Wombleroot("ExtremePlant", int.MaxValue));
            simulator.Processing(1);
            Assert.AreEqual(true,plants[0].Nutrient >= 0, "Nutrient should not overflow into negative.");
        }
    }
}
