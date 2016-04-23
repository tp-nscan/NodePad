using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Design.Common
{
    public class Grid2D2IVmD : Grid2D2IVm
    {
        public Grid2D2IVmD() 
            : base(TestStrides, ColorSets2I.QuadColorUFLeg2I, "Design title")
        {
            UpdateData(DesignData.Noisey2D2IGrid(TestStrides));
        }

        public static Sz2<int> TestStrides = new Sz2<int>(10, 10);

    }
}
