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
            var curValue = Values[index];
            var deltaL = NNfunc.ModUFDelta(curValue, Values[Left[index]]);
            var deltaR = NNfunc.ModUFDelta(curValue, Values[Right[index]]);
            var deltaT = NNfunc.ModUFDelta(curValue, Values[Top[index]]);
            var deltaB = NNfunc.ModUFDelta(curValue, Values[Bottom[index]]);

            var ptb = NumUt.ModUF32(curValue + noise + step *
                      (
                          deltaL +
                          deltaR +
                          deltaT +
                          deltaB
                      ));

            return new P1V<int, float>(index, ptb);
        }

        public NodeGrid Update(float step, float noiseLevel)
        {
            var noisyList = Noise.Take(Strides.Count())
                .Select(t => t*noiseLevel)
                .ToList();

            var res = noisyList.Select((v, i) => Update(i, v, step));           

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
