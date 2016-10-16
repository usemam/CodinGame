(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    let contains i l =
        l |> List.fold (fun r x -> r || x = i) false

    let rec lastN n l =
        match List.length l <= 2 with
        | true -> l
        | false -> lastN n l.Tail

    (* N: the total number of nodes in the level, including the gateways *)
    (* L: the number of links *)
    (* E: the number of exit gateways *)
    let token = (Console.In.ReadLine()).Split [|' '|]
    let N = int(token.[0])
    let L = int(token.[1])
    let E = int(token.[2])

    let nodes = [0..N-1]
    let edges =
        [0..L-1] |> List.map (fun i ->
            (* N1: N1 and N2 defines a link between these nodes *)
            let token1 = (Console.In.ReadLine()).Split [|' '|]
            let N1 = int(token1.[0])
            let N2 = int(token1.[1])
            (N1, N2))

    let getAdjacent n =
        edges |> List.fold (fun l (x, y) ->
            match x = n with
            | true -> y::l
            | false ->
                match y = n with
                | true -> x::l
                | false -> l) []
    let mutable graph =
        nodes |> List.fold (fun g i ->
            Map.add i (getAdjacent i) g) Map.empty

    let gateways =
        [0..E-1] |> List.fold (fun l i ->
            let EI = int(Console.In.ReadLine()) (* the index of a gateway node *)
            EI::l) []
    let isGateway i =
        contains i gateways

    let removeEdge g (x, y) =
        let ex = Map.find x g
        let ex' = List.filter (fun e -> e <> y) ex
        let g' = g |> ((Map.remove x) >> (Map.add x ex'))
        let ey = Map.find y g'
        let ey' = List.filter (fun e -> e <> x) ey
        g' |> ((Map.remove y) >> (Map.add y ey'))

    let getShortestPath g n =
        let rec bfs queue =
            match List.isEmpty queue with
            | true -> failwith "Failed to find a path"
            | false ->
                let path = List.head queue
                let current = Seq.last path
                match isGateway current with
                | true -> path
                | false ->
                    let q' = List.tail queue
                    let notVisited =
                        Map.find current g
                        |> List.filter (fun i -> not (contains i path))
                    notVisited
                    |> List.fold (fun q i -> q@[path@[i]]) q'
                    |> bfs
        bfs [[n]]

    (* game loop *)
    while true do
        let SI = int(Console.In.ReadLine()) (* The index of the node on which the Skynet agent is positioned this turn *)

        let shortest = getShortestPath graph SI

        let [x;y;] = lastN 2 shortest
        graph <- removeEdge graph (x, y)
        (* Example: 0 1 are the indices of the nodes you wish to sever the link between *)
        printfn "%d %d" x y
        ()
    0