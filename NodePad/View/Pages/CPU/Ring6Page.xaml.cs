using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class Ring6Page
    {
        public Ring6Page()
        {
            InitializeComponent();
            DataContext = new Ring6PageVm();
        }
    }
}
