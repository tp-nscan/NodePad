namespace TT
open System
open System.Windows.Media


type ColorLeg2I<'a> = 
    { exteriorC:Color; spanC:Color[,]; mapper:P2<'a> -> P2<int>; rangeV:R<'a>; }

module ColorSets2I = 

    let Transp (c:Color) (transp:int) = 
        Color.FromArgb((byte transp), c.R, c.G, c.B)


    let ExtendColors (ca:Color[]) =
        Array2D.init (ca.GetLength(0)) 256 (fun i j -> Transp ca.[i] j)


    let ExtendMap (mapper:'a->int) (transpMap:'a->int) =
        fun (v:P2<'a>) -> {P2.X= mapper v.X; Y= transpMap v.Y}

    let ExtendMap2<'a> (mapper:'a->int) (transpMap:'a->int) =
        fun a  b -> {P2.X= mapper a; Y= transpMap b}


    let QuadColorUFLeg2I =
        { exteriorC=Colors.Orange
          spanC= ExtendColors ColorSets.QuadColorSpan256 
          mapper= ExtendMap Partition.UF32to256 Partition.UF32to256
          rangeV= {R.MinX=0.0f; MaxX=1.0f; MinY=0.0f; MaxY=1.0f;}
        }


    let inline GetLeg2IColorF32 (lm:ColorLeg2I<float32>) (value:P2<float32>) =
        if (BT.InRP lm.rangeV value) then  
            ( lm.mapper value ) |> A2dUt.MemberByP2 lm.spanC
        else lm.exteriorC


    let GetLegMidVal (lm:ColorLeg2I<float32>) =
         lm.rangeV.MaxX - lm.rangeV.MinX