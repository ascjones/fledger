namespace Fledger.Tests

module JournalParsingTests =
    
    open FsUnit
    open FParsec
    open NUnit.Framework
    open Fledger.JournalParsing
    open System

    let testParser p str expected = 
        match run p str with 
        | Success(result, _, _)     -> result |> should equal expected
        | Failure(errorMsg, _, _)   -> failwith errorMsg

    [<Test>] 
    let parseDate ()= 
        testParser txnDateParser @"2014-01-14" (DateTime(2014,01,14))

    [<Test>]
    let parseRegularTx ()=
        testParser regularTxnParser @"2014-01-14=2014-01-12" (DateTime(2014,01,14), Some(DateTime(2014,01,12)))