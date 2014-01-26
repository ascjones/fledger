namespace Fledger

module JournalParsing = 

    open System
    open FParsec

    // type abbreaviations to circumevent value restriction http://www.quanttec.com/fparsec/tutorial.html#fs-value-restriction
    type UserState = unit
    type Parser<'t> = Parser<'t, UserState>

    let str s = pstring s

    let ledgerdate : Parser<_> =
        let date sep = pipe5 pint32 sep pint32 sep pint32 (fun y _ m _ d -> DateTime(y,m,d))
        let dateWithSep = date (str "-")
        dateWithSep .>> optional (str "=") >>. opt dateWithSep
