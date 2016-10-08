(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let width = int(Console.In.ReadLine()) (* the number of cells on the X axis *)
    let height = int(Console.In.ReadLine()) (* the number of cells on the Y axis *)

    let findRightNeighbour (x, y) map =
        let y' =
            match [y+1..width] |> List.tryFind (fun i ->
                match Map.tryFind (x, i) map with
                | Some(1) -> true
                | Some(_) -> false
                | None -> false) with
            | Some(y'') -> y''
            | None -> -1
        match y' > -1 with
        | true -> (x, y')
        | false -> (-1, -1)
    
    let findBottomNeighbour (x, y) map =
        let x' =
            match [x+1..height] |> List.tryFind (fun i ->
                match Map.tryFind (i, y) map with
                | Some(1) -> true
                | Some(_) -> false
                | None -> false) with
            | Some(x'') -> x''
            | None -> -1
        match x' > -1 with
        | true -> (x', y)
        | false -> (-1, -1) 

    let printNodeAndNeighbours (x,y) map =
        let (x2, y2) = findRightNeighbour (x,y) map
        let (x3, y3) = findBottomNeighbour (x,y) map
        printfn "%d %d %d %d %d %d" y x y2 x2 y3 x3

    let nodes =
        [0..height-1] |> List.fold (fun s' h ->
            let line = Console.In.ReadLine()
            let lineNodes = line.[0..width-1] |> Seq.toList
            List.fold2 (fun s'' n w ->
                match n with
                | '0' -> Map.add (h, w) 1 s''
                | '.' -> Map.add (h, w) 0 s''
                | _ -> failwith "unexpected node") s' lineNodes [0..width-1]
        ) Map.empty

    [0..height-1] |> List.iter (fun h ->
        [0..width-1] |> List.iter (fun w ->
            match Map.find (h, w) nodes with
            | 1 -> printNodeAndNeighbours (h,w) nodes
            | _ -> ()
        ))

    let enter = Console.In.ReadLine()
    
    0
