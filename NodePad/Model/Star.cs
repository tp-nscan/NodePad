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

        public void CalcDelta()
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
}
