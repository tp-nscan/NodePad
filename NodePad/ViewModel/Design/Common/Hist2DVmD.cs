using System;
using System.Collections.Generic;
using System.Linq;
using NodePad.ViewModel.Common;
using TT;

namespace NodePad.ViewModel.Design.Common
{
    public class Hist2DvmD : Hist2DVm
    {
        public Hist2DvmD() : base(BinCts, ColorLegT, "Design title")
        {
            UpdateData(TestData(100));
        }

        public static IEnumerable<P2<float>> TestData(int count)
        { 
            return GenBT.TestP2N(0.0f, 1.0f, DateTime.Now.Millisecond, count).ToArray(); ;
        }

        public static Sz2<int> BinCts => new Sz2<int>(100,100);

        public static R<float> TestBounds => 
            new R<float>(minX:-1.0f, maxX:2.2f, minY:1.2f, maxY:3.4f);

        public static Func<int[], ColorLeg<int>> ColorLegT => 
             ColorSets.WcHistLegInts;

    }
}
