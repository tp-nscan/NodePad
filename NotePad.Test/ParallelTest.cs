using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NodePad.Test
{
    [TestClass]
    public class ParallelTest
    {

        [TestMethod]
        public unsafe void TestMethod22p()
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
        public void TestMethod12p()
        {
            var alength = 16384000;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();
            int chunks = 16;
            for (var s = 0; s < 10; s++)
            {
                Parallel.For(0, chunks, i => ProcStretch(i, chunks, ape1, ape2));
                Parallel.For(0, chunks, i => ProcStretch(i, chunks, ape2, ape1));
            }

            sw.Stop();
            Debug.WriteLine("Elapsed={0}", sw.Elapsed);
        }


        void ProcStretch(int chunk, int max, double[] aRay, double[] bRay)
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
        public unsafe void TestMethod22()
        {
            var alength = 1638400;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();
            
            for (var s = 0; s < 10; s++)
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
            var alength = 16384000;

            var ape1 = new double[alength];
            var ape2 = new double[alength];

            var sw = new Stopwatch();
            var randy = new Random();

            for (var i = 0; i < alength; i++)
            {
                ape1[i] = randy.NextDouble();
            }

            sw.Start();

            for (var s = 0; s < 10; s++)
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
