using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodePad.Common;
using TT;

namespace NodePad.Model
{
    public class Star4Grid
    {
        public int Seed { get; }

        public Star4Grid(float[,] initVals, int seed)
        {
            Seed = seed;
            Noise = GenS.NormalSF32(GenV.Twist(seed), 0.0f, 1.0f);
            Stars = Star4Procs.MakeStarGrid(initVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public IEnumerable<float> Noise { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Star4[,] Stars { get; }

        public int Generation { get; private set; }

        public void GetDeltas()
        {
            A2dUt.flattenRowMajor(Stars).ForEach(s => s.CalcDelta());
        }

        public void Update(float step,
                           float noise,
                           float nfDecay,
                           float absDeltaToNoise,
                           float nfCpl)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star4, float>(a, b));

            Parallel.ForEach(zippy, tup => tup.Item1.Update(
                step: step,
                noise: tup.Item2 * noise,
                nfDecay: nfDecay,
                absDeltaToNoise: absDeltaToNoise,
                nfCpl: nfCpl));


            Generation++;
        }

    }


}
