namespace mal.Main
open mal.Step1

module Main =
    [<EntryPoint>]
    let main argv = 
        Step1.REPL
        0