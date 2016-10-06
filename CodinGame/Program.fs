(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let tail l =
        match l with
        | [] -> failwith "tail on empty list"
        | x::xs -> xs

    let rec tryLast l =
        match l with
        | [] -> None
        | [x] -> Some(x)
        | x::xs -> tryLast xs

    let readTable n =
        [0..n-1]
            |> List.fold (fun t i ->
                (* EXT: file extension *)
                (* MT: MIME type. *)
                let token = (Console.In.ReadLine()).Split [|' '|]
                let EXT = token.[0].ToUpper()
                let MT = token.[1]
                match Map.tryFind EXT t with
                | Some(k) -> t
                | None -> Map.add EXT MT t) Map.empty

    let getFileExt (fileName:string) =
        let nameChunks = fileName.Split [|'.'|] |> Seq.toList |> tail
        match tryLast nameChunks with
        | Some(s) -> s
        | None -> String.Empty

    let N = int(Console.In.ReadLine()) (* Number of elements which make up the association table. *)
    let Q = int(Console.In.ReadLine()) (* Number Q of file names to be analyzed. *)
    let table = readTable N

    [0..Q-1]
        |> List.iter (fun i ->
            let FNAME = Console.In.ReadLine().ToUpper() (* One file name per line. *)
            match Map.tryFind (getFileExt FNAME) table with
            | Some(s) -> printfn "%s" s
            | None -> printfn "UNKNOWN")

    let enter = Console.In.ReadLine()
    
    0
