﻿using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc4.Interfaces;
using Xbim.IO.Memory;

namespace Xbim.Geometry.Engine.Interop.Tests
{
    [TestClass]
    public class PrimitiveGeometryTests
    {
        static private IXbimGeometryEngine geomEngine;
        static private ILoggerFactory loggerFactory;
        static private ILogger logger;

        [ClassInitialize]
        static public void Initialise(TestContext context)
        {
            loggerFactory = new LoggerFactory().AddConsole(LogLevel.Trace);
            geomEngine = new XbimGeometryEngine();
            logger = loggerFactory.CreateLogger<Ifc4GeometryTests>();
        }
        [ClassCleanup]
        static public void Cleanup()
        {
            loggerFactory = null;
            geomEngine = null;
            logger = null;
        }

        [TestMethod]
        public void can_build_composite_curve()
        {
            using (var model = MemoryModel.OpenRead(@".\\TestFiles\Primitives\\composite_curve.ifc"))
            {
                var compCurve = model.Instances.OfType<IIfcCompositeCurve>().FirstOrDefault();
                Assert.IsNotNull(compCurve);
                var face = geomEngine.CreateFace(compCurve);
                Assert.IsNotNull(face.OuterBound);
            }
        }
    }
}
