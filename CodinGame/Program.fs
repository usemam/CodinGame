(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let n = int(Console.In.ReadLine()) (* the number of adjacency relations *)
    let edges =
        [0..n-1]
        |> List.map (fun _ -> Console.In.ReadLine())
        |> List.map (fun s -> s.Split [|' '|])
        |> List.map (fun l -> (l.[0] |> int, l.[1] |> int))

    let (minNode, maxNode) =
        edges
        |> List.fold (fun l (x, y) -> x::y::l) []
        |> Seq.distinct
        |> Seq.sort
        |> (fun s -> (Seq.head s, Seq.last s))

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
        [| for i in minNode..maxNode do yield getAdjacent i |]

    let getMostDistantNode (g: int list []) n =
        let dMap = [| for i in minNode..maxNode do yield 0 |]
        let rec dfs stack =
            match List.isEmpty stack with
            | true -> ()
            | false ->
                let current = List.head stack
                let distance = dMap.[current - minNode]
                let notVisited =
                    g.[current - minNode]
                    |> List.filter (fun x -> dMap.[x - minNode] = 0)
                notVisited
                |> List.iter (fun x -> dMap.[x - minNode] <- distance + 1)
                let newStack =
                    notVisited
                    |> List.fold (fun s x -> x::s) (List.tail stack)
                dfs newStack

        dfs [n]
        dMap
        |> Array.mapi (fun i x -> (i, x))
        |> Array.maxBy (fun (_, x) -> x)
        |> fun (i, x) -> (i + minNode, x)
        

    let secondNode = getMostDistantNode graph minNode
    let thirdNode = getMostDistantNode graph (fst secondNode)
    let graphD = snd thirdNode
    (* The minimal amount of steps required to completely propagate the advertisement *)
    printfn "%d" ((graphD + 1)/2)

    Console.In.ReadLine()
    
    0