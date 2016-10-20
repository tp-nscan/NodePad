using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using TT;

namespace NodePad.Model.P
{
    public class LocalData
    {
        public LocalData(NodeGrid nodeGrid, List<P1V<int, float>> results = null)
        {
            NodeGrid = nodeGrid;
            Results = results ?? new List<P1V<int, float>>();
        }

        public NodeGrid NodeGrid { get; }
        public List<P1V<int, float>> Results { get; }
    }

    public class LocalData2
    {
        public LocalData2(List<P1V<int, float>> results = null)
        {
            Results = results ?? new List<P1V<int, float>>();
        }
        
        public List<P1V<int, float>> Results { get; }
    }

    public static class NodeProcs
    {
        public static IEnumerable<P2V<int, float>> DataToP2Vs(this NodeGrid nodeGrid)
        {
            return Enumerable.Range(0, nodeGrid.Strides.Count())
                .Select(i =>
                {
                    var coords = GridUtil.CoordsForGridIndex(nodeGrid.Strides.X, i);
                    return new P2V<int, float>(coords.X, coords.Y, nodeGrid.Values[i]);
                });
        }

        public static IEnumerable<P2V<int, float>> DataToP2Vs(this NodeArray nodeArray, Sz2<int> bounds)
        {
            var index = 0;
            for (var x = 0; x < bounds.X; x++)
            {
                for (var y = 0; y < bounds.X; y++)
                {
                    yield return new P2V<int, float>(x, y, (float) nodeArray.Current[index++]);
                }
            }
        }

        public static IEnumerable<P1V<int, float>> DataToP1Vs(this NodeGrid nodeGrid)
        {
            return Enumerable.Range(0, nodeGrid.Strides.Count())
                .Select(i => new P1V<int, float>(i, nodeGrid.Values[i]));
        }

        public static NodeGrid RandNodeGrid(Sz2<int> bounds, int initSeed, int updateSeed)
        {
            var nodeVals = GenS.SeqOfRandUF32(GenV.Twist(initSeed)).Take(bounds.Count()).ToArray();
            var tuples = Enumerable.Range(0, bounds.Count()).Select(i => new P1V<int, float>(i, nodeVals[i]));

            return new NodeGrid(
                        strides: bounds,
                        tuples: tuples,
                        generation: 0,
                        seed: updateSeed
                    );
        }


        public static P1V<int, float> Update(this NodeGrid nodeGrid, int index, float step, float noiseLevel)
        {
            var curValue = nodeGrid.Values[index];
            var deltaL = NNfunc.ModUFDelta(curValue, nodeGrid.Values[nodeGrid.Left[index]]);
            var deltaR = NNfunc.ModUFDelta(curValue, nodeGrid.Values[nodeGrid.Right[index]]);
            var deltaT = NNfunc.ModUFDelta(curValue, nodeGrid.Values[nodeGrid.Top[index]]);
            var deltaB = NNfunc.ModUFDelta(curValue, nodeGrid.Values[nodeGrid.Bottom[index]]);

            var ptb = NumUt.ModUF32(
                curValue + 
                noiseLevel * nodeGrid.Noise[index] + 
                step * (
                          deltaL +
                          deltaR +
                          deltaT +
                          deltaB
                        )
                );

            return new P1V<int, float>(index, ptb);
        }


        public static IEnumerable<P1V<int, float>> UpdateSeveral(this NodeGrid nodeGrid, IEnumerable<int> indexes, float step, float noiseLevel)
        {
            return indexes.Select(dex => nodeGrid.Update(dex, noiseLevel, step));
        }


        public static NodeGrid MakeNextGen(this NodeGrid nodeGrid, IEnumerable<P1V<int, float>> newVals)
        {
            return new NodeGrid(nodeGrid.Strides, newVals, nodeGrid.Generation + 1, nodeGrid.NextSeed);
        }


        public static NodeGrid Copy(this NodeGrid nodeGrid)
        {
            return new NodeGrid(nodeGrid.Strides, nodeGrid.DataToP1Vs(), nodeGrid.Generation, nodeGrid.Seed);
        }


