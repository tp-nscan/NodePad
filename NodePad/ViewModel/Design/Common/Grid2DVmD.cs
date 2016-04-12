﻿using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Design.Common
{
    public class Grid2DVmD : Grid2DVm<float>
    {
        public Grid2DVmD() : base(TestStrides, ColorSets.RedBlueSFLeg, "Test title")
        {
            UpdateData(DesignData.Grid2DTestData(TestStrides));
        }

        public static Sz2<int> TestStrides = new Sz2<int>(30,15);
        
    }
}