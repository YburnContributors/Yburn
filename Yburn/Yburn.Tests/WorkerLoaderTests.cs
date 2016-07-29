using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yburn.Tests
{
    [TestClass]
    public class WorkerLoaderTests
    {
        /********************************************************************************************
       * Public members, functions and properties
       ********************************************************************************************/

        [TestMethod]
        public void LoadSingleQQ()
        {
            Assert.IsNotNull(WorkerLoader.CreateInstance("SingleQQ"));
        }

        [TestMethod]
        public void LoadQQonFire()
        {
            Assert.IsNotNull(WorkerLoader.CreateInstance("QQonFire"));
        }

        [TestMethod]
        public void LoadElectromagnetism()
        {
            Assert.IsNotNull(WorkerLoader.CreateInstance("Electromagnetism"));
        }

        [TestMethod]
        public void LoadInMediumDecayWidth()
        {
            Assert.IsNotNull(WorkerLoader.CreateInstance("InMediumDecayWidth"));
        }
    }
}