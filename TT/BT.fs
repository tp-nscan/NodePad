namespace TT
open System

type P2<'c>  = { X:'c; Y:'c }
type Sz2<'c>  = { X:'c; Y:'c }
type Sz4<'c>  = { X1:'c; Y1:'c; X2:'c; Y2:'c }
type P3<'c>  = { X:'c; Y:'c; Z:'c }
type I<'c>  = { Min:'c; Max:'c }
type R<'c>  = { MinX:'c; MaxX:'c; MinY:'c; MaxY:'c } 
type LS2<'c>  = { X1:'c; Y1:'c; X2:'c; Y2:'c } 

type P1V<'c,'v>  = { X:'c; V:'v }
type P2V<'c,'v>  = { X:'c; Y:'c; V:'v }
type P3V<'c,'v>  = { X:'c; Y:'c; Z:'c; V:'v }
type IV<'c,'v>  = { Min:'c; Max:'c; V:'v  } 
type RV<'c,'v>  = { MinX:'c; MaxX:'c; MinY:'c; MaxY:'c; V:'v } 
type LS2V<'c,'v>  = { X1:'c; Y1:'c; X2:'c; Y2:'c; V:'v } 

module BTconst =

    let AntiIofS = 
        { 
            I.Min = System.Single.PositiveInfinity; 
            Max = System.Single.NegativeInfinity; 
        }

    let AntiIofInt = 
        { 
            I.Min = System.Int32.MaxValue; 
            Max = System.Int32.MinValue;
        }

    let AntiRofF32 = 
        { 
            R.MinX = System.Single.PositiveInfinity; 
            R.MaxX = System.Single.NegativeInfinity; 
            MinY = System.Single.PositiveInfinity;  
            MaxY = System.Single.NegativeInfinity; 
        }

    let AntiRofInt = 
        { 
            R.MinX = System.Int32.MaxValue; 
            R.MaxX = System.Int32.MinValue; 
            MinY = System.Int32.MaxValue;  
            MaxY = System.Int32.MinValue; 
        }


module BTInline = 

    // makes an interval from arbitrary values with the expected ordering
    let inline RegularI< ^a when ^a: comparison> (x1:^a) (x2:^a) =
        if x1 < x2 then
            { I.Min=x1; I.Max=x2 }
        else 
            { I.Min=x2; I.Max=x1 }
            

    // makes a rectangle from arbitrary values with the expected orderings
    let inline RegularR< ^a when ^a: comparison> (x1:^a) (x2:^a) (y1:^a) (y2:^a) =
        if x1 < x2 then
            if y1 < y2 then
                { R.MinX=x1; R.MaxX=x2; R.MinY=y1; R.MaxY=y2; }
            else 
                { R.MinX=x1; R.MaxX=x2; R.MinY=y2; R.MaxY=y1; }
        else 
            if y1 < y2 then
                { R.MinX=x2; R.MaxX=x1; R.MinY=y1; R.MaxY=y2; }
            else 
                { R.MinX=x2; R.MaxX=x1; R.MinY=y2; R.MaxY=y1; }


    let inline walk_the_creature_2 (creature:^a when ^a:(member Walk : unit -> unit)) =
        (^a : (member Walk : unit -> unit) creature)


    let inline Span (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) =
       (^a : (member Max : ^b) range) - (^a : (member Min : ^b) range)


    let inline Mid (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) 
                   (p1:^b) =
       ((^a : (member Max : ^b) range) + (^a : (member Min : ^b) range) ) / 2


    let inline InI (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) 
                   (tv:^b) =
       ((^a : (member Max : ^b) range) >= tv) && ((^a : (member Min : ^b) range) <= tv)


    let inline ClipToI (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b)) 
                       (tv:^b) =
        if tv < (^a : (member Min : ^b) range)  then (^a : (member Min : ^b) range) 
        else if tv > (^a : (member Max : ^b) range)  then (^a : (member Max : ^b) range) 
        else tv


    let inline AddInIV (range:^a when ^a:(member Min : ^b) and ^a:(member Max : ^b) and ^a:(member V : ^b))  
                       (delta:^b) =
        let res = (^a : (member V : ^b) range) + delta
        if res < (^a : (member Min : ^b) range)  then (^a : (member Min : ^b) range) 
        else if res > (^a : (member Max : ^b) range)  then (^a : (member Max : ^b) range) 
        else res

//
//    let inline AddInRangeI bounds a b =
//        let res = a + b
//        if res < bounds.Min then bounds.Min
//        else if res > bounds.Max then bounds.Max
//        else res



    let inline Area (range:^a when 
                        ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) 
                        and
                        ^a:(member MinY : ^b) and ^a:(member MaxY : ^b)
                    ) =

       ((^a : (member MaxX : ^b) range) - (^a : (member MinX : ^b) range))
       *
       ((^a : (member MaxY : ^b) range) - (^a : (member MinY : ^b) range))


    let inline SpanX (range:^a when ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) ) =
       ((^a : (member MaxX : ^b) range) - (^a : (member MinX : ^b) range))


    let inline SpanY (range:^a when ^a:(member MinY : ^b) and ^a:(member MaxY : ^b) ) =
       ((^a : (member MaxY : ^b) range) - (^a : (member MinY : ^b) range))


    let inline InRP (range:^a when 
                        ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) 
                        and
                        ^a:(member MinY : ^b) and ^a:(member MaxY : ^b)
                    ) 
                    (p2:^c when 
                        ^c:(member X : ^b) and ^c:(member Y : ^b) 
                    ) =

       (^a : (member MaxX : ^b) range) >= (^c : (member X : ^b) p2)
       &&
       (^a : (member MinX : ^b) range) <= (^c : (member X : ^b) p2)
       &&
       (^a : (member MaxY : ^b) range) >= (^c : (member Y : ^b) p2)
       &&
       (^a : (member MinY : ^b) range) <= (^c : (member Y : ^b) p2)



    let inline InRV2 (range:^a when 
                        ^a:(member MinX : ^b) and ^a:(member MaxX : ^b) 
                        and
                        ^a:(member MinY : ^b) and ^a:(member MaxY : ^b)
                     ) 
                     (x:^b) (y:^b)  =

       (^a : (member MaxX : ^b) range) >= x
       &&
       (^a : (member MinX : ^b) range) <= x
       &&
       (^a : (member MaxY : ^b) range) >= y
       &&
       (^a : (member MinY : ^b) range) <= y


    let inline StretchIP (range:I< ^b>) (p1:^b ) =
        { 
            I.Min = if (range.Min > p1) then p1 else range.Min
            Max   = if (range.Max < p1) then p1 else range.Max
        }


    let inline StretchII (range:I< ^b>) (iv:^c when ^c:(member Min : ^b)
                                                and ^c:(member Max : ^b) ) =
        { 
            I.Min = if (range.Min > (^c : (member Min : ^b) iv)) then (^c : (member Min : ^b) iv) else range.Min
            Max   = if (range.Max < (^c : (member Max : ^b) iv)) then (^c : (member Max : ^b) iv) else range.Max
        }


    let inline StretchRP (box:R< ^b>) (p2:^c when ^c:(member X : ^b) and ^c:(member Y : ^b) ) =
        { 
            R.MinX = if (box.MinX > (^c : (member X : ^b) p2)) then (^c : (member X : ^b) p2) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member X : ^b) p2)) then (^c : (member X : ^b) p2) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member Y : ^b) p2)) then (^c : (member Y : ^b) p2) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member Y : ^b) p2)) then (^c : (member Y : ^b) p2) else box.MaxY
        }

    let inline StretchRPF32 (box:R<float32>) (p2:P2<float32>) =
        { 
            R.MinX = if (box.MinX > p2.X) then p2.X else box.MinX
            MaxX   = if (box.MaxX < p2.X) then p2.X else box.MaxX
            MinY   = if (box.MinY > p2.Y) then p2.Y else box.MinY
            MaxY   = if (box.MaxY < p2.Y) then p2.Y else box.MaxY
        }

    let inline StretchRR (box:R< ^b>) 
                           (rect:^c when ^c:(member MinX : ^b) and ^c:(member MaxX : ^b) 
                                     and ^c:(member MinY : ^b) and ^c:(member MaxY : ^b)) =
        { 
            R.MinX = if (box.MinX > (^c : (member MinX : ^b) rect)) then (^c : (member MinX : ^b) rect) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member MaxX : ^b) rect)) then (^c : (member MaxX : ^b) rect) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member MinY : ^b) rect)) then (^c : (member MinY : ^b) rect) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member MaxY : ^b) rect)) then (^c : (member MaxY : ^b) rect) else box.MaxY
        }


    let inline StretchRL (box:R< ^b>) 
                           (line:^c when ^c:(member X1 : ^b) and ^c:(member X2 : ^b) 
                                     and ^c:(member Y1 : ^b) and ^c:(member Y2 : ^b)) =
        { 
            R.MinX = if (box.MinX > (^c : (member X1 : ^b) line)) then (^c : (member X1 : ^b) line) else box.MinX
            MaxX   = if (box.MaxX < (^c : (member X2 : ^b) line)) then (^c : (member X2 : ^b) line) else box.MaxX 
            MinY   = if (box.MinY > (^c : (member Y1 : ^b) line)) then (^c : (member Y1 : ^b) line) else box.MinY
            MaxY   = if (box.MaxY < (^c : (member Y2 : ^b) line)) then (^c : (member Y2 : ^b) line) else box.MaxY
        }


    let inline FilterRP (box:R< ^b>) (p2:seq< ^c> when ^c:(member X : ^b) and ^c:(member Y : ^b) ) =
            p2 |> Seq.filter(fun p -> InRP box p)


    let inline BoundingRP (box:R< ^b>) (p2:seq< ^c> when ^c:(member X : ^b) and ^c:(member Y : ^b) ) =
            p2 |> Seq.fold (fun acc elem -> StretchRP acc elem ) box


    let inline BoundRectP2F32 p2s =
            p2s |> Array.fold(fun acc p -> StretchRPF32 acc p ) BTconst.AntiRofF32 


    let inline BoundingRR (box:R< ^b>) 
                            (rects:seq< ^c> when ^c:(member MinX : ^b) and ^c:(member MaxX : ^b) 
                                             and ^c:(member MinY : ^b) and ^c:(member MaxY : ^b)) =
            rects |> Seq.fold (fun acc elem -> StretchRR acc elem ) box


    let inline BoundingRLS2 (box:R< ^b>) 
                            (lines:seq< ^c> when ^c:(member X1 : ^b) and ^c:(member X2 : ^b) 
                                             and ^c:(member Y1 : ^b) and ^c:(member Y2 : ^b)) =
            lines |> Seq.fold (fun acc elem -> StretchRL acc elem ) box


    let inline BoundingIP (range:I< ^b>) (p1:seq< ^b>) =
            p1 |> Seq.fold (fun acc elem -> StretchIP acc elem ) range


    let inline BoundingII (range:I< ^b>) 
                          (sI:seq< ^a> when ^a:(member Min: ^b) 
                                        and ^a:(member Max: ^b)) =
            sI |> Seq.fold (fun acc elem -> StretchII acc elem ) range





