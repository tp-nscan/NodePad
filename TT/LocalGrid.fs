namespace TT
open System

module LocalGrid =
    
    type Star(initVal:float32, left:Star, right:Star, top:Star, bottom:Star, row:int, col:int) =
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