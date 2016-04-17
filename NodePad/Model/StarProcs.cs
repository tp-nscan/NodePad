using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT;

namespace NodePad.Model
{
    public static class StarProcs
    {
        public static StarGrid RandStarGrid(Sz2<int> bounds, int seed)
        {
            return new StarGrid(GenA2.RandUF32(bounds, seed));
        } 

        public static Star[,] MakeStarGrid(float[,] initVals)
        {
            var rows = initVals.GetLength(1);
            var cols = initVals.GetLength(0);

            var stars = new Star[rows, cols] ;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    stars[i,j] = new Star(i, j, initVals[i,j]);
                }
            }

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    stars[i, j].Top = stars[(i - 1 + rows) % rows, j];
                    stars[i, j].Left = stars[i,                    (j - 1 + cols) % cols];
                    stars[i, j].Bottom = stars[(i + 1 + rows) % rows, j];
                    stars[i, j].Right = stars[i,                    (j + 1 + cols) % cols];
                }
            }

            return stars;
        }

        //returns the directed circular distance from lhs to rhs, positive is clockwise
        public static float MfDelta(this float lhs, float rhs)
        {
            var delta = lhs - rhs;
            if (lhs > rhs)
            {
                return (delta <= 0.5) ? -delta : 1.0f - delta;
            }
            return (delta < -0.5) ? -1.0f - delta : -delta;
        }


        public static float AsMf(this float lhs)
        {
            if (lhs >= 0)
            {
                if (lhs < 1.0)
                {
                    return lhs;
                }
                return (float)(lhs - Math.Floor(lhs));
            }
            return 1000.0f - (-lhs).AsMf();
        }

        public static IEnumerable<P2V<int, float>> ToP2Vs(this StarGrid starGrid)
        {
            return A2dUt.ToP2V(starGrid.Stars).Select(s =>
                new P2V<int, float>(x:s.X, y:s.Y, v:s.V.CurValue));
        }

        public static StarGrid Update(this StarGrid starGrid, float stepSize)
        {
            Parallel.ForEach(A2dUt.flattenRowMajor(starGrid.Stars), st => st.CalcDelta());
            Parallel.ForEach(A2dUt.flattenRowMajor(starGrid.Stars), st => st.Update(stepSize));
            return starGrid;
        }
    }
}