
namespace TT
open System

module GridUtil = 

    let LinMod modulus value =
        ((value % modulus) + modulus) % modulus


    let IndexForGrid (strides:Sz2<int>) (offset:P2<int>) =
        (LinMod strides.Y offset.Y) * strides.X + (LinMod strides.X offset.X)


    let CoordsForGridIndex colCount dex =
        let x = (dex % colCount)
        {P2.X = x; Y = (dex - x) / colCount;}


    let OffsetIndexes (strides:Sz2<int>) (offset:P2<int>) =
        A2dUt.Raster2d strides 
        |> Seq.map(fun pt -> IndexForGrid strides (BT.AddP2 pt offset))
        |> Seq.toArray

    