using NodePad.ViewModel.Pages.Sandbox;

namespace NodePad.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for Grid2d2IPage.xaml
    /// </summary>
    public partial class Grid2d2IPage
    {
        public Grid2d2IPage()
        {
            InitializeComponent();
            DataContext = new Grid2d2IPageVm();
        }
    }
}
