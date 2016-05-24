using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodePad.ViewModel.Common.ParamVm;
using TT;

namespace NotePad.Test
{
    [TestClass]
    public class ParamVmFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pg = Params.Godzilla;

            var vm = new ParamGroupVm(pg, String.Empty);

            var pg2 = vm.UpdatedParamGroup;

            var chump = Params.GetAllParams(pg).ToList();
            var chump2 = Params.GetAllParams(pg2).ToList();
        }
    }
}
