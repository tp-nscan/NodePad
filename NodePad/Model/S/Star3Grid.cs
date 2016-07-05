using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodePad.Common;
using TT;

namespace NodePad.Model.S
{
    public class Star3Grid
    {
        public int Seed { get; }

        public Star3Grid(float[,] initVals, float[,] fixedVals, int seed)
        {
            Seed = seed;
            Noise = GenS.NormalSF32(GenV.Twist(seed), 0.0f, 1.0f);
            Stars = Star3Procs.MakeStarGrid(initVals, fixedVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public IEnumerable<float> Noise { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Star3[,] Stars { get; }

        public int Generation { get; private set; }

        public void GetDeltas()
        {
            A2dUt.flattenRowMajor(Stars).ForEach(s => s.CalcDelta());
        }

        public void Update(float step, 
                           float noise, 
                           float ffCpl)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star3, float>(a, b));

            Parallel.ForEach(zippy, tup => tup.Item1.Update(step, tup.Item2 * noise, ffCpl));
            Generation++;
        }

    }

}