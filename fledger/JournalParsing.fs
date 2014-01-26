namespace Fledger

module JournalParsing = 

    open System
    open FParsec

    // type abbreaviations to circumevent value restriction http://www.quanttec.com/fparsec/tutorial.html#fs-value-restriction
    type UserState = unit
    type Parser<'t> = Parser<'t, UserState>

    let str s = pstring s

    let txnDateParser : Parser<_> =
        let date sep = pipe5 pint32 sep pint32 sep pint32 (fun y _ m _ d -> DateTime(y,m,d))
        date (str "-")
        //let dateWithSep = date (str "-")
        //dateWithSep .>> optional (str "=") >>. opt dateWithSep

    let regularTxnParser : Parser<_> =
        txnDateParser
        .>>. opt (pchar '='>>. txnDateParser)
    
//    regularTxnParser :: TokenParsing m => m RawEntity
//regularTxnParser = RawTransactionEntity <$!> go
//  where go = RawTransaction
//             <$!> txnDateParser
//             <*> optional (char '=' *> txnDateParser)
//             <*> (many spaceChars *>
//                  optional (tokenP (char '*' <|> char '!')))
//             <*> optional
//                 (tokenP (parens (many (noneOf ")\r\n"))))
//             <*> tokenP (until (longSepOrEOLIf (char ';')))
//             <*> optional noteParser
//             <*> (endOfLine *> some postingParser)
//             <?> "regular transaction" 
