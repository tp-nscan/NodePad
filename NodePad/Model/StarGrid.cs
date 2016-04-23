﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodePad.Common;
using TT;

namespace NodePad.Model
{
    public class StarGrid
    {
        public int Seed { get; }

        public StarGrid(float[,] initVals, int seed)
        {
            Seed = seed;
            Noise = GenS.NormalSF32(GenV.Twist(seed), 0.0f, 1.0f);
            Stars = StarProcs.MakeStarGrid(initVals);
            Rows = Stars.GetLength(1);
            Columns = Stars.GetLength(0);
            Generation = 0;
        }

        public IEnumerable<float> Noise { get; }

        public int Rows { get; }

        public int Columns { get; }

        public Star[,] Stars { get; }

        public int Generation { get; private set; }

        public void GetDeltas()
        {
            A2dUt.flattenRowMajor(Stars).ForEach(s => s.CalcDelta());
        }

        public void Update(float step, float noise)
        {
            var zippy = A2dUt.flattenRowMajor(Stars)
                             .Zip(Noise.Take(A2dUt.Count(Stars)),
                                 (a, b) => new Tuple<Star, float>(a, b));

            Parallel.ForEach(zippy, tup => tup.Item1.Update(step, tup.Item2 * noise));
            Generation++;
        }

    }

}