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

            var d = MathUtils.ArrayToDict(parames);

        }
    }
}
