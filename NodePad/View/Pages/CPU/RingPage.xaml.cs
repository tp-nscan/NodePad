using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    /// <summary>
    /// Interaction logic for Ring.xaml
    /// </summary>
    public partial class RingPage
    {
        public RingPage()
        {
            InitializeComponent();
            DataContext = new RingPageVm();
        }
    }
}
