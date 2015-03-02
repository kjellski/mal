namespace mal
open System
open mal.Reader

module Step1 = 
    let mutable loop = true

    let (|Prefix|_|) (p:string) (s:string) =
        if s.StartsWith(p) then
            Some(s.Substring(p.Length))
        else
            None

    let READ (input :string) =
        if input.StartsWith("exit") then
            loop <- false
            Console.WriteLine("Exitting... fare well...")
        input

    let EVAL sexp = sexp

    let PRINT (output :string) = 
        System.Console.WriteLine output
    
    let public REPL =
        while loop do
            Console.WriteLine "user> "
            let input = Console.ReadLine()
            let reader = Reader.Reader(input)
            READ input
            |> EVAL 
            |> PRINT