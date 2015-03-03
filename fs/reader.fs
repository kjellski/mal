namespace mal.Reader
open System.Text.RegularExpressions

module Reader =
    type Reader(input :string) = 
        let tokenRegex = "[\s,]*(~@|[\[\]{}()'`~^@]|\"(?:\\.|[^\\\"])*\"|;.*|[^\s\[\]{}('\"`,;)]*)"
        let unparsed = input
        let parsed = 
            Regex.Matches(unparsed, tokenRegex)
            |> Seq.cast<Match>
            |> Seq.map (fun m -> m.Value.Trim())
            |> Seq.filter(fun e -> e.Length > 0)
            |> Seq.toList
        
        let mutable position = 0

        member this.next =
            let token = this.parsedAtPosition position
            position <- position + 1
            token

        member this.peek = 
            this.parsedAtPosition position
            
        member private this.parsedAtPosition pos = 
            if parsed.Length > pos then
                let token = parsed.Item pos 
                Some(token)
            else 
                None

        member this.all = 
            parsed

        member this.printall = 
            for token in this.all do
                printf "%s" token

    let reader = Reader("(+ 1 2 (* 3 4))")
    let read_form = 
        ""