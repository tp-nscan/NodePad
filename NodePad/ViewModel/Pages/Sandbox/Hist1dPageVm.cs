using NodePad.Common;
using NodePad.ViewModel.Common;
using NodePad.ViewModel.Design.Common;

namespace NodePad.ViewModel.Pages.Sandbox
{
    public class Hist1dPageVm : BindableBase
    {
        public Hist1dPageVm()
        {
            Hist1DVm = new Hist1DvmD();
        }

        private Hist1DVm _hist1DVm;
        public Hist1DVm Hist1DVm
        {
            get { return _hist1DVm; }
            set
            {
                SetProperty(ref _hist1DVm, value);
            }
        }
    }
}
