using System.Collections.Generic;
using System.Linq;
using TT;

namespace NodePad.Model.S
{
    public static class Star5Procs
    {
        public static Star5Grid RandStarGrid(Sz2<int> bounds, float[,] fixedVals, int seed)
        {
            return new Star5Grid(GenA2.RandUF32(bounds, seed), fixedVals, seed);
        }

        public static Star5[,] MakeStarGrid(float[,] initVals,
            float[,] fixedVals)
        {
            var rows = initVals.GetLength(1);
            var cols = initVals.GetLength(0);

            var stars = new Star5[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    stars[i, j] = new Star5(i, j, initVals[i, j], 
                        fixedVals[i, j]);
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


        public static IEnumerable<P2V<int, float>> CurValuesAsP2Vs(this Star5Grid starGrid)
        {
            return A2dUt.ToP2V(starGrid.Stars).Select(s =>
                new P2V<int, float>(x: s.X, y: s.Y, v: s.V.CurValue));
        }

        public static IEnumerable<P2V<int, float>> CurDeltasAsP2Vs(this Star5Grid starGrid)
        {
            return A2dUt.ToP2V(starGrid.Stars).Select(s =>
                new P2V<int, float>(x: s.X, y: s.Y, v: s.V.NoiseField));
        }
    }
}