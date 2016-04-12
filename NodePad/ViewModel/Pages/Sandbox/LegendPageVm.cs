using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class LegendPageVm : BindableBase
    {
        public LegendPageVm()
        {
            LegendVm = new LegendVmD();
        }

        private LegendVm _legendVm;
        public LegendVm LegendVm
        {
            get { return _legendVm; }
            set
            {
                SetProperty(ref _legendVm, value);
            }
        }
    }
}
