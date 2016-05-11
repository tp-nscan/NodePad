namespace TT
open System
open System.Collections.Generic

module MathUtils =
  let DictToSeq d = d |> Seq.map (fun (KeyValue(k,v)) -> (k,v))
  let DictToSeqValues d = d |> Seq.map (fun (KeyValue(k,v)) -> v)
  let DictToArray (d:IDictionary<_,_>) = d |> DictToSeq |> Seq.toArray
  let DictToList (d:IDictionary<_,_>) = d |> DictToSeq |> Seq.toList
  let SeqToList d = d |> Seq.map (fun (KeyValue(k,v)) -> v) |> Seq.toList
  let MapToDict (m:Map<'k,'v>) = new Dictionary<'k,'v>(m) :> IDictionary<'k,'v>
  let ListToDict (l:('k * 'v) list) = new Dictionary<'k,'v>(l |> Map.ofList) :> IDictionary<'k,'v>
  let SeqToDict (s:('k * 'v) seq) = new Dictionary<'k,'v>(s |> Map.ofSeq) :> IDictionary<'k,'v>
  let ArrayToDict (a:('k * 'v) []) = new Dictionary<'k,'v>(a |> Map.ofArray) :> IDictionary<'k,'v>