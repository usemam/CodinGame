(* The while loop represents the game. *)
(* Each iteration represents a turn of the game *)
(* where you are given inputs (the heights of the mountains) *)
(* and where you have to print an output (the index of the mountain to fire on) *)
(* The inputs you are given are automatically updated according to your last actions. *)
open System


[<EntryPoint>]
let main argv =

    let directions =
        [
            ("N", fun (x, y) -> (x, y-1));
            ("NE", fun (x, y) -> (x+1, y-1));
            ("E", fun (x, y) -> (x+1, y));
            ("SE", fun (x, y) -> (x+1, y+1));
            ("S", fun (x, y) -> (x, y+1));
            ("SW", fun (x, y) -> (x-1, y+1));
            ("W", fun (x, y) -> (x-1, y));
            ("NW", fun (x, y) -> (x-1, y-1))
        ]
    
    let isValidPoint (x, y) = (x > -1 && x < 40) && (y > -1 && y < 18)

    let delta (x1, y1) (x2, y2) =
        let square x = x * x
        sqrt(((x2-x1) |> float |> square) + ((y2-y1) |> float |> square))
    
    let getSiblingPoints point target =
        directions
        |> List.map (fun (name, f) -> (name, f point))
        |> List.filter (fun (name, p) -> isValidPoint p)
        |> List.map (fun (name, p) -> (name, p, delta p target))

    let fst3 (f, _, _) = f
    let snd3 (_, s, _) = s
    let thrd (_, _, t) = t
        
    
    (* lightX: the X position of the light of power *)
    (* lightY: the Y position of the light of power *)
    (* initialTX: Thor's starting X position *)
    (* initialTY: Thor's starting Y position *)
    let token = (Console.In.ReadLine()).Split [|' '|]
    let lightX = int(token.[0])
    let lightY = int(token.[1])
    let initialTX = int(token.[2])
    let initialTY = int(token.[3])
    let light = (lightX, lightY)
    let mutable current = (initialTX, initialTY)
    (* game loop *)
    while true do
        let remainingTurns = int(Console.In.ReadLine()) (* The remaining amount of turns Thor can move. Do not remove this line. *)
    
        (* Write an action using printfn *)
        (* To debug: Console.Error.WriteLine("Debug message") *)
        let siblings = getSiblingPoints current light
        let next = siblings |> List.minBy (fun (name, p, d) -> d)

        (* A single line providing the move to be made: N NE E SE S SW W or NW *)
        printfn "%s" (fst3 next)

        current <- snd3 next
    0
