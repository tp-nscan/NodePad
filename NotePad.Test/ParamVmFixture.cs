using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodePad.Model.P;
using NodePad.ViewModel.Common.ParamVm;
using TT;

namespace NodePad.Test
{
    [TestClass]
    public class ParamVmFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pg = Params.Godzilla;

            var vm = new ParamGroupVm(pg, String.Empty);

            var pg2 = vm.UpdatedParamGroup;

            var chump = Params.GetAllParams(pg).ToList();
            var chump2 = Params.GetAllParams(pg2).ToList();
        }

        [TestMethod]
        public void TestMethod2()
        {
            const int COUNT = 32, structSz = 2 << 16;
            int memSz = structSz * System.Runtime.InteropServices.Marshal.SizeOf(typeof(float));

            float[] nodeSetSrc = new float[structSz];
            float[] nodeSetDest = new float[structSz];

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();

            for (var i = 0; i < COUNT; i++)
                Buffer.BlockCopy(nodeSetSrc, 0, nodeSetDest, 0, memSz);

            sw.Stop();
            Console.WriteLine("Buffer.BlockCopy: {0:N0} ticks",
                sw.ElapsedTicks);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // Thread-Local variable that yields a name for a thread
            ThreadLocal<string> ThreadName = new ThreadLocal<string>(() =>
            {
                return "Thread" + Thread.CurrentThread.ManagedThreadId;
            });

            // Action that prints out ThreadName for the current thread
            Action action = () =>
            {
                // If ThreadName.IsValueCreated is true, it means that we are not the
                // first action to run on this thread.
                bool repeat = ThreadName.IsValueCreated;

                Console.WriteLine("ThreadName = {0} {1}", ThreadName.Value, repeat ? "(repeat)" : "");
            };

            // Launch eight of them.  On 4 cores or less, you should see some repeat ThreadNames
            Parallel.Invoke(action, action, action, action, action, action, action, action, action, action, action, action, action, action);

            // Dispose when you are done
            ThreadName.Dispose();
        }

        [TestMethod]
        public void TestMethod4()
        {
            // Thread-Local variable that yields a name for a thread
            ThreadLocal<string> ThreadName = new ThreadLocal<string>(() =>
            {
                return "Thread" + Thread.CurrentThread.ManagedThreadId;
            });

            // Action that prints out ThreadName for the current thread
            Action action = () =>
            {
                // If ThreadName.IsValueCreated is true, it means that we are not the
                // first action to run on this thread.
                bool repeat = ThreadName.IsValueCreated;

                Console.WriteLine("ThreadName = {0} {1}", ThreadName.Value, repeat ? "(repeat)" : "");
            };

            // Launch eight of them.  On 4 cores or less, you should see some repeat ThreadNames
            Parallel.Invoke(action, action, action, action, action, action, action, action, action, action, action, action, action, action);

            // Dispose when you are done
            ThreadName.Dispose();
        }

        [TestMethod]
        public void TestMethod5()
        {
            RunBatch();
        }

        async void RunBatch()
        {
            int Stride = 32;
            var jobs = 4;
            var GridStrides = new Sz2<int>(Stride, Stride);
            var nodeGrid = NodeProcs.RandNodeGrid(GridStrides, 123, 456);
            var localOrders = ColUtils.SubSeqs(jobs, GridStrides.Count() / jobs)
                .Select(t => t.ToList())
                .ToList();
            var trackedTasks = new List<Task<int>>();
            for (var i = 0; i < jobs; i++)
            {
                var i1 = i;
                trackedTasks.Add(Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    return i1;
                }));
            }
            await Task.WhenAll(trackedTasks);
            var tot = trackedTasks.Sum(tt => tt.Result);
        }

        //async Task WorkerMainAsync()
        //{
        //    SemaphoreSlim ss = new SemaphoreSlim(10);
        //    List<Task> trackedTasks = new List<Task>();
        //    while (DoMore())
        //    {
        //        await ss.WaitAsync();
        //        trackedTasks.Add(Task.Run(() =>
        //        {
        //            DoPollingThenWorkAsync();
        //            ss.Release();
        //        }));
        //    }
        //    await Task.WhenAll(trackedTasks);
        //}

        //void DoPollingThenWorkAsync()
        //{
        //    var msg = Poll();
        //    if (msg != null)
        //    {
        //        Thread.Sleep(2000); // process the long running CPU-bound job
        //    }
        //}



        [TestMethod]
        public unsafe void TestStackalloc()
        {
            const int arraySize = 950000;
            float* fib = stackalloc float[arraySize];
            float* p = fib;
            // The sequence begins with 1, 1.
            *p++ = *p++ = 1;
            for (int i = 2; i < arraySize; ++i, ++p)
            {
                // Sum the previous two numbers.
                *p = p[-1] - p[-2];
            }
            for (var i = arraySize -10; i < arraySize; ++i)
            {
                Console.WriteLine(fib[i]);
            }
        }


        [TestMethod]
        public unsafe void TestFixed()
        {
            const int arraySize = 950000;
            float* fib = stackalloc float[arraySize];
            float* p = fib;
            // The sequence begins with 1, 1.
            *p++ = *p++ = 1;
            for (int i = 2; i < arraySize; ++i, ++p)
            {
                // Sum the previous two numbers.
                *p = p[-1] - p[-2];
            }
            for (var i = arraySize - 10; i < arraySize; ++i)
            {
                Console.WriteLine(fib[i]);
            }
        }


    }
}
