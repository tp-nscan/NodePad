namespace TT
open System

module NumUt =

    let Epsilon = 0.00001f

    let Fraction32Of (num:int) (frac:float32) =
        System.Convert.ToInt32(((float32 num) * frac))


    let AbsF32 (v:float32) =
        if v < 0.0f then -v
        else v


    let MapUF (min:float) (max:float) (value:float) =
        let span = max-min
        min + value*span


    let inline AddInRange min max a b =
        let res = a + b
        if res < min then min
        else if res > max then max
        else res

    let inline FlipWhen a b flipProb draw current =
        match (flipProb < draw) with
        | true -> if a=current then b else a
        | false -> current


    let ToUF value =
        if value < 0.0f then 0.0f
        else if value > 1.0f then 1.0f
        else value


    let ToSF value =
        if value < -1.0f then -1.0f
        else if value > 1.0f then 1.0f
        else value


    let rec ModUF32 value =
        if ((value > 1.0f) || (value < 0.0f) ) then 
            value - (float32 (Math.Floor (float value)))
        else
            value


    let rec ModUF value =
        if ((value > 1.0) || (value < 0.0) ) then 
            value - Math.Floor(value)
        else
            value


    let FloatToUF value =
        if value < 0.0 then 0.0f
        else if value > 1.0 then 1.0f
        else (float32 value)


    let FloatToSF value =
        if value < -1.0 then -1.0f
        else if value > 1.0 then 1.0f
        else (float32 value)


    let inline AorB a b thresh value =
        if value < thresh then a
        else b


