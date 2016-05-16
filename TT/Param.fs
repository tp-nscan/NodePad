namespace TT
open System
open System.Collections.Generic

type ParamType = Int=0 | F32=1

type Pb<'a> = {bounds:I<'a>; value:'a;key:string; descr:string;}

type Param = 
   | PInt of Pb<int>
   | PF32 of Pb<float32>

type ParamGroup = {key:string; descr:string; groups:ParamGroup list; nodes:Param list}

module Params = 

    let ForCSharp param =
        match param with
        | PInt _  -> (param, ParamType.Int)
        | PF32 _  -> (param, ParamType.F32)


    let UpdateParam param (newVal:float32) =
        match param with
        | PInt pInt  -> PInt( {Pb.bounds=pInt.bounds; value=(int newVal); key=pInt.key; descr=pInt.descr;})
        | PF32 pF32  -> PF32( {Pb.bounds=pF32.bounds; value=newVal; key=pF32.key; descr=pF32.descr;})


    let UpdateParams (pd: IDictionary<string, Param>) (key:string) (value:float32) =
        pd.Item(key) = (UpdateParam (pd.Item(key)) value)


    let DisplayFreqParam     = PInt( {Pb.bounds={I.Min=0;    Max=50;};   value=10;    key="DisplayFreq"; descr="Display Frequency";})
    let NoiseParam           = PF32( {Pb.bounds={I.Min=0.0f; Max=0.5f;}; value=0.25f; key="Noise"; descr="Noise";})
    let StepSizeParam        = PF32( {Pb.bounds={I.Min=0.0f; Max=0.3f;}; value=0.01f; key="StepSize"; descr="Step Size";})
    
    let NearCplParam         = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="NearCpl"; descr="cpl";})
    let FarCplParam          = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="FarCpl"; descr="cpl";})
    let DecayParam           = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="Decay"; descr="decay";})
    let TargetParam          = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="Target"; descr="target";})
    let TargetCplParam       = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="TargetCpl"; descr="target cpl";})

    let NoiseFieldDecayParam = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="NoiseFieldDecay"; descr="Noise field decay";})
    let DeltaToNoiseParam    = PF32( {Pb.bounds={I.Min=0.0f; Max=0.9f;}; value=0.3f;  key="DeltaToNoise"; descr="Delta to noise";})
    let NoiseFieldCplParam   = PF32( {Pb.bounds={I.Min=0.0f; Max=0.3f;}; value=0.12f; key="NoiseFieldCpl"; descr="Noise field cpl";})
    let FixedFieldCplParam   = PF32( {Pb.bounds={I.Min=0.0f; Max=6.0f;}; value=1.0f;  key="FixedFieldCpl"; descr="Fixed field cpl";})
    let AbsDeltaTargetParam  = PF32( {Pb.bounds={I.Min=0.0f; Max=0.4f;}; value=2.2f;  key="AbsDeltaTarget"; descr="AbsDelta target";})


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
            ("AbsDeltaTarget", AbsDeltaTargetParam)
        |]

