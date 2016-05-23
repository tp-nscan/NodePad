using NodePad.Common;
using NodePad.ViewModel.Common.ParamVm;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class ParamPageVm : BindableBase
    {
        public ParamPageVm()
        {
            ParamGroupVm = new ParamGroupVmD();
        }

        private ParamGroupVm _paramGroupVm;

        public ParamGroupVm ParamGroupVm
        {
            get { return _paramGroupVm; }
            set
            {
                SetProperty(ref _paramGroupVm, value);
            }
        }
    }

}
