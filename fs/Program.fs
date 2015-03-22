namespace mal.Main
open mal.Step1

module Main =
    [<EntryPoint>]
    let main argv = 
        Step2.REPL
        0