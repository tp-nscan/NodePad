namespace NodePad.Model
{
    public class StarGrid
    {
        public StarGrid(float[,] initVals)
        {
            Stars = StarProcs.MakeStarGrid(initVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public int Rows { get; }

        public int Columns { get; }

        public Star[,] Stars { get; }

       public int Generation { get; private set; }

        public void GetDeltas()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    Stars[i, j].CalcDelta();
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

            Generation++;
        }

    }
}