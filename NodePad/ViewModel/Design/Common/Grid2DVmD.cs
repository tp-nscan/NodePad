using System.Windows;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Design.Common
{
    public class Grid2DVmD : Grid2DVm<float>
    {
        public Grid2DVmD() : base(TestStrides, ColorSets.RedBlueSFLeg, "Test title")
        {
            //UpdateData(DesignData.NoiseyStarAs2d(TestStrides));
            //UpdateData(DesignData.CirculoPtsV(bounds: TestStrides, 
            //                                  center: new P2<double>(0.4, 0.6), 
            //                                  radius:2.4));

            UpdateData(DesignData.HoleFunc(50.0, TestStrides));

            //foreach (var t in DesignData.HoleFunc(5.0, TestStrides))
            //{
            //    System.Diagnostics.Debug.WriteLine($"{t.X}\t{t.Y}\t{t.V}\t");
            //}

        }

        public static Sz2<int> TestStrides = new Sz2<int>(150,150);
        
    }
}