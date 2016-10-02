(* The while loop represents the game. *)
(* Each iteration represents a turn of the game *)
(* where you are given inputs (the heights of the mountains) *)
(* and where you have to print an output (the index of the mountain to fire on) *)
(* The inputs you are given are automatically updated according to your last actions. *)
open System


[<EntryPoint>]
let main argv =

    let L = int(Console.In.ReadLine())
    let H = int(Console.In.ReadLine())
    let T = Console.In.ReadLine()
    let alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?"

    let getLetterLine (index:int) (row:string) =
        row.[L*index..L*(index+1)-1]

    let foldRow row rowIndex board =
        alphabet |> Seq.fold (
            fun state letter ->
                let letterIndex = alphabet |> Seq.findIndex (fun c -> c = letter)
                state |> Map.add (letter, rowIndex) (getLetterLine letterIndex row)
            ) board

    let asciiMap = 
        [0..H-1] |> List.fold (
            fun state i ->
                let row = Console.In.ReadLine()
                foldRow row i state
            ) Map.empty

    let printAscii c i =
        match asciiMap |> Map.tryFind (c |> Char.ToUpper, i) with
            | Some(s) -> printf "%s" s
            | None -> printf "%s" (Map.find ('?', i) asciiMap)

    [0..H-1] |> List.iter (fun i ->
        T |> Seq.iter (fun c -> printAscii c i)
        Console.Out.WriteLine())

    0
