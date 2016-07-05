using System;
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
    }
}
