using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class Ring5Page
    {
        public Ring5Page()
        {
            InitializeComponent();
            DataContext = new Ring5PageVm();
        }
    }
}
