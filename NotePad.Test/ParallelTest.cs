using System;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodePad.Common;
using NodePad.Model.P;

namespace NodePad.Test
{
    [TestClass]
    public class ParallelTest
    {
        [TestMethod]
        public unsafe void SerialFixed1d()
        {
            var alength = 16384;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();

            for (var s = 0; s < 1000; s++)
            {
                fixed (double* aptr1 = ape1, aptr2 = ape2)
                {
                    for (var i = 0; i < alength; i++)
                    {
                        *(aptr2 + i) = *(aptr1 + (i + 1 + alength) % alength) + *(aptr1 + (i - 1 + alength) % alength);
                    }
                    for (var i = 0; i < alength; i++)
                    {
                        *(aptr1 + i) = *(aptr2 + (i + 1 + alength) % alength) + *(aptr2 + (i - 1 + alength) % alength);
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine("Elapsed={0}", sw.Elapsed);
        }


        [TestMethod]
        public void Parallel1d()
        {
            var alength = 16384;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();
            int chunks = 1;
            for (var s = 0; s < 1000; s++)
            {
                Parallel.For(0, chunks, i => ProcStretch1d(i, chunks, ape1, ape2));
                Parallel.For(0, chunks, i => ProcStretch1d(i, chunks, ape2, ape1));
            }

            sw.Stop();
            Debug.WriteLine("Elapsed={0}", sw.Elapsed);
        }


        void ProcStretch1d(int chunk, int max, double[] aRay, double[] bRay)
        {
            var alength = aRay.Length;
            var start = chunk * alength / max;
            var end = chunk * alength / max + alength / max;

            for (var i = start; i < end; i++)
            {
                aRay[i] = bRay[(i + 1 + alength) % alength] + bRay[(i - 1 + alength) % alength];
            }
        }


        [TestMethod]
        public void Parallel2d()
        {
            var alength = 16384;
            var cpl = 0.001;
            var rando = new ThreadSafeRandom();
            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble() * 2 - 1.0;
            }

            for (var k = 0; k < 8; k++)
            {
                sw.Start();
                var chunks = 4;
                for (var s = 0; s < 40; s++)
                {
                    var noise = 0.1 - 0.002 * s;
                    for (var q = 0; q < 50; q++)
                    {
                        var noise1 = noise;
                        Parallel.For(0, chunks, i => ProcStretch2d(i, chunks, ape1, ape2, cpl, noise1, rando));
                        Parallel.For(0, chunks, i => ProcStretch2d(i, chunks, ape2, ape1, cpl, noise1, rando));
                    }
                    Debug.WriteLine("{0}\t{1}\t{2}", s, noise, Correlo(ape1));
                }

                sw.Stop();
                Debug.WriteLine("Elapsed={0}", sw.Elapsed);
                sw.Reset();
            }
        }

        [TestMethod]
        public void ParallelArray2d()
        {
            var alength = 16384;
            var cpl = 0.1;

            var rando = new ThreadSafeRandom();

            var sw = new Stopwatch();
            var randy = new Random();

            var nodeArray = new NodeArray(
                Enumerable.Range(0, alength)
                          .Select(i=> randy.NextDouble() * 2 - 1.0)
                          .ToArray());

            for (var k = 0; k < 8; k++)
            {
                sw.Start();
                for (var s = 0; s < 40; s++)
                {
                    var noise = 0.1 - 0.002 * s;
                    for (var q = 0; q < 50; q++)
                    {
                        nodeArray = NodeArray.UpdateStar(nodeArray, cpl, noise, randy);
                        nodeArray = NodeArray.UpdateStar(nodeArray, cpl, noise, randy);
                    }
                    Debug.WriteLine("{0}\t{1}\t{2}", s, noise, Correlo(nodeArray.Current.ToArray()));
                }

                sw.Stop();
                Debug.WriteLine("Elapsed={0}", sw.Elapsed);
                sw.Reset();
            }
        }



        void ProcStretch2d(int chunk, int max, double[] output, double[] input, double cpl, double noiseLevel, Random randy)
        {
            var alength = output.Length;
            var slength = (int)Math.Sqrt(alength);
            var start = chunk * alength / max;
            var end = chunk * alength / max + alength / max;
            var halfCpl = cpl/2;

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

        public double Correlo(double[] aRay)
        {
            var alength = aRay.Length;
            double vRet = 0;
            for (var i = 0; i < alength; i++)
            {
                vRet += aRay[i] * aRay[(i + 1) % alength];
            }
            return vRet;
        }

        [TestMethod]
        public unsafe void TestMethod22()
        {
            var alength = 16384;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();
            
            for (var s = 0; s < 1000; s++)
            {
                fixed (double* aptr1 = ape1, aptr2 = ape2)
                {
                    for (var i = 0; i < alength; i++)
                    {
                        *(aptr2 + i) = *(aptr1 + (i + 1 + alength) % alength) + *(aptr1 + (i - 1 + alength) % alength);
                    }
                    for (var i = 0; i < alength; i++)
                    {
                        *(aptr1 + i) = *(aptr2 + (i + 1 + alength) % alength) + *(aptr2 + (i - 1 + alength) % alength);
                    }
                }
            }
            sw.Stop();
            Debug.WriteLine("Elapsed={0}", sw.Elapsed);
        }


        [TestMethod]
        public void TestMethod12()
        {
            var alength = 16384;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();

            for (var s = 0; s < 1000; s++)
            {
                for (var i = 0; i < alength; i++)
                {
                    ape1[i] = ape2[(i + 1 + alength) % alength] + ape2[(i - 1 + alength) % alength];
                }
                for (var i = 0; i < alength; i++)
                {
                    ape2[i] = ape1[(i + 1 + alength) % alength] + ape1[(i - 1 + alength) % alength];
                }
            }

            sw.Stop();
            Debug.WriteLine("Elapsed={0}", sw.Elapsed);
        }



    }
}
