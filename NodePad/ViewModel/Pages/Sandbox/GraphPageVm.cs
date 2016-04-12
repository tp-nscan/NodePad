using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class GraphPageVm : BindableBase
    {
        public GraphPageVm()
        {
            GraphVm = new GraphVmD();
        }

        private GraphVmD _graphVm;
        public GraphVmD GraphVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