        public static NodeGrid Update(this NodeGrid nodeGrid, float step, float noiseLevel)
        {
            var res = Enumerable.Range(0, nodeGrid.Strides.Count())
                                .Select(i => nodeGrid.Update(i, noiseLevel, step));

            return new NodeGrid(nodeGrid.Strides, res, nodeGrid.Generation + 1, nodeGrid.NextSeed);
        }

        public static NodeGrid UpdateP(this NodeGrid nodeGrid, float step, float noiseLevel)
        {
            var myLock = new object();
            var jobs = 4;
            var localOrders = ColUtils.SubSeqs(jobs, nodeGrid.Strides.Count()/ jobs)
                .Select(t=>t.ToList())
                .ToList();
            var results = new List<P1V<int, float>>();

                Parallel.ForEach(
                    localOrders,
                    () => new LocalData(nodeGrid),
                    (indexes, loopstate, loco) =>
                    {
                        //Console.WriteLine("{0}:{1}", Thread.CurrentThread.ManagedThreadId, url);
                        //return ng.Update(index, step, noiseLevel);
                        var retVal = new LocalData(nodeGrid, nodeGrid.UpdateSeveral(indexes, step, noiseLevel)
                            .ToList());
                        return retVal;
                    },
                    (localRes) =>
                    {
                        lock (myLock)
                        {
                            var res = localRes.Results;
                            results.AddRange(res);
                        }
                    });

            if (results.Count == nodeGrid.Strides.Count())
            {
                nodeGrid = nodeGrid.MakeNextGen(results);
            }
            else
            {
                //nodeArray = nodeArray.MakeNextGen(results);
            }

            return nodeGrid;

        }


        public static async Task<NodeGrid> UpdateP2(this NodeGrid nodeGrid, float step, float noiseLevel)
        {
            var jobs = 4;
            var localOrders = ColUtils.SubSeqs(jobs, nodeGrid.Strides.Count() / jobs)
                .Select(t => t.ToList())
                .ToList();
            var trackedTasks = new List<Task<List<P1V<int, float>>>>();
            for (var i = 0; i < jobs; i++)
            {
                var i1 = i;
                var ng = nodeGrid.Copy();
                trackedTasks.Add(
                    Task.Run(
                        () => ng.UpdateSeveral(localOrders[i1], step, noiseLevel).ToList())
                );
            }

            await Task.WhenAll(trackedTasks);
            var results = new List<P1V<int, float>>();
            foreach (var tt in trackedTasks)
            {
                results.AddRange(tt.Result);
            }

            return nodeGrid.MakeNextGen(results);
        }

        public static NodeArray RandNodeArray(Sz2<int> bounds, Random randy)
        {
            return new NodeArray(Enumerable.Range(0, bounds.Count())
                          .Select(i => randy.NextDouble() * 2 - 1.0)
                          .ToArray());
        }



        public static async Task<NodeArray> UpdateFred(this NodeArray nodeArray, double cpl, double noise, Random randy)
        {
            NodeArray result = nodeArray;
            await Task.Run(() => { result = NodeArray.UpdateStar(nodeArray, cpl, noise, randy); });
            return result;
        }


        public static NodeGrid UpdateP3(this NodeGrid nodeGrid, float step, float noiseLevel)
        {
            var jobs = 4;
            var localOrders = ColUtils.SubSeqs(jobs, nodeGrid.Strides.Count() / jobs)
                .Select(t => t.ToList())
                .ToList();

            Task<List<P1V<int, float>>> t0 = Task.Run(() => 
                nodeGrid.UpdateSeveral(localOrders[0], step, noiseLevel).ToList());

            Task<List<P1V<int, float>>> t1 = Task.Run(() =>
                nodeGrid.UpdateSeveral(localOrders[1], step, noiseLevel).ToList());

            Task<List<P1V<int, float>>> t2 = Task.Run(() =>
                nodeGrid.UpdateSeveral(localOrders[2], step, noiseLevel).ToList());

            Task<List<P1V<int, float>>> t3 = Task.Run(() =>
                nodeGrid.UpdateSeveral(localOrders[3], step, noiseLevel).ToList());

            Task.WaitAll(t0, t1, t2, t3);

            var results = new List<P1V<int, float>>();
            results.AddRange(t0.Result);
            results.AddRange(t1.Result);
            results.AddRange(t2.Result);
            results.AddRange(t3.Result);

            return nodeGrid.MakeNextGen(results);
        }

    }




}
