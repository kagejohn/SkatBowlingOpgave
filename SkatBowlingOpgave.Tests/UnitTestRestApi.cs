using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkatBowlingOpgave.Models;
using SkatBowlingOpgave.Models.Tests;

namespace SkatBowlingOpgave.Tests
{
    /// <summary>
    /// Summary description for UnitTestRestApi
    /// </summary>
    [TestClass]
    public class UnitTestRestApi
    {
        private RestApiViewModelsTest restApiViewModelsTest;
        private RestApiViewModels restApiViewModels;
        public UnitTestRestApi()
        {
            restApiViewModelsTest = new RestApiViewModelsTest();
            restApiViewModels = new RestApiViewModels();
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("Resultat uploadet succesfuldt", restApiViewModelsTest.BowlingpointsTest(restApiViewModels).Result.success);
        }
    }
}
