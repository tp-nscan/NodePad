using System;
using System.Linq;
using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;
using TT;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class Hist2dPageVm : BindableBase
    {
        public Hist2dPageVm(int count)
        {
            Hist2DVm = new Hist2DVm(Hist2DvmD.BinCts, Hist2DvmD. ColorLegT, "Design title");
            CountVm = new IntVm(count, 10000000, "Count", 0);
            CountVm.OnValueChanged.Subscribe(ResetVals);
        }

        void ResetVals(int count)
        {
            _hist2DVm.UpdateData(
                GenBT.TestP2N(0.0f, 1.0f, DateTime.Now.Millisecond, count)
                     .ToArray());
        }

        private Hist2DVm _hist2DVm;
        public Hist2DVm Hist2DVm
        {
            get { return _hist2DVm; }
            set
            {
                SetProperty(ref _hist2DVm, value);
            }
        }

        public IntVm CountVm { get; }
    }
}
