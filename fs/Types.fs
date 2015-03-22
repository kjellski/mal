namespace mal.types

module Types =

    type AtomType =
        | Number of int
        | Variable of string
        override this.ToString() = 
            match this with
            | Number n -> string n
            | Variable var -> var
        
    type MalVal = 
        | MalList of list: MalVal list
        | MalAtom of AtomType
        override this.ToString() = 
           match this with 
           | MalList list -> "( " + System.String.Join(", ", List.map string list) + " )"
           | MalAtom atom -> atom.ToString()