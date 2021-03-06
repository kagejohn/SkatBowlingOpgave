// <copyright file="RestApiViewModelsTest.cs">Copyright ©  2017</copyright>
using System;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkatBowlingOpgave.DataModels;
using SkatBowlingOpgave.Models;

namespace SkatBowlingOpgave.Models.Tests
{
    /// <summary>This class contains parameterized unit tests for RestApiViewModels</summary>
    [PexClass(typeof(RestApiViewModels))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class RestApiViewModelsTest
    {
        /// <summary>Test stub for Bowlingpoints()</summary>
        [PexMethod]
        public Task<PointResultater> BowlingpointsTest([PexAssumeUnderTest]RestApiViewModels target)
        {
            Task<PointResultater> result = target.Bowlingpoints();
            return result;
            // TODO: add assertions to method RestApiViewModelsTest.BowlingpointsTest(RestApiViewModels)
        }

        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public RestApiViewModels ConstructorTest()
        {
            RestApiViewModels target = new RestApiViewModels();
            return target;
            // TODO: add assertions to method RestApiViewModelsTest.ConstructorTest()
        }
    }
}
