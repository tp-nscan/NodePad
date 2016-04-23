using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    /// <summary>
    /// Interaction logic for RingPageControl.xaml
    /// </summary>
    public partial class PairsPageControl
    {
        public PairsPageControl()
        {
            InitializeComponent();
            DataContext = new PairsPageVm();
        }
    }
}
