namespace mal.types

module Types =
    type MalType() = class end

    type MalList() = 
        inherit MalType()

    type MalVal() =
        inherit MalType()
    
    type MalAtom() =
        inherit MalType()
        