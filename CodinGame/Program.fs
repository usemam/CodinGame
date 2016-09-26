(* The while loop represents the game. *)
(* Each iteration represents a turn of the game *)
(* where you are given inputs (the heights of the mountains) *)
(* and where you have to print an output (the index of the mountain to fire on) *)
(* The inputs you are given are automatically updated according to your last actions. *)
open System


[<EntryPoint>]
let main argv =
    
    let take n lst =
        let rec takeInner n source res =
            if n <= 0 then res else
                match source with
                | [] -> res
                | x::xs -> takeInner (n-1) xs (x::res)
        takeInner n lst []

    let groupBy f lst =
        lst
        |> List.fold (fun group x ->
            match group |> Map.tryFind (f x) with
            | Some(s) -> group |> Map.remove (f x) |> Map.add (f x) (x::s)
            | None -> group |> Map.add (f x) [x]
            ) Map.empty
        |> Map.toList

    let readTemps n =
        let temps = Console.In.ReadLine()
        temps.Split([|' '|]) |> List.ofArray |> take n |> List.map (fun s -> int(s))

    let groupTemps temps =
        temps |> groupBy (fun x -> abs(x))

    let n = int(Console.In.ReadLine()) (* the number of temperatures to analyse *)
    if n > 0 then
        let temps = readTemps n (* the n temperatures expressed as integers ranging from -273 to 5526 *)

        let closestGroup = temps |> groupTemps |> List.minBy (fun (x, _) -> x)

        printfn "%d"
            (closestGroup |> fun (x, xs) -> xs |> List.max)
    else
        printfn "0"
    0
