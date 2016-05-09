namespace TT
open System

type ParamType = Int=0 | F32=1

type Pb<'a> = {mutable data:'a; descr:string; pt:ParamType}

module Params = 

    let DisplayFreqParam = {Pb.data={IV.Min=0; Max=50; V=10;}; descr="Display Frequency"; pt=ParamType.Int}
    let NoiseParam = {Pb.data={IV.Min=0.0f; Max=0.5f; V=0.25f;}; descr="Noise"; pt=ParamType.F32}
    let StepSizeParam = {Pb.data={IV.Min=0.0f; Max=0.3f; V=0.01f;}; descr="Step Size"; pt=ParamType.F32}
    let NoiseFieldDecayParam = {Pb.data={IV.Min=0.0f; Max=0.9f; V=0.3f;}; descr="Noise field decay"; pt=ParamType.F32}


//module ParamOps =

//    let inline AdjustParam (param:Param<'c>) (delta:'c) =
//        {Range = param.Range; Param.CurVal = (NumUt.AddInRange param.Range.Min param.Range.Max param.CurVal delta) }




