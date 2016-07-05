using System.Windows.Controls;
using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class Ring4Page : UserControl
    {
        public Ring4Page()
        {
            InitializeComponent();
            DataContext = new Ring4PageVm();
        }
    }
}
