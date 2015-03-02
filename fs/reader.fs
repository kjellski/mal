namespace mal.Reader
open System.Text.RegularExpressions

module public Reader =
    type Reader(input :string) = 
        let tokenRegex = "[\s,]*(~@|[\[\]{}()'`~^@]|\"(?:\\.|[^\\\"])*\"|;.*|[^\s\[\]{}('\"`,;)]*)"
        let unparsed = input
        
        member this.next =
            unparsed
        member this.peek = 
            unparsed
            
        member this.all = 
            Regex.Matches(input, tokenRegex)
            |> Seq.cast<Match>
            |> Seq.groupBy (fun m -> m.Value)
