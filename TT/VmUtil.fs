namespace TT
open System

module VmUtil =

    let NumberFormat (min:float32) (max:float32) =
        let diff = max - min
        match diff with
        | v when (v > 100.0f) -> "0"
        | v when (v > 10.0f) -> "0.0"
        | v when (v > 1.0f) -> "0.00"
        | v when (v > 0.10f) -> "0.000"
        | v when (v > 0.01f) -> "0.0000"
        | _ -> "0.00000"


    let TicFrequency (min:float32) (max:float32) =
        let diff = max - min
        match diff with
        | v when (v > 100.0f) -> 10.0
        | v when (v > 10.0f) -> 1.0
        | v when (v > 1.0f) -> 0.10
        | v when (v > 0.10f) -> 0.0010
        | v when (v > 0.01f) -> 0.00010
        | _ -> 0.000010