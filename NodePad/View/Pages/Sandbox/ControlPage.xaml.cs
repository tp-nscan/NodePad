using NodePad.ViewModel.Pages.Sandbox;

namespace NodePad.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for ControlPage.xaml
    /// </summary>
    public partial class ControlPage
    {
        public ControlPage()
        {
            InitializeComponent();
            DataContext = new ControlPageVm();
        }
    }
}
