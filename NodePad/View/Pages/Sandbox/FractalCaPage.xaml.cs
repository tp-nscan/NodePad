using NodePad.ViewModel.Pages.Sandbox;

namespace NodePad.View.Pages.Sandbox
{
    public partial class FractalCaPage
    {
        public FractalCaPage()
        {
            InitializeComponent();
            DataContext = new FractalCaPageVm();
        }
    }
}
