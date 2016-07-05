using System.Windows.Controls;
using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    public partial class Ring2Page : UserControl
    {
        public Ring2Page()
        {
            InitializeComponent();
            DataContext = new Ring2PageVm();
        }
    }
}
