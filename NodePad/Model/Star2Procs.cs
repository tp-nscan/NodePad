using System.Collections.Generic;
using System.Linq;
using TT;

namespace NodePad.Model
{
    public static class Star2Procs
    {
        public static Star2Grid RandStarGrid(Sz2<int> bounds, int seed)
        {
            return new Star2Grid(GenA2.RandUF32(bounds, seed), seed);
        }

        public static Star2[,] MakeStarGrid(float[,] initVals)
        {
            var rows = initVals.GetLength(1);
            var cols = initVals.GetLength(0);

            var stars = new Star2[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    stars[i, j] = new Star2(i, j, initVals[i, j]);
                }
            }

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    stars[i, j].Top = stars[(i - 1 + rows) % rows, j];
                    stars[i, j].Left = stars[i, (j - 1 + cols) % cols];
                    stars[i, j].Bottom = stars[(i + 1 + rows) % rows, j];
                    stars[i, j].Right = stars[i, (j + 1 + cols) % cols];
                }
            }

            return stars;
        }


        public static IEnumerable<P2V<int, float>> CurValuesAsP2Vs(this Star2Grid starGrid)
        {
            return A2dUt.ToP2V(starGrid.Stars).Select(s =>
                new P2V<int, float>(x: s.X, y: s.Y, v: s.V.CurValue));
        }

        public static IEnumerable<P2V<int, float>> CurDeltasAsP2Vs(this Star2Grid starGrid)
        {
            return A2dUt.ToP2V(starGrid.Stars).Select(s =>
                new P2V<int, float>(x: s.X, y: s.Y, v: s.V.NoiseField));
        }
    }
}