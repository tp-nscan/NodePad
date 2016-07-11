namespace TT
open System

module ColUtils =
    
    let SubSeqs seqCount seqLength =
        seq { for ss in 1 .. seqCount do
                yield seq { (ss-1)*seqLength .. ss *seqLength - 1 }
            }

