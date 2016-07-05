using System;
using TT;

namespace NodePad.Model.S
{
    public class Star3
    {
        public Star3(int row, int column, 
            float curValue, float fixedValue)
        {
            Row = row;
            Column = column;
            CurValue = curValue;
            FixedValue = fixedValue;
        }

        public Star3 Left { get; set; }
        public Star3 Right { get; set; }
        public Star3 Top { get; set; }
        public Star3 Bottom { get; set; }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public float CurValue { get; private set; }
        public float FixedValue { get; private set; }

        public float Delta { get; private set; }

        public float DeltaL { get; private set; }
        public float DeltaR { get; private set; }
        public float DeltaT { get; private set; }
        public float DeltaB { get; private set; }

        public float AbsDelta { get; private set; }

        public void CalcDelta()
        {
            Delta =  NNfunc.ModUFDelta(CurValue, Left.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Right.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Top.CurValue);
            Delta += NNfunc.ModUFDelta(CurValue, Bottom.CurValue);
           // Delta += NNfunc.ModUFDelta(CurValue, Bottom.CurValue);

            AbsDelta = Math.Abs(DeltaL) + Math.Abs(DeltaR) + Math.Abs(DeltaT) + Math.Abs(DeltaB);
        }

        public void Update(float step, float noise, float ffCpl)
        {
            if (Math.Abs(FixedValue) > NumUt.Epsilon)
            {
                Delta += ffCpl * NNfunc.ModUFDelta(CurValue, FixedValue);
            }

            CurValue = NumUt.ModUF32(CurValue + Delta*step + noise);
        }
    }
}
