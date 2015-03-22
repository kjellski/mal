namespace mal.matching
open System
open System.Text.RegularExpressions

module Matching =
    let tokenizer unparsed = 
        let tokenRegex = "[\s,]*(~@|[\[\]{}()'`~^@]|\"(?:\\.|[^\\\"])*\"|;.*|[^\s\[\]{}('\"`,;)]*)"
        Regex.Matches(unparsed, tokenRegex)
        |> Seq.cast<Match>
        |> Seq.map (fun m -> m.Value.Trim())
        |> Seq.filter(fun e -> e.Length > 0)
        |> Seq.toList

    let (|Prefix|_|) (p:string) (s:string) =
        if s.StartsWith(p) then
            Some(s.Substring(p.Length))
        else
            None

    let isOperator s =
        Regex.Matches(s, "[\/\+\-\*]")
        |> Seq.cast<Match>
        |> Seq.filter(fun e -> e.Length > 0)
        |> Seq.toList
        |> Seq.length = 1
    
    let isAtom s =
        Regex.Matches(s, "[a-zA-Z][\w]+")
        |> Seq.cast<Match>
        |> Seq.filter(fun e -> e.Length > 0)
        |> Seq.toList
        |> Seq.length = 1

    let isNumber s =
        let (parsed, _) = Int32.TryParse(s)
        parsed
