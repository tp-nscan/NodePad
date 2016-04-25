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
            UpdateData(DesignData.CirculoPts(bounds: TestStrides, 
                                             center: new P2<double>(0.4, 0.6), 
                                             radius:0.4));
            
        }

        public static Sz2<int> TestStrides = new Sz2<int>(250,250);
        
    }
}