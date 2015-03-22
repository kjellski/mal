namespace mal.reader
open System
open System.Text.RegularExpressions
open mal.matching.Matching
open mal.types.Types

module Reader =    
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
                
    let read_string (reader: Reader) : MalVal =
        let string = 
            match reader.peek with
            | Some(str) -> str.Replace("\"", "")
            | None -> ""
        reader.next |> ignore 
        string |> String |> MalAtom
            
    let rec read_list (reader :Reader) : MalVal  = 
        match reader.peek with 
        | None -> failwith "Missing \")\""
        | Some(")") -> 
            reader.next |> ignore
            MalList []
        | _ -> 
            let head = read_form reader
            let (MalList rest) = read_list reader
            MalList (head::rest)

    and read_atom (reader: Reader) : MalVal = 
        match reader.next with
        | Some("\"") -> read_string reader
        | Some(num) when isNumber num -> Int32.Parse num |> Number |> MalAtom
        | Some(atom) when isAtom atom -> atom |> Variable |> MalAtom
        | Some(operator) when isOperator operator -> operator |> makeOperator |> Operator |> MalAtom
        | Some(fail) -> failwith ("Whoops! " + fail + " was unecpected.")
        | None -> failwith "The cake was a lie, nothing left."

    and read_form (reader: Reader) : MalVal = 
        match reader.peek with
        | Some("(") -> 
            reader.next |> ignore
            read_list reader
        | Some(_) -> read_atom reader
        | None -> MalList []
        
    let read_str form = 
        let token = tokenizer form
        let ret = read_form(Reader(token))
        ret