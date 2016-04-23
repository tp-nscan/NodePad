using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class Grid2d2IPageVm : BindableBase
    {
        public Grid2d2IPageVm()
        {
            Grid2D2IVm = new Grid2D2IVmD();
        }

        private Grid2D2IVm _graphVm;
        public Grid2D2IVm Grid2D2IVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
