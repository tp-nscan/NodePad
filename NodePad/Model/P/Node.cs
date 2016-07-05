namespace NodePad.Model.P
{
    public struct Node
    {
        public Node(int index, float value, float upLeft, float upRight, float lowLeft, float lowRight)
        {
            Index = index;
            Value = value;
            UpLeft = upLeft;
            UpRight = upRight;
            LowLeft = lowLeft;
            LowRight = lowRight;
        }

        public int Index { get; }
        public float Value { get; }
        public float UpLeft { get; }
        public float UpRight { get; }
        public float LowLeft { get; }
        public float LowRight { get; }
    }



}
