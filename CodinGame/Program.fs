(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

type Direction = Left | Right

[<EntryPoint>]
let main argv =

    (* nbFloors: number of floors *)
    (* width: width of the area *)
    (* nbRounds: maximum number of rounds *)
    (* exitFloor: floor on which the exit is found *)
    (* exitPos: position of the exit on its floor *)
    (* nbTotalClones: number of generated clones *)
    (* nbAdditionalElevators: ignore (always zero) *)
    (* nbElevators: number of elevators *)
    let token = (Console.In.ReadLine()).Split [|' '|]
    let nbFloors = int(token.[0])
    let width = int(token.[1])
    let nbRounds = int(token.[2])
    let exitFloor = int(token.[3])
    let exitPos = int(token.[4])
    let nbTotalClones = int(token.[5])
    let nbElevators = int(token.[7])

    let isExitFloor n = exitFloor = n

    let readElevatorPosition map =
        let token1 = (Console.In.ReadLine()).Split [|' '|]
        let elevatorFloor = int(token1.[0])
        let elevatorPos = int(token1.[1])
        Map.add elevatorFloor elevatorPos map
    let elevators =
        [0..nbElevators-1]
        |> List.fold (fun m _ -> readElevatorPosition m) Map.empty

    (* game loop *)
    let readClonePositionAndPrintAction blocks =
        let decideOnAction (floor, pos) (dir : Direction) =
            let moveTo =
                match isExitFloor floor with
                | true -> exitPos
                | false -> Map.find floor elevators
            let moveToDirection =
                match moveTo - pos with
                | diff when diff > 0 -> Right
                | diff when diff < 0 -> Left
                | _ -> dir
            match dir = moveToDirection with
            | true -> "WAIT"
            | false -> "BLOCK"

        (* cloneFloor: floor of the leading clone *)
        (* clonePos: position of the leading clone on its floor *)
        (* direction: direction of the leading clone: LEFT or RIGHT *)
        let token2 = (Console.In.ReadLine()).Split [|' '|]
        let cloneFloor = int(token2.[0])
        let clonePos = int(token2.[1])
        let direction = token2.[2]

        (* action: WAIT or BLOCK *)
        let action =
            match direction.ToUpper() with
            | "NONE" -> "WAIT"
            | "LEFT" -> decideOnAction (cloneFloor, clonePos) Left
            | "RIGHT" -> decideOnAction (cloneFloor, clonePos) Right
            | _ -> failwith "Unsupported direction"
    
        printfn "%s" action
        let newBlocks =
            match action with
            | "BLOCK" -> (cloneFloor, clonePos)::blocks
            | _ -> blocks
        newBlocks

    Seq.initInfinite (fun i -> i)
    |> Seq.fold (fun s _ -> readClonePositionAndPrintAction s) []
    |> ignore

    0