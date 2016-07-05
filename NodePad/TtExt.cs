using TT;

namespace NodePad
{
    public static class TtExt
    {
        public static int Count(this Sz2<int> bounds)
        {
            return bounds.X*bounds.Y;
        }
    }
}
