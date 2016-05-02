using System.Windows.Controls;
using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    /// <summary>
    /// Interaction logic for Ring2Page.xaml
    /// </summary>
    public partial class Ring4Page : UserControl
    {
        public Ring4Page()
        {
            InitializeComponent();
            DataContext = new Ring4PageVm();
        }
    }
}
