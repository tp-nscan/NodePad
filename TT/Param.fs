namespace TT
open System

type ParamType = Int=0 | F32=1

type Pb<'a> = {mutable data:'a; descr:string; pt:ParamType}

type Param = 
   | PInt of Pb<IV<int,int>>
   | PF32 of Pb<IV<float32,float32>>

module Params = 

    let DisplayFreqParam = PInt( {Pb.data={IV.Min=0; Max=50; V=10;}; descr="Display Frequency"; pt=ParamType.Int})
    let NoiseParam = PF32( {Pb.data={IV.Min=0.0f; Max=0.5f; V=0.25f;}; descr="Noise"; pt=ParamType.F32})
    let StepSizeParam = PF32( {Pb.data={IV.Min=0.0f; Max=0.3f; V=0.01f;}; descr="Step Size"; pt=ParamType.F32})
    let NoiseFieldDecayParam = PF32( {Pb.data={IV.Min=0.0f; Max=0.9f; V=0.3f;}; descr="Noise field decay"; pt=ParamType.F32})
    let DeltaToNoiseParam = PF32( {Pb.data={IV.Min=0.0f; Max=0.9f; V=0.3f;}; descr="Delta to noise"; pt=ParamType.F32})
    let NoiseFieldCplParam = PF32( {Pb.data={IV.Min=0.0f; Max=0.3f; V=0.12f;}; descr="Noise field cpl"; pt=ParamType.F32})
    let FixedFieldCplParam = PF32( {Pb.data={IV.Min=0.0f; Max=6.0f; V=1.0f;}; descr="Fixed field cpl"; pt=ParamType.F32})
    let AbsDeltaTargetVm = PF32( {Pb.data={IV.Min=0.0f; Max=0.4f; V=0.2f;}; descr="AbsDelta target"; pt=ParamType.F32})

    let RingParams =
        [
            DisplayFreqParam
            NoiseParam
            StepSizeParam
        ]

    let Ring2Params =
        [
            DisplayFreqParam
            NoiseParam
            StepSizeParam
            NoiseFieldDecayParam
            DeltaToNoiseParam
            NoiseFieldCplParam
        ]

    let Ring3Params =
        [
            DisplayFreqParam
            NoiseParam
            StepSizeParam
            FixedFieldCplParam
        ]

    let Ring4Params =
        [
            DisplayFreqParam
            NoiseParam
            StepSizeParam
            NoiseFieldDecayParam
            DeltaToNoiseParam
            NoiseFieldCplParam
            FixedFieldCplParam
        ]

    let Ring5Params =
        [
            DisplayFreqParam
            NoiseParam
            StepSizeParam
            NoiseFieldDecayParam
            DeltaToNoiseParam
            NoiseFieldCplParam
            FixedFieldCplParam
            AbsDeltaTargetVm
        ]

