

//#load "BT.fs"
//open TT
//
//#time
//let sp = { I.Min= 3.0f; Max = 4.0f }
//let pt = 3.5f
//let ans = BT.InL sp pt
//ans
//let res = BT.Span sp 
//res
//
//
//#load "BT.fs"
//open TT
//
//let mutable res = true
//let sp = { I.Min= 3.0f; Max = 4.0f }
//#time
//for i in 0 .. 100000000 do
//    let pt = 3.4f
//    res <- BT.InL sp pt
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let mutable res = 0.0f
//for i in 0 .. 100000000 do
//    let pt = 3.4f
//    res <- BT.Area rect
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let pt = { P2.X= 3.3f; Y = 4.0f; }
//let mutable res = true
//for i in 0 .. 100000000 do
//    res <- BT.InR rect pt
//
//
//#load "BT.fs"
//open TT
//
//#time
//let rect = { R.MinX= 3.0f; MaxX = 4.0f; R.MinY= 3.0f; MaxY = 4.8f; }
//let pt = { P2.X= 5.3f; Y = 2.0f; }
//let mutable res = rect
//for i in 0 .. 100000000 do
//    res <- BT.StretchRP rect pt

//
#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.3.7.0\lib\net40\MathNet.Numerics.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.FSharp.3.7.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Users\tpnsc_000\Documents\GitHub\HopAlong\Utils\bin\Debug\Utils.dll"

open System
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.LinearAlgebra
open MathNet.Numerics.Random

#load "BT.fs"
#load "Gen.fs"
open TT

let rA = GenA2.RandSF32 {Sz2.X=4; Y=3} 123
A2dUt.GetColumn rA 0

//let k = (GenBT.TestP2 123 230) |> Seq.toArray
//k |> Seq.toArray
(GenBT.TestP2 12 150000) |> Seq.toArray |> BT.BoundingRP BT.AntiRofF32


//
//GenBT.TestI 123 33 |> Seq.toArray

//GenBT.TestR 123 33 |> Seq.toArray

//let k = GenBT.TestLS2 123 3 |> Seq.toArray

//let k = GenBT.TestI 123 3333 
//k |> BT.BoundingII BT.AntiIofS

//let k = GenBT.TestF32 123 3333 
//k |> BT.BoundingIP BT.AntiIofS

//let k = GenBT.TestR 123 3333 
//k |> BT.BoundingRR BT.AntiRofF32






//let a = GenS.NormalF32 (GenV.Twist 5) 0.5f 0.5f 
//        |> Seq.take(10)
//        |> Seq.toArray
//
//let mutable b =  a |> GenS.FlipBSF32Seq 0.5f (GenV.Twist 52)
//
//#time
//for i in 0 .. 1000000 do
//    b <- a |> GenS.FlipBUF32Seq 0.5f (GenV.Twist 52)


//let j = seq {1 .. 10} |> Seq.windowed 3 |> Seq.map(fun v-> (v.[0], v.[1])) |> Seq.toList
//
//#load "BT.fs"
//#load "CT.fs"
//open TT
//
//let jj = {I.Min=0.2f; Max=0.5f;}
//let k = CT.TicMarks jj 22 |> Seq.toArray


//#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.3.7.0\lib\net40\MathNet.Numerics.dll"
//#r @"C:\Users\tpnsc_000\Documents\GitHub\DonutDevil\packages\MathNet.Numerics.FSharp.3.7.0\lib\net40\MathNet.Numerics.FSharp.dll"
//#r @"C:\Users\tpnsc_000\Documents\GitHub\HopAlong\Utils\bin\Debug\Utils.dll"
//
//open System
//open MathNet.Numerics
//open MathNet.Numerics.Distributions
//open MathNet.Numerics.LinearAlgebra
//open MathNet.Numerics.Random
//#load "BT.fs"
//#load "Gen.fs"
//#load "Partition.fs"
//#load "MathNetUtils.fs"
//
//open TT
//
//let res = CT.TileInterval {I.Min=1.0f; Max=3.7f } 4 |> Seq.toArray
//
//let sq =  seq {0.0f .. 10000000.0f }
//let res = sq |> GenBT.IofSeq |> Seq.take(100) |> Seq.toArray


//let mutable res = sq |> SeqUt.SeqToI |> Seq.take(100) |> Seq.toArray


#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"

open System
open System.Windows.Media
open MathNet.Numerics
open MathNet.Numerics.Distributions
open MathNet.Numerics.Random
#load "BT.fs"
#load "Gen.fs"
#load "MathNetUtils.fs"
#load "Partition.fs"
#load "ColorSets.fs"

open TT

ColorSets.RedBlueSpan256
    |> Seq.toArray 
    |> Array.map(fun c -> ColorSets.ColStr c)


ColorSets.QuadColorRing 2 Colors.Black Colors.White Colors.Red Colors.Blue 
    |> Seq.toArray 
    |> Array.map(fun c -> ColorSets.ColStr c)

ColorSets.QuadColorRing 63 Colors.Black Colors.White Colors.Red Colors.Blue |> Seq.toArray |> Array.length

