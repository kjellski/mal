namespace mal.reader
open System.Text.RegularExpressions
open mal.matching.Matching
open mal.types.Types

module Reader =
    let tokenizer unparsed = 
        let tokenRegex = "[\s,]*(~@|[\[\]{}()'`~^@]|\"(?:\\.|[^\\\"])*\"|;.*|[^\s\[\]{}('\"`,;)]*)"
        Regex.Matches(unparsed, tokenRegex)
        |> Seq.cast<Match>
        |> Seq.map (fun m -> m.Value.Trim())
        |> Seq.filter(fun e -> e.Length > 0)
        |> Seq.toList
    
    type Reader (tokens : List<string>) = 
        let _tokens = tokens        
        let mutable position = 0

        member this.next =
            let token = this.parsedAtPosition position
            position <- position + 1
            token

        member this.peek = 
            this.parsedAtPosition position
            
        member this.parsedAtPosition pos = 
            if _tokens.Length > pos then
                let token = _tokens.Item pos 
                Some(token)
            else 
                None

        member this.all = 
            _tokens

        member this.printall = 
            for token in this.all do
                printf "%s" token

    let testReader = Reader(tokenizer "(+ 1 2 (* 3 4))")
    
    let read_list (reader :Reader) = 
        MalList()

    let read_atom (reader :Reader) = 
        MalAtom()

    let read_form (reader :Reader) : MalType = 
        let readr = reader
        let token = reader.peek
        match token with
        | Some("(") -> read_list reader
        | Some(_) -> read_atom reader
        | None -> MalType()
        
    let read_str form = 
        let token = tokenizer form
        read_form(Reader(token))