// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

(* The while loop represents the game. *)
(* Each iteration represents a turn of the game *)
(* where you are given inputs (the heights of the mountains) *)
(* and where you have to print an output (the index of the mountain to fire on) *)
(* The inputs you are given are automatically updated according to your last actions. *)
open System


[<EntryPoint>]
let main argv = 
    
    let rec getMaxHeightAndIndex i (maxH, maxI) =
        if i > 7 then (maxH, maxI)
        else
            let mountainH = int(Console.In.ReadLine())
            getMaxHeightAndIndex (i+1) (if mountainH > maxH then (mountainH, i) else (maxH, maxI))

    (* game loop *)
    while true do
        let biggestMountain = getMaxHeightAndIndex 0 (0, 0)
        (* Write an action using printfn *)
        (* To debug: Console.Error.WriteLine("Debug message") *)
    
        printfn "%d" (snd biggestMountain) (* The index of the mountain to fire on. *)
    0