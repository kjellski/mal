﻿namespace mal.Step1
open System
open mal.types.Types
open mal.reader.Reader

module Step2 =
    let mutable loop = true

// hash map for environment
//    let repl_env = [
//        "+", fun a b -> a + b;    
//    ] |> Map.ofList

    let READ (input: string) =
        if input.StartsWith("exit") then
            loop <- false
            printf "Exitting... fare well..."
        let ast = read_str input
        ast

    let EVAL sexp = sexp

    let PRINT (output: MalVal) = 
        System.Console.WriteLine output
    
    let public REPL =
        while loop do
            printf "user> "
            try 
                let input = Console.ReadLine()
                READ input
                |> EVAL 
                |> PRINT
            with
            | error -> Console.WriteLine( string error )

