using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TT.Test
{
    [TestClass]
    public class ParamFixture
    {
        [TestMethod]
        public void TestParamDict()
        {
            var parames = Params.Ring5Params;

            var d = MathUtils.ListToDict(parames);

        }

        [TestMethod]
        public void TestReadParamGroup()
        {
            var parames = Params.Godzilla;

            var d = Params.GetParamGroupValue(parames, "a.b.cc.gg");

        }
    }
}
