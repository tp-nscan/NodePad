using System.Windows.Controls;
using NodePad.ViewModel.Pages.CPU;

namespace NodePad.View.Pages.CPU
{
    /// <summary>
    /// Interaction logic for Ring2Page.xaml
    /// </summary>
    public partial class Ring2Page : UserControl
    {
        public Ring2Page()
        {
            InitializeComponent();
            DataContext = new Ring2PageVm();
        }
    }
}
