(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

[<EntryPoint>]
let main argv =
    
    (* W: width of the building. *)
    (* H: height of the building. *)
    let token = (Console.In.ReadLine()).Split [|' '|]
    let W = int(token.[0])
    let H = int(token.[1])
    let N = int(Console.In.ReadLine()) (* maximum number of turns before game over. *)
    let token1 = (Console.In.ReadLine()).Split [|' '|]
    let X0 = int(token1.[0])
    let Y0 = int(token1.[1])

    let up bat a b c d =
        let x = fst bat
        let y = (snd bat + snd a)/2
        ((x, y), (x, snd a), (x, snd a), bat, bat)
    let upRight bat a b c d =
        let x = (fst bat + fst b)/2
        let y = (snd bat + snd a)/2
        ((x, y), (fst bat, snd a), b, bat, (fst b, snd bat))
    let right bat a b c d =
        let x = (fst bat + fst b)/2
        let y = snd bat
        ((x, y), bat, (fst b, y), bat, (fst b, y))
    let downRight bat a b c d =
        let x = (fst bat + fst b)/2
        let y = (snd bat + snd c)/2
        ((x, y), bat, (fst b, snd bat), (fst bat, snd c), d)
    let down bat a b c d =
        let x = fst bat
        let y = (snd bat + snd c)/2
        ((x, y),  bat, bat, (x, snd c), (x, snd c))
    let downLeft bat a b c d =
        let x = (fst a + fst bat)/2
        let y = (snd bat + snd c)/2
        ((x, y), (fst a, snd bat), bat, c, (fst bat, snd d))
    let left bat a b c d =
        let x = (fst a + fst bat)/2
        let y = snd bat
        ((x, y), (fst a, y), bat, (fst a, y), bat)
    let upLeft bat a b c d =
        let x = (fst a + fst bat)/2
        let y = (snd b + snd bat)/2
        ((x, y), a, (fst bat, snd a), (fst a, snd bat), bat)

    let rec bombBinarySearch bat a b c d =
        let getJumpSlotAndNewRect dir =
            let dirMap =
                [
                    ("U", up);
                    ("UR", upRight);
                    ("R", right);
                    ("DR", downRight);
                    ("D", down);
                    ("DL", downLeft);
                    ("L", left);
                    ("UL", upLeft)
                ]
            let mutator =
                dirMap
                |> List.find (fun (x, _) -> x = dir)
                |> snd
            mutator bat a b c d

        let bombDir = Console.In.ReadLine().ToUpper()

        let ((x, y), a', b', c', d') = getJumpSlotAndNewRect bombDir

        printfn "%d %d" x y

        bombBinarySearch (x, y) a' b' c' d'
    
    bombBinarySearch (X0, Y0) (0,0) (W-1,0) (0,H-1) (W-1,H-1)
    
    0