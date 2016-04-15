using System;

namespace NodePad.Model
{
    public class StarGrid
    {
        public StarGrid(float[,] initVals)
        {
            Stars = StarProcs.MakeStarGrid(initVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
        }

        public int Rows { get; }

        public int Columns { get; }

        public Star[,] Stars { get; private set; }

        public void GetDeltas()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Stars[i, j].GetDelta();
                }
            }
        }

        public void Update(float step)
        {
            for(var i=0; i< Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Stars[i,j].Update(step);
                }
            }
        }

    }

    public class Star
    {
        public Star(int row, int column, float curValue)
        {
            Row = row;
            Column = column;
            CurValue = curValue;
        }

        public Star Left { get; set; }
        public Star Right { get; set; }
        public Star Top { get; set; }
        public Star Bottom { get; set; }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public float CurValue { get; private set; }
        public float Delta { get; private set; }

        public void GetDelta()
        {
            Delta  = CurValue.MfDelta(Left.CurValue);
            Delta += CurValue.MfDelta(Top.CurValue);
            Delta += CurValue.MfDelta(Right.CurValue);
            Delta += CurValue.MfDelta(Bottom.CurValue);
        }

        public void Update(float step)
        {
            CurValue = (CurValue + Delta*step).AsMf();
        }

    }

    public static class StarProcs
    {
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
                    stars[i, j].Left = stars[(i - 1 + rows) % rows, j];
                    stars[i, j].Left = stars[i, (j - 1 + cols) % cols];
                    stars[i, j].Left = stars[(i + 1 + rows) % rows, j];
                    stars[i, j].Left = stars[i, (j + 1 + cols) % cols];
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
            return 1.0f - (-lhs).AsMf();
        }


    }

}
