namespace Fledger.Tests

module JournalParsingTests =
    
    open FsUnit
    open FParsec
    open NUnit.Framework
    open Fledger.JournalParsing
    open System
    open Parser

    let testParser p str expected = 
        match run p str with 
        | Success(result, _, _)     -> result |> should equal expected
        | Failure(errorMsg, _, _)   -> failwith errorMsg

    let tx = @"2014-01-14=2014-01-17 * La Parada
; MD5Sum: 2ff94fd4dad8e2f8e7888e2d5fc9cd8b
; CSV: 2014-01-14,2014-01-17,LA PARADA CAPE,-330.00,26017.79
Expenses:Food:Restaurant                                           
Assets:ZA:ABSA:Current                                          -330.00 ZAR"

    [<Test>] 
    let parseDate ()= 
        testParser txnDateParser @"2014-01-14" (DateTime(2014,01,14))

//    [<Test>]
//    let parseRegularTx ()=
//        testParser regularTxnParser tx {Date=DateTime(2014,01,14);AuxDate=Some(DateTime(2014,01,17));State='*';Description="La Parada"} 

    [<Test>]
    let parseTx ()=
        let result = parseJournalFile "~/code/ledger/data/2014-01.ledger" System.Text.ASCIIEncoding.UTF8
        result |> should equal 0