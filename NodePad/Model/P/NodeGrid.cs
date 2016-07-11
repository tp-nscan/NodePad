using System.Collections.Generic;
using System.Linq;
using TT;

namespace NodePad.Model.P
{
    public class NodeGrid
    {
        public NodeGrid(Sz2<int> strides, IEnumerable<P1V<int, float>> tuples, int generation, int seed)
        {
            Strides = strides;
            Values = new float[Strides.Count()];
            foreach (var tup in tuples)
            {
                Values[tup.X] = tup.V;
            }

            Generation = generation;

            Seed = seed;
            var randy = GenV.Twist(Seed);
            Noise = GenS.NormalSF32(randy, 0.0f, 1.0f).Take(Strides.Count()).ToArray();
            NextSeed = randy.Next();

            var Offhi = new P2<int>(0, -1);
            var Offlo = new P2<int>(0, 1);
            var Offlf = new P2<int>(-1, 0);
            var Offrt = new P2<int>(1, 0);

            Top = GridUtil.OffsetIndexes(Strides, Offhi);
            Bottom = GridUtil.OffsetIndexes(Strides, Offlo);
            Left = GridUtil.OffsetIndexes(Strides, Offlf);
            Right = GridUtil.OffsetIndexes(Strides, Offrt);
        }

        public int NextSeed { get; }
        public int Seed { get; }

        public IList<float> Noise { get; }

        public int Generation { get; }

        public Sz2<int> Strides { get; }
        public float[] Values { get; }
        public int[] Top { get; }
        public int[] Bottom { get; }
        public int[] Left { get; }
        public int[] Right { get; }

        public P1V<int, float> Update(int index, float noiseLevel, float step)
        {
            var curValue = Values[index];
            var deltaL = NNfunc.ModUFDelta(curValue, Values[Left[index]]);
            var deltaR = NNfunc.ModUFDelta(curValue, Values[Right[index]]);
            var deltaT = NNfunc.ModUFDelta(curValue, Values[Top[index]]);
            var deltaB = NNfunc.ModUFDelta(curValue, Values[Bottom[index]]);

            var ptb = NumUt.ModUF32
                (
                    curValue + 
                    noiseLevel * Noise[index] + 
                    step * (
                              deltaL +
                              deltaR +
                              deltaT +
                              deltaB
                           )
                );

            return new P1V<int, float>(index, ptb);
        }

    }


}
