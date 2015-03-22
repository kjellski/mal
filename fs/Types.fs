namespace mal.types

module Types =
    
    type MalOperatorType = 
        | Add 
        | Sub 
        | Mul 
        | Div 
        override this.ToString() =  
            match this with 
            | Add _ -> "+"
            | Sub _ -> "-"
            | Mul _ -> "*"
            | Div _ -> "/"

    
    let makeOperator = function
        | "+" -> Add 
        | "-" -> Sub
        | "*" -> Mul 
        | "/" -> Div
        | fail -> failwith ("No operator made from " + fail)

    type MalAtomType =
        | Number of int
        | Variable of string
        | Operator of MalOperatorType
        override this.ToString() = 
            match this with
            | Number n -> string n
            | Variable var -> var
            | Operator op -> op.ToString()

    type MalVal = 
        | MalList of list: MalVal list
        | MalAtom of MalAtomType
        override this.ToString() = 
           match this with 
           | MalList list -> "( " + System.String.Join(", ", List.map string list) + " )"
           | MalAtom atom -> atom.ToString()