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
        testParser ledgerdate @"2014-01-14" (DateTime(2014,01,14))