//
//ColorSets.ColorSpan 1 Colors.Black Colors.White |> Seq.toArray |> Array.map(fun c -> ColorSets.ColStr c)
//
//ColorSets.ByteInterp 220uy 120uy 256 128
//
//let q = GenSteps.UniformUbMap 150 0.5f
//
//let st = GenSteps.ExpStepSeq 7.0 14.0 |> Seq.take 14  |> Seq.toArray
//
//let std = st |> Array.map(fun x-> x - 0.1)
//
//
//let tf a =
//    let la = Math.Log a
//    Seq.initInfinite(fun x -> Math.Exp (la * (float x)))
//    |> Seq.take(20) |> Seq.toArray
//    
//// er 1.8
//let er a =
//    let la = Math.Log a
//    Seq.initInfinite(fun x -> Math.Exp (la * Math.Pow( (float x), 0.85 )))
//    |> Seq.take(14) |> Seq.toArray

//
//#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
//#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
//#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"
//
//#load "BT.fs"
//#load "GenBT.fs"
//#load "Gen.fs"
//
//open TT
//
//open System
//open MathNet.Numerics
//open MathNet.Numerics.Distributions
//open MathNet.Numerics.Random
//
//let gg = GenV.Twist 3
//let mappy = F32toF32.BinOfA F32toF32.Step14FromOneTo180
//let mutable res = 3
//#time
//for i in 0 .. 1000000 do
//    res <- mappy (gg.Next() % 400)



//#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
//#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
//#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"
//
//#load "BT.fs"
//#load "GenBT.fs"
//#load "Gen.fs"
//
//open TT
//
//open System
//open MathNet.Numerics
//open MathNet.Numerics.Distributions
//open MathNet.Numerics.Random
//
//let gg = GenV.Twist 3
//let mappy = F32toF32.BinOfL F32toF32.Step14FromOneTo180L
//let mutable res = 3
//#time
//for i in 0 .. 1000000 do
//    res <- mappy (gg.Next() % 400)



#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"

#load "BT.fs"
#load "Gen.fs"
#load "Partition.fs"
#load "MathNetUtils.fs"
#load "MathNetConv.fs"
#load "Grid2dCnxn.fs"
#load "DesignData.fs"
open TT

let bounds = {Sz2.X=5; Y=4}
let aIn = GenMatrix.RandSparseF32 bounds 9 1283
let base64 = MathNetConv.SparseF32MatrixToBase64 aIn
MathNetConv.Base64ToSparseF32Matrix bounds base64

Grid2dCnxn.AllOffsets {Sz2.X=5; Y=4} Grid2dCnxn.StarNbrs |> Seq.toArray
A2dUt.Raster2d {Sz2.X=5; Y=4} |> Seq.toArray
(DesignData.Grid2dGradient {Sz2.X=10; Sz2.Y=15}) |> Seq.toArray
(A2dUt.Raster2d {Sz2.X=4; Sz2.Y=4}) |> Seq.toArray



open System
let g = Guid.Parse("d09fb93d-f22d-4878-94c3-425be1ce270f")


//#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
//#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
//#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"
//
//
//#load "BT.fs"
//#load "Gen.fs"
//open TT
//
//let ts = GenS.NormalSF32 (GenV.Twist 113) 0.0f 0.85f |> Seq.take(1000000) |> Seq.toArray
//let mutable res = 3.0f
//#time
//for j in 0 .. 99 do
//    for i in 0 .. 999999 do
//        res <- NumUt.ModUF32 ts.[i]



#r @"..\packages\MathNet.Numerics.3.11.0\lib\net40\MathNet.Numerics.dll"
#r @"..\packages\MathNet.Numerics.FSharp.3.11.0\lib\net40\MathNet.Numerics.FSharp.dll"
#r @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6\PresentationCore.dll"


#load "BT.fs"
#load "Gen.fs"
#load "NNfunc.fs"

open TT

let tr = GenS.NormalUF32 (GenV.Twist 113) 0.0f 0.85f |> Seq.take(1000000) |> Seq.toArray
let tl = GenS.NormalUF32 (GenV.Twist 113) 0.0f 0.85f |> Seq.take(1000000) |> Seq.toArray

let mutable res = 3.0f
#time
for j in 0 .. 999 do
    for i in 0 .. 999999 do
        res <- NNfunc.ModUFDelta tr.[i] tl.[i]



#load "BT.fs"
open System
open TT

let DiscretP2 (bounds:Sz2<float>) (value:P2<float>) =
    {P2.X = int32 ( NumUt.ModUF(value.X) * bounds.X ); 
        Y = int32 ( NumUt.ModUF(value.Y) * bounds.Y ); }

let Convexo (bounds:Sz2<int>) (thetaOffsets: float -> P2<float>) thetaValues =
    seq { 0 .. 4 * bounds.X - 1 }
    |> Seq.map(fun tic -> 0.5 * (float tic) * Math.PI / (float bounds.X) )
    |> Seq.map(fun v -> let res = DiscretP2 (NumUt.Sz2IntToFloat bounds) (thetaOffsets v)
                        { P2V.X=res.X; Y=res.Y; V=thetaValues v })
    |> Seq.distinctBy(fun v -> (v.X, v.Y))

let CirculoPts (bounds:Sz2<int>) (center:P2<float>) (radius:float) =
    let pos = fun i-> {P2.X = center.X + radius * Math.Cos(i); 
                            Y = center.Y + radius * Math.Sin(i); }
    Convexo bounds pos (fun i -> (float32 (Math.Sin(i))))


let boundo = {Sz2.X=16; Y=17}
let centro = {P2.X=0.6; Y=0.7}

let aa = CirculoPts boundo centro 0.4 |> Seq.toArray

aa |> Array.length