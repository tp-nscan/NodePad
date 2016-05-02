using System;
using TT;

namespace NodePad.Model
{
    public class Star4
    {
        public Star4(int row, int column, float curValue)
        {
            Row = row;
            Column = column;
            CurValue = curValue;
        }

        public Star4 Left { get; set; }
        public Star4 Right { get; set; }
        public Star4 Top { get; set; }
        public Star4 Bottom { get; set; }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public float CurValue { get; private set; }

        public float Delta { get; private set; }

        public float DeltaL { get; private set; }
        public float DeltaR { get; private set; }
        public float DeltaT { get; private set; }
        public float DeltaB { get; private set; }

        public float AbsDelta { get; private set; }

        public float NoiseFieldCpl { get; private set; }

        public float NoiseField { get; private set; }

        public void CalcDelta()
        {
            DeltaL = NNfunc.ModUFDelta(CurValue, Left.CurValue);
            DeltaR = NNfunc.ModUFDelta(CurValue, Right.CurValue);
            DeltaT = NNfunc.ModUFDelta(CurValue, Top.CurValue);
            DeltaB = NNfunc.ModUFDelta(CurValue, Bottom.CurValue);

            Delta = DeltaL + DeltaR + DeltaT + DeltaB;

            AbsDelta = Math.Abs(DeltaL) + Math.Abs(DeltaR) +
                       Math.Abs(DeltaT) + Math.Abs(DeltaB);

            NoiseFieldCpl = Left.NoiseField + Right.NoiseField +
                            Top.NoiseField + Bottom.NoiseField;
        }

        public void Update(float step, float noise, float nfDecay,
                           float absDeltaToNoise, float nfCpl)
        {
            NoiseField = NoiseField * nfDecay +
                         AbsDelta * absDeltaToNoise +
                         NoiseFieldCpl * nfCpl;

            CurValue = NumUt.ModUF32(CurValue + Delta * step + noise * NoiseField);

        }

    }
}