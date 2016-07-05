using System.Collections.Generic;
using System.Linq;
using TT;

namespace NodePad.Model.P
{
    public static class NodeProcs
    {
        public static IEnumerable<P2V<int, float>> CurValuesAsP2Vs(this NodeGrid nodeGrid)
        {
            return Enumerable.Range(0, nodeGrid.Strides.Count())
                .Select(i =>
                {
                    var coords = GridUtil.CoordsForGridIndex(nodeGrid.Strides.X, i);
                    return new P2V<int, float>(coords.X, coords.Y, nodeGrid.Values[i]);
                });
        }

        public static NodeGrid RandNodeGrid(Sz2<int> bounds, int initSeed, int updateSeed)
        {
            return new NodeGrid(
                        strides: bounds,
                        values: GenS.SeqOfRandUF32(GenV.Twist(initSeed)).Take(bounds.Count()).ToArray(),
                        generation: 0,
                        noise: GenS.NormalSF32(GenV.Twist(updateSeed), 0.0f, 1.0f)
                    );
        }
    }
}
