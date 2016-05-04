using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    /// <summary>
    /// Interaction logic for Ring2Page.xaml
    /// </summary>
    public partial class Ring5Page
    {
        public Ring5Page()
        {
            InitializeComponent();
            DataContext = new Ring5PageVm();
        }
    }
}
