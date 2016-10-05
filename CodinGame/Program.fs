(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let replicate n x =
        let rec replicateInner n l =
            match n > 0 with
            | true -> replicateInner (n-1) (x::l)
            | false -> l
        replicateInner n []

    let tail l =
        match l with
        | [] -> failwith "tail of empty list."
        | _::xs -> xs

    let tryHead l =
        match l with
        | [] -> None
        | x::_ -> Some(x)

    let add symbol acc =
        match acc with
        | [] -> [(symbol, 1)]
        | (s, n)::xs ->
            match symbol = s with
            | true -> (s, n+1)::xs
            | false -> (symbol, 1)::(s, n)::xs

    let rec binaryToInternal bin acc =
        let h = tryHead bin
        match h with
        | Some(s) -> add s acc |> binaryToInternal (tail bin)
        | None -> acc

    let toUnary (s, n) =
        let prefix =
            match s with
            | '0' -> "00"
            | '1' -> "0"
            | _ -> failwith "unexpected value"
        let value = replicate n '0' |> String.Concat
        prefix + " " + value

    let intToBinary i =
        let rec intToBinary' i =
            match i with
            | 0 | 1 -> string i
            | _ ->
                let bit = string (i%2)
                (intToBinary' (i/2)) + bit
        let bin = intToBinary' i
        let binLength = String.length bin
        match binLength = 7 with
        | true -> bin
        | false -> ((replicate (7 - binLength) '0') |> String.Concat) + bin

    let message = Console.In.ReadLine()
    let messageAsBinary =
        message
            |> Seq.map int
            |> Seq.map intToBinary
            |> String.Concat
    
    let unary =
        binaryToInternal (Seq.toList messageAsBinary) []
        |> List.rev
        |> Seq.fold (fun s x -> s + " " + (toUnary x)) String.Empty
        |> Seq.skip 1
        |> String.Concat

    printfn "%s" unary
    let enter = Console.In.ReadLine()
    
    0
