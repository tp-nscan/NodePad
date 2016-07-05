using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodePad.Common;
using TT;

namespace NodePad.Model.S
{
    public class StarGrid
    {
        public StarGrid(float[,] initVals, int noiseSeed)
        {
            NoiseSeed = noiseSeed;
            Noise = GenS.NormalSF32(GenV.Twist(NoiseSeed), 0.0f, 1.0f);
            Stars = StarProcs.MakeStarGrid(initVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public int NoiseSeed { get; }

        public IEnumerable<float> Noise { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Star[,] Stars { get; }

        public int Generation { get; private set; }

        public void GetDeltas()
        {
            A2dUt.flattenRowMajor(Stars).ForEach(s => s.CalcDelta());
        }

        public void UpdateP(float step, float noise)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star, float>(a, b));

            Parallel.ForEach(zippy, tup => tup.Item1.Update(step, tup.Item2 * noise));
            Generation++;
        }

        public void UpdateS(float step, float noise)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star, float>(a, b));

            foreach (var tup in zippy)
            {
                tup.Item1.Update(step, tup.Item2*noise);
            }

            Generation++;
        }

    }

}