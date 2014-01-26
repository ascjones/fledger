
// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module fledger.Main

open System
open FParsec

let test p str = 
    match run p str with 
    | Success(result, _, _)     -> printfn "Sucess: %A" result
    | Failure(errorMsg, _, _)   -> printfn "Failure: %s" errorMsg

let str s = pstring s

let ledgerdate =
    let date sep = pipe5 pint32 sep pint32 sep pint32 (fun y _ m _ d -> DateTime(y,m,d))
    let dateWithSep = date (str "-")
    dateWithSep .>> optional (str "=") >>. opt dateWithSep

let tx = @"2014-01-14=2014-01-17 * La Parada
    ; MD5Sum: 2ff94fd4dad8e2f8e7888e2d5fc9cd8b
    ; CSV: 2014-01-14,2014-01-17,LA PARADA CAPE,-330.00,26017.79
    Expenses:Food:Restaurant                                           
    Assets:ZA:ABSA:Current                                          -330.00 ZAR"
   

[<EntryPoint>]
let main args = 
//    test pfloat "1.25"
//    test pfloat "1.25E 3"
//    test floatBetweenBrackets "[1.0]"

    test ledgerdate "2014-01-14"
    test ledgerdate "2014-01-14=2014-01-17"
    0

