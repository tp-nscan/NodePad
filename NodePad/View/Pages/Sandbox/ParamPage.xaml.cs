using NodePad.ViewModel.Pages.Sandbox;

namespace NodePad.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for ParamPage.xaml
    /// </summary>
    public partial class ParamPage
    {
        public ParamPage()
        {
            InitializeComponent();
            DataContext = new ParamPageVm();
        }
    }
}
