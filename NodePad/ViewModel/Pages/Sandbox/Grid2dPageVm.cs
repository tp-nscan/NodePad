using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class Grid2dPageVm : BindableBase
    {
        public Grid2dPageVm()
        {
            Grid2DVm = new Grid2DVmD();
        }

        private Grid2DVm<float> _graphVm;
        public Grid2DVm<float> Grid2DVm
        {
            get { return _graphVm; }
            set
            {
                SetProperty(ref _graphVm, value);
            }
        }
    }
}
