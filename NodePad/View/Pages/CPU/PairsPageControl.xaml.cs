using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class PairsPageControl
    {
        public PairsPageControl()
        {
            InitializeComponent();
            DataContext = new PairsPageVm();
        }
    }
}
