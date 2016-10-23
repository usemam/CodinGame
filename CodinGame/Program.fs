(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let n = int(Console.In.ReadLine()) (* the number of adjacency relations *)
    let edges =
        [0..n-1]
        |> List.map (fun i -> Console.In.ReadLine())
        |> List.map (fun s -> s.Split [|' '|])
        |> List.map (fun l -> (l.[0] |> int, l.[1] |> int))

    let nodes =
        edges
        |> List.fold (fun l (x, y) -> x::y::l) []
        |> Seq.distinct
        |> List.ofSeq

    let getAdjacent n =
        edges
        |> List.fold (fun l (x, y) ->
            match x = n with
            | true -> y::l
            | false ->
                match y = n with
                | true -> x::l
                | false -> l) []
    let graph =
        nodes
        |> List.fold (fun g i ->
            Map.add i (getAdjacent i) g) Map.empty

    let getMostDistantNode g n =
        let contains i l =
            List.fold (fun r x -> x = i || r) false l
        let rec dfs stack paths =
            match List.isEmpty stack with
            | true -> paths
            | false ->
                let path = List.head stack
                let (current, distance) = List.head path
                
                let visited = path |> List.map (fun (x,_) -> x)

                let notVisited =
                    Map.find current g
                    |> List.filter (fun x -> not (contains x visited))
                match List.isEmpty notVisited with
                | true ->
                    dfs (List.tail stack) (path::paths)
                | false ->
                    let newStack =
                        List.fold (fun s n -> ((n, distance+1)::path)::s) (List.tail stack) notVisited
                    dfs newStack paths

        let paths = dfs [[(n,0)]] []
        let longestPath =
            paths |> List.maxBy (fun l -> List.length l)
        List.head longestPath

    let startNode = List.head nodes
    let secondNode = getMostDistantNode graph startNode
    let thirdNode = getMostDistantNode graph (fst secondNode)
    let graphD = snd thirdNode
    (* The minimal amount of steps required to completely propagate the advertisement *)
    printfn "%d" ((graphD + 1)/2)

    Console.In.ReadLine()
    
    0