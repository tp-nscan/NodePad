namespace TT
open System

type ParamType = Int=0 | F32=1

type Pb<'a> = {bounds:I<'a>; mutable value:'a; descr:string;}

type Param = 
   | PInt of Pb<int>
   | PF32 of Pb<float32>



module Params = 

    let ForCSharp param =
        match param with
        | PInt _  -> (param, ParamType.Int)
        | PF32 _  -> (param, ParamType.F32)


    let DisplayFreqParam = PInt( {Pb.bounds={I.Min=0; Max=50;}; value=10; descr="Display Frequency";})
    let NoiseParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.5f;}; value=0.25f; descr="Noise";})
    let StepSizeParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.3f;}; value=0.01f; descr="Step Size";})
    let NoiseFieldDecayParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f; descr="Noise field decay";})
    let DeltaToNoiseParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f; descr="Delta to noise";})
    let NoiseFieldCplParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.3f;}; value=0.12f; descr="Noise field cpl";})
    let FixedFieldCplParam = PF32( {Pb.bounds={I.Min=0.0f; Max=6.0f;}; value=1.0f; descr="Fixed field cpl";})
    let AbsDeltaTargetVm = PF32( {Pb.bounds={I.Min=0.0f; Max=0.4f;}; value=2.2f; descr="AbsDelta target";})

    let RingParams =
        [|
            ("DisplayFreq", DisplayFreqParam)
            ("Noise", NoiseParam)
            ("StepSize", StepSizeParam)
        |]

    let Ring2Params =
        [|
            ("DisplayFreq", DisplayFreqParam)
            ("Noise", NoiseParam)
            ("StepSize", StepSizeParam)
            ("NoiseDecay", NoiseFieldDecayParam)
            ("DeltaToNoise", DeltaToNoiseParam)
            ("NoiseField", NoiseFieldCplParam)
        |]

    let Ring3Params =
        [|
            ("DisplayFreq", DisplayFreqParam)
            ("Noise", NoiseParam)
            ("StepSize", StepSizeParam)
            ("FixedFieldCpl", FixedFieldCplParam)
        |]

    let Ring4Params =
        [|
            ("DisplayFreq", DisplayFreqParam)
            ("Noise", NoiseParam)
            ("StepSize", StepSizeParam)
            ("NoiseDecay", NoiseFieldDecayParam)
            ("DeltaToNoise", DeltaToNoiseParam)
            ("NoiseField", NoiseFieldCplParam)
            ("FixedFieldCpl", FixedFieldCplParam)
        |]

    let Ring5Params =
        [|
            ("DisplayFreq", DisplayFreqParam)
            ("Noise", NoiseParam)
            ("StepSize", StepSizeParam)
            ("NoiseDecay", NoiseFieldDecayParam)
            ("DeltaToNoise", DeltaToNoiseParam)
            ("NoiseField", NoiseFieldCplParam)
            ("FixedFieldCpl", FixedFieldCplParam)
            ("AbsDeltaTarget", AbsDeltaTargetVm)
        |]