module BT =

    let Sz2IntToFloat (s2:Sz2<int>) =
        {Sz2.X = (float s2.X); Y = (float s2.Y)}

    let Sz2IntToFloat32 (s2:Sz2<int>) =
        {Sz2.X = (float32 s2.X); Y = (float32 s2.Y)}

    let Count (sz:Sz2<int>) =
        sz.X * sz.Y

    let MutateP1V p1V f =
        {P1V.X = p1V.X; V = f p1V.V}

    let MutateP2V p2V f =
        {P2V.X = p2V.X; Y= p2V.Y; V = f p2V.V}

    let MutateP3V p3V f =
        {P3V.X = p3V.X; Y=p3V.Y; Z=p3V.Z; V = f p3V.V}

    let MutateIV iV f =
        {IV.Min = iV.Min; Max = iV.Max; V = BTInline.ClipToI iV f}

    let MutateRV rV f =
        {RV.MinX=rV.MinX; MinY = rV.MinY; MaxX=rV.MaxX; MaxY=rV.MaxY; V = f rV.V}

    let MutateLS2V ls2V f =
        {LS2V.X1=ls2V.X1; LS2V.X2=ls2V.X2; LS2V.Y1=ls2V.Y1; LS2V.Y2=ls2V.Y2; V = f ls2V.V}
        


