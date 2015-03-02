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
        
        let mutable position = 0

        member this.next =
            let token = parsed.nth position
            position <- position + 1
            token

        member this.peek = 
            parse[position]
            
        member this.all = 
            parsed