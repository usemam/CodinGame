(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let pi = 3.14

let degToRad (x:float) = x*pi/180.0

type Point =
    {Number:int; Name:string; Address:string; Phone:string; Long:float; Lat:float}
type Point with
    member a.distance b =
        let aLong = degToRad a.Long
        let bLong = degToRad b.Long
        let aLat = degToRad a.Lat
        let bLat = degToRad b.Lat
        let x = (bLong - aLong)*cos((aLat + bLat)/2.0)
        let y = bLat - aLat
        sqrt(x*x + y*y)*6371.0

[<EntryPoint>]
let main argv =
    
    let arrayToPoint (arr:string []) =
        {
            Name = arr.[1];
            Number = arr.[0] |> int;
            Address = arr.[2];
            Phone = arr.[3];
            Long = arr.[4] |> Convert.ToDouble;
            Lat = arr.[5] |> Convert.ToDouble
        }

    let LON = Console.In.ReadLine() |> Convert.ToDouble
    let LAT = Console.In.ReadLine() |> Convert.ToDouble

    let p = {
        Number = 0;
        Name = ""
        Address = "";
        Phone = "";
        Long = LON;
        Lat = LAT}

    let N = int(Console.In.ReadLine())
    let points =
        [0..N-1]
        |> List.map (fun i -> Console.In.ReadLine())
        |> List.map (fun s -> s.Split [|';'|])
        |> List.map (fun arr -> arrayToPoint arr)
    let closest =
        points |> List.minBy (fun x -> x.distance p)

    printfn "%s" closest.Name

    let enter = Console.In.ReadLine()
    
    0