module AUt =

    let Hamming s1 s2 = Array.map2((=)) s1 s2 |> Seq.sumBy(fun b -> if b then 0 else 1)


    let inline CompareArrays<'a> comp (seqA:'a[]) (seqB:'a[]) =
        Seq.fold (&&) true (Seq.zip seqA seqB |> Seq.map (fun (aa,bb) -> comp aa bb))


    let inline LenSq zeroVal (array:'a[]) =
        array |> Array.fold(fun acc v -> acc + v*v) zeroVal  


    let Len (array:float32[]) =
        array |> LenSq 0.0f
              |> float
              |> sqrt
              |> float32


    let ScaleF32 (scale:float32) (array:float32[]) =
        array |> Array.map(fun v -> scale *v)


    let Scale (scale:float) (array:float32[]) =
        array |> ScaleF32 (float32 scale)



module A2dUt = 
    
    let BoundsOf<'a> (a2: 'a[,]) =
        {Sz2.X=a2.GetLength(1); Y=a2.GetLength(0)}


    let Count<'a> (a2: 'a[,]) =
        a2.GetLength(1) * a2.GetLength(0)


    let Raster2d (strides:Sz2<int>) =
        seq { for col in 0 .. strides.X - 1 do
                for row in 0 .. strides.Y - 1 do
                    yield {P2.X= col; Y= row;}
            }

        
    let MemoSF2 stride f =
        let hs = stride / 2
        let hsf = float32 hs
        let discr x y =
            let xf = (float32 (x - hs)) / hsf
            let yf = (float32 (y - hs)) / hsf
            f xf yf
        Array2D.init stride stride 
                (fun row col ->(discr col row))


    let A2dSF2 (a:float32[,]) =
        let sh = a.GetLength(0)  / 2
        let shf = (float32 sh)
        fun x y ->
            a.[(int (x * shf)) + sh, (int (y * shf)) + sh]
        

    let SymRowMajorIndex x y =
        if (x >= y) then x*(x+1)/2 + y
        else y*(y+1)/2 + x


    let UtCoords stride =
       let rec qq stride coords =
           seq {
                    match coords with
                    | a, b when b < a -> 
                        yield (a, b)
                        yield! (qq stride (a, b + 1))
                    | a, b when b < (stride- 1) -> 
                        yield (a, b)
                        yield! qq stride (a + 1, 0)
                    | a, b ->
                        yield (a, b)
           }
       seq {
             yield!  qq stride (0, 0)
       }
         

    // Creates a 1D array that represents a 2d upper triangular matrix
    let UpperTriangulate sqSize f =
        let cached = (UtCoords sqSize) 
                        |> Seq.map(fun (a,b) -> f a b)
                        |> Seq.toArray                            
        (fun x y -> cached.[SymRowMajorIndex x y] )


  // Creates a 1D array that represents a 2d upper triangular matrix with uniform diagonal
    let UpperTriangulateDiag (diagVal:'a) stride f =
        let cached = (UtCoords stride) 
                        |> Seq.map(fun (a,b) -> 
                                        match (a,b) with
                                        | (a,b) when a=b -> diagVal
                                        | (a,b) -> f a b)
                        |> Seq.toArray                            

        (fun x y -> cached.[SymRowMajorIndex x y] )


    let inline SetUniformDiagonal (mtx:'a[,]) (diagVal) = 
        Array2D.init (mtx.GetLength 0) 
                     (mtx.GetLength 1) 
                     (fun x y -> if(x=y) then diagVal  else mtx.[x,y])


    let Transpose (mtx : _ [,]) = 
        Array2D.init (mtx.GetLength 1) (mtx.GetLength 0) (fun x y -> mtx.[y,x])
    

    let GetRowsForArray2D (array:'a[,]) =
      let rowC = array.GetLength(0)
      let colC = array.GetLength(1)
      Array.init rowC (fun i ->
        Array.init colC (fun j -> array.[i,j]))


    let FillRowMajor rowCount colCount (vals:seq<'a>) =
        let data = vals |> Seq.take(rowCount*colCount) |> Seq.toArray
        Array2D.init rowCount colCount (fun x y -> data.[y+x*colCount])
    

    let FillColumnMajor rowCount colCount (vals:seq<'a>) =
        let data = vals |> Seq.take(rowCount*colCount) |> Seq.toArray
        Array2D.init rowCount colCount (fun x y -> data.[x+y*colCount])


    let GetRow (A:'a[,]) (rowNum:int) = 
        seq { 0 .. A.GetLength(1) - 1 } 
            |> Seq.map(fun i -> A.[rowNum, i])
            |> Seq.toArray


    let GetColumn (A:'a[,]) (colNum:int) = 
        seq { 0 .. A.GetLength(0) - 1 } 
            |> Seq.map(fun i -> A.[i, colNum])
            |> Seq.toArray


    let flattenRowMajor (A:'a[,]) = A |> Seq.cast<'a>


    let flattenColumnMajor (A:'a[,]) = A |> Transpose |> Seq.cast<'a>


    let getColumn c (A:_[,]) = A.[*,c] |> Seq.toArray


    let getRow r (A:_[,]) = A.[r,*] |> Seq.toArray


    let P2sForA2d (rowCt:int) (colCt:int) =
        seq { for row in 0 .. rowCt - 1 do
                for col in 0 .. colCt - 1 do
                    yield {P2.X = col; Y=row; }
        }


    let MemberByP2<'a> (ar: 'a[,]) (loc:P2<int>) =
        ar.[loc.Y, loc.X]


    let ToP2V<'a> (a2d:'a[,]) =
        seq { for row in 0 .. (a2d.GetLength 0) - 1 do
                for col in 0 .. (a2d.GetLength 1) - 1 do
                    yield {P2V.X = col; Y=row; V= a2d.[row, col]}
            }


    let FromP2V<'a> (bounds:Sz2<int>) (p2vs:seq<P2V<int,'a>>) =
        let array = Array2D.zeroCreate<'a> bounds.Y bounds.X
        let cumer (acc:'a[,]) (nuVal:P2V<int,'a>) = 
                  acc.[nuVal.Y, nuVal.X] <- nuVal.V
                  acc
        p2vs |> Seq.fold (fun (acc: 'a[,]) (v:P2V<int,'a>) -> cumer acc v) array
        |> ignore
        array



module SeqUt = 

    let IterB (s:System.Collections.Generic.IEnumerator<'a>) =
        function () ->
                    s.MoveNext() |> ignore
                    s.Current


    let inline ZipMap f a b = Seq.zip a b |> Seq.map (fun (x,y) -> f x y)
   

    let AddInRange min max offsets baseSeq =
        Seq.map2 (NumUt.AddInRange min max) baseSeq offsets


    let AddInUF32 offsets baseSeq =
        Seq.map2 (NumUt.AddInRange 0.0f 1.0f) baseSeq offsets


    let AddInSF32 offsets baseSeq =
        Seq.map2 (NumUt.AddInRange -1.0f 1.0f) baseSeq offsets
