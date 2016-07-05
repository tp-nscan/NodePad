using System;
using System.Collections.Generic;
using System.Linq;
using NodePad.Common;
using TT;

namespace NodePad.Model.S
{
    public class Star5Grid
    {
        public int Seed { get; }

        public Star5Grid(float[,] initVals, float[,] fixedVals, int seed)
        {
            Seed = seed;
            Noise = GenS.NormalSF32(GenV.Twist(seed), 0.0f, 1.0f);
            Stars = Star5Procs.MakeStarGrid(initVals, fixedVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public IEnumerable<float> Noise { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Star5[,] Stars { get; }

        public int Generation { get; private set; }

        public void GetDeltas()
        {
            //500 @ 10.74s
           // A2dUt.flattenRowMajor(Stars).ForEach(s => s.CalcDelta());
            // 39.167
            //var pcs = A2dUt.flattenRowMajor(Stars).ToArray();
            //Parallel.ForEach(pcs, s => s.CalcDelta());
        }

        public void Update(float step,
                           float noise,
                           float nfDecay,
                           float absDeltaToNoise,
                           float nfCpl,
                           float ffCpl)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star5, float>(a, b));

            zippy.ToArray().AsParallel().ForEach(
                tup => tup.Item1.Update(
                    step: step,
                    noise: tup.Item2 * noise,
                    nfDecay: nfDecay,
                    absDeltaToNoise: absDeltaToNoise,
                    nfCpl: nfCpl,
                    ffCpl: ffCpl)
                );

            //zippy.ForEach(

            //    tup => tup.Item1.UpdateP(
            //        step: step,
            //        noiseLevel: tup.Item2 * noiseLevel,
            //        nfDecay: nfDecay,
            //        absDeltaToNoise: absDeltaToNoise,
            //        nfCpl: nfCpl,
            //        ffCpl: ffCpl)

            //    );

            //Parallel.ForEach(zippy, tup => tup.Item1.UpdateP(
            //    step: step,
            //    noiseLevel: tup.Item2 * noiseLevel,
            //    nfDecay: nfDecay,
            //    absDeltaToNoise: absDeltaToNoise,
            //    nfCpl: nfCpl,
            //    ffCpl: ffCpl));


            Generation++;
        }

    }


}
