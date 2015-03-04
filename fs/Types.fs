namespace mal.types

module Types =
    type MalType =
        | MalList of MalType list
        | MalVal 
        | MalAtom
        