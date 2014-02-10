namespace Fledger.Journal

// Contains all the type definitions in the Journal
// Code originally 'forked' from https://github.com/readysetmark/WealthPulse/blob/master/Journal/Journal.fs

type Commodity = string

type Amount = decimal * Commodity option

type Status =
    | Cleared
    | Uncleared

type EntryType =
    | Balanced
    | VirtualBalanced
    | VirtualUnbalanced

type Header = {
    Date: System.DateTime;
    Status: Status;
    Code: string option;
    Description: string;
    Comment: string option
}

type Entry = {
    Header: Header;
    Account: string;
    AccountLineage: string list;
    EntryType: EntryType;
    Amount: Amount;
    Value: Amount option;
    Comment: string option;
}

type JournalData = {
    Transactions: Entry list;
    MainAccounts: Set<string>;
    AllAccounts: Set<string>;
}