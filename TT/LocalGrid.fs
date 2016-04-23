namespace TT
open System

module LocalGrid =
    
    type P2MV =
     { 
        Row:int
        Col:int
        mutable V:float32
     }

     type Star =
      {
        Top : P2MV
        Left : P2MV
        Center : P2MV
        Right : P2MV
        Bottom : P2MV
      }

    let P2MVFromValuator (bounds:Sz2<int>) valuator =
        Array2D.init bounds.Y bounds.X (fun r c -> {P2MV.Row = r; Col=c; V=(valuator r c)}) 


    let P2MVFromSequence (bounds:Sz2<int>) (vals:seq<float32>) =
        let data = vals |> Seq.take(BT.Count bounds) |> Seq.toArray
        let valuator row col = data.[row*bounds.X + col]
        P2MVFromValuator bounds valuator


    let P2MVFromArray2d (vals:float32[,]) =
        let valuator row col = vals.[row, col]
        P2MVFromValuator (A2dUt.BoundsOf vals) valuator


    type MakeStar(initVal:float32, left:Star, right:Star, top:Star, bottom:Star, row:int, col:int) =
        let mutable curVal = initVal
        let mutable newVal = 0.0f
        let Left = left
        member this.GetVal = curVal


    type Star2(initVal:float32, row:int, col:int) =
        let mutable curVal = initVal
        let mutable newVal = 0.0f
        let mutable _star = None
        let _row = row
        let _col = col
        member this.GetVal = curVal
        member this.Row = _row
        member this.Col = _col