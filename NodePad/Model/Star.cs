using System;
using TT;

namespace NodePad.Model
{
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

        public float DeltaL { get; private set; }
        public float DeltaR { get; private set; }
        public float DeltaT { get; private set; }
        public float DeltaB { get; private set; }

        public float AbsDelta { get; private set; }

        public void CalcDelta()
        {
            Delta  = NNfunc.ModUFDelta(CurValue, Left.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Right.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Top.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Bottom.CurValue);

            AbsDelta = Math.Abs(DeltaL) + Math.Abs(DeltaR) + Math.Abs(DeltaT) + Math.Abs(DeltaB);

        }

        public void Update(float step, float noise)
        {
            CurValue = NumUt.ModUF(CurValue + Delta * step + noise);
        }

    }
}
