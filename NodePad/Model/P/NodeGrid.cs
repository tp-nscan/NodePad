using System.Collections.Generic;
using System.Linq;
using NodePad.Common;
using TT;

namespace NodePad.Model.P
{
    public class NodeGrid
    {
        public NodeGrid(Sz2<int> strides, float[] values, int generation, IEnumerable<float> noise)
        {
            Strides = strides;
            Values = values;
            Generation = generation;
            Noise = noise;

            var Offhi = new P2<int>(0, -1);
            var Offlo = new P2<int>(0, 1);
            var Offlf = new P2<int>(-1, 0);
            var Offrt = new P2<int>(1, 0);

            Top = GridUtil.OffsetIndexes(Strides, Offhi);
            Bottom = GridUtil.OffsetIndexes(Strides, Offlo);
            Left = GridUtil.OffsetIndexes(Strides, Offlf);
            Right = GridUtil.OffsetIndexes(Strides, Offrt);
        }
        
        public IEnumerable<float> Noise { get; }

        public int Generation { get; }

        public Sz2<int> Strides { get; }
        public float[] Values { get; }
        public int[] Top { get; }
        public int[] Bottom { get; }
        public int[] Left { get; }
        public int[] Right { get; }

        public P1V<int, float> Update(int index, float noise, float step)
        {
            var CurValue = Values[index];
            var DeltaL = NNfunc.ModUFDelta(CurValue, Values[Left[index]]);
            var DeltaR = NNfunc.ModUFDelta(CurValue, Values[Right[index]]);
            var DeltaT = NNfunc.ModUFDelta(CurValue, Values[Top[index]]);
            var DeltaB = NNfunc.ModUFDelta(CurValue, Values[Bottom[index]]);

            var ptb = Values[index] + noise + step *
                      (
                          DeltaL +
                          DeltaR +
                          DeltaT +
                          DeltaB
                      );

            return new P1V<int, float>(index, ptb);
        }

        public NodeGrid Update(float noiseLevel, float step)
        {
            var res = Enumerable.Range(0, Strides.Count())
                
                .Select(i => Update(i, noiseLevel, step));

            var nuVals = new float[Strides.Count()];

            foreach (var tup in res)
            {
                nuVals[tup.X] = tup.V;
            }

            return new NodeGrid(Strides, nuVals, Generation+1, Noise);
        }

    }

    public static class GridHelpers
    {
        public static int IndexForRc(int row, int col, int rowCount, int colCount)
        {
            return ((col + colCount) % colCount) + ((row + rowCount) % rowCount) * colCount;
        }



    }


}
