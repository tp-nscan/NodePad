using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePad.Model.P
{
    public class NodeArray
    {
        public NodeArray(double[] initVal)
        {
            _arrayA = initVal;
            _arrayB = new double[_arrayA.Length];
            ArraySize = _arrayA.Length;
            ArrayStride = (int) Math.Sqrt(ArraySize);
            _generation = 0;
        }

        double[] _arrayA;
        double[] _arrayB;
        public int ArraySize { get; }
        public int ArrayStride { get; }

        private int _generation;
        public int Generation => _generation;

        public IReadOnlyList<double> Current => 
            (Generation % 2 == 0) ? _arrayA.ToList() : _arrayB.ToList();

        double[] InputArray
        {
            get { return (Generation%2 == 0) ? _arrayA : _arrayB; }
            set
            {
                if (Generation % 2 == 0)
                {
                    _arrayA = value;
                }
                else
                {
                    _arrayB = value;
                }
            }
        }


        double[] OutputArray
        {
            get { return (Generation%2 == 0) ? _arrayB : _arrayA; }
            set
            {
                if (Generation % 2 == 0)
                {
                    _arrayB = value;
                }
                else
                {
                    _arrayA = value;
                }
            }
        }

        void ProcStretch2d(int curChunk, int chunkCount, double[] input, double[] output,  double cpl, double noiseLevel, Random randy)
        {
            var alength = output.Length;
            var slength = (int)Math.Sqrt(alength);
            var start = curChunk * alength / chunkCount;
            var end = curChunk * alength / chunkCount + alength / chunkCount;
            var halfCpl = cpl / 2;

            for (var i = start; i < end; i++)
            {
                var nbrs = input[(i + 1 + alength) % alength]
                         + input[(i - 1 + alength) % alength]
                         + input[(i - slength + alength) % alength]
                         + input[(i + slength + alength) % alength];

                var res = cpl * nbrs + input[i] + randy.NextDouble() * noiseLevel - halfCpl;
                output[i] = (res < -1.0) ? -1.0 : (res > 1.0) ? 1.0 : res;
            }
        }

        public static NodeArray UpdateStar(NodeArray nodeArray, double cpl, double noise, Random randy)
        {
            var input = nodeArray.InputArray;
            var output = nodeArray.OutputArray;
            nodeArray._generation++;

            var chunkCount = 8;

            Parallel.For((long) 0, chunkCount, i => nodeArray.ProcStretch2d(
                curChunk: (int)i, 
                chunkCount: chunkCount,
                input: input,
                output: output, 
                cpl: cpl,
                noiseLevel: noise,
                randy: randy));

            nodeArray.InputArray = output;
            return nodeArray;
        }
    }
}
