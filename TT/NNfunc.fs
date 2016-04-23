namespace TT
open System

module NNfunc =

    let ModUFDelta lhs rhs =
        let delta = rhs - lhs
        if (lhs > rhs) then
            if (delta >= -0.5f) then delta else (1.0f + delta)
        else
            if (delta > 0.5f) then (delta - 1.0f) else delta


    let Correlo (s:float32) (k:float32) m n =
        let dp = (m-k) * (m-k) + (n+k) * (n+k)
        let dn = (m+k) * (m+k) + (n-k) * (n-k)
        let ep = Math.Exp(float  (s * (-dp)))
        let en = Math.Exp(float  (s * (-dn)))
        float32 ( 0.5 * ( ep + en ) )


    let MStretchO (s:float32) (k:float32) =
        A2dUt.A2dSF2 (A2dUt.MemoSF2 300 (Correlo s k))


    type Cached() =
        static let _stretchO =
            A2dUt.MemoSF2 400 (Correlo 60.0f 0.1f)

        static member StretchO =
            A2dUt.A2dSF2 _stretchO

        static member StretchC x y =
            A2dUt.A2dSF2 _stretchO x y