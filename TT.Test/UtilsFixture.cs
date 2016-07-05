using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TT.Test
{
    [TestClass]
    public class Utilsixture
    {
        [TestMethod]
        public void TestLinMod()
        {
            var modulus = 5;

            var val = 3;
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val + modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val + 2*modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val - modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val - 2*modulus));

            val = 1;
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val + modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val + 2*modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val - modulus));
            Assert.AreEqual(val, GridUtil.LinMod(modulus, val - 2*modulus));

        }


        [TestMethod]
        public void TestIndexForGrid()
        {
            var rowCount = 5;
            var colCount = 7;
            var gridSz = new Sz2<int>(colCount, rowCount);
            var Offpp = new P2<int>(colCount, rowCount);
            var Offmm = new P2<int>(-colCount, -rowCount);
            var Off2pp = new P2<int>(2*colCount, 2*rowCount);
            var Off2mm = new P2<int>(-2*colCount, -2*rowCount);

            var row = 1;
            var col = 2;
            var dex = row*colCount + col;
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offpp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offmm));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2pp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2mm));


            row = 4;
            col = 6;
            dex = row*colCount + col;
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offpp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offmm));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2pp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2mm));

            row = 2;
            col = 6;
            dex = row*colCount + col;
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offpp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Offmm));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2pp));
            Assert.AreEqual(dex, GridUtil.IndexForGrid(gridSz, Off2mm));

        }

        [TestMethod]
        public void TestCoordsForGridIndex()
        {
            var colCount = 7;

            var row = 1;
            var col = 2;
            var dex = row*colCount + col;
            var coords = GridUtil.CoordsForGridIndex(colCount, dex);
            Assert.AreEqual(col, coords.X);
            Assert.AreEqual(row, coords.Y);

            row = 11;
            col = 5;
            dex = row*colCount + col;
            coords = GridUtil.CoordsForGridIndex(colCount, dex);
            Assert.AreEqual(col, coords.X);
            Assert.AreEqual(row, coords.Y);

            row = 9;
            col = 1;
            dex = row*colCount + col;
            coords = GridUtil.CoordsForGridIndex(colCount, dex);
            Assert.AreEqual(col, coords.X);
            Assert.AreEqual(row, coords.Y);


        }

        [TestMethod]
        public void TestOffsetIndexes()
        {
            var rowCount = 5;
            var colCount = 7;
            var gridSz = new Sz2<int>(colCount, rowCount);

            var Offp = new P2<int>(0, 0);
            var Offpp = new P2<int>(1, 1);

            var a1 = GridUtil.OffsetIndexes(gridSz, Offp);
            var a2 = GridUtil.OffsetIndexes(gridSz, Offpp);

        }

    }
}
