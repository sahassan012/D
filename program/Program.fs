// F# program to analyze Divvy daily ride data.
//

#light

module project04

//
// ParseLine and ParseInput
//
// Given a sequence of strings representing Divvy data, 
// parses the strings and returns a list of lists.  Each
// sub-list denotes one bike ride.  Example:
//
//   [ [15,22,141,17,5,1124]; ... ]
//
// The values are station id (from), station id (to), bike
// id, starting hour (0..23), starting day of week (0 Sunday-6 Saturday)
// and trip duration (secs), 
//
let ParseLine (line:string) = 
  let tokens = line.Split(',')
  let ints = Array.map System.Int32.Parse tokens
  Array.toList ints

let rec ParseInput lines = 
  let rides = Seq.map ParseLine lines
  Seq.toList rides

//
//
//  Count:
//    Takes int x and int list L as parameters
//    Recursively Counts the amount of times x is present in L 
//    Returns the total Count
//
let rec Count (x: int)(L: int list) =
  match L with
  | [] -> 0
  | e::rest when e=x -> 1 + (Count x rest)
  | _::rest -> 0 + (Count x rest)

//
//
//  Contains:
//    Recursively checks if parameter L(List) Contains parameter x(Int)
//    Returns true only if x exists in L
//
//
let rec Contains x L =
  match L with
  | []  -> false
  | e::_  when e = x -> true
  | _::rest -> (Contains x rest)

//
//
//  Distinct:
//    Function takes a list of lists(lst) as parameter
//    Returns a list (newList) with the distict heads of lists in lst
//  
//
let Distinct(lst : 'a list) =
  let f item newList =
    match newList with
    | [] -> [item]
    | _ -> 
      match ((Contains item newList)) with
      | false -> item :: newList 
      | true -> newList
  List.foldBack f lst []

//
//
//  CountBikeDur:
//   Takes parameter x (Int) and L (Int List List) 
//   Recursively calculates the bike ride duration with matching BikeID(x) 
//   Returns the seconds of duration as an int for a specific BikeID 
// 
// 
let rec CountBikeDur (x: int)(L: int list list) =
  match L with
  | [] -> 0
  | e::rest when (List.head e ) = x -> List.head(List.tail (List.tail (List.tail e))) + CountBikeDur x rest
  | _::rest -> 0 + (CountBikeDur x rest)

//
//
//  CountBikeRides:
//    Takes parameter x (Int) and L (Int List List)
//    Recursively calculates the bike rides with matching BikeID(x) 
//    Returns the total count of rides
//  
//
let rec CountBikeRides (x: int)(L: int list list) =
  match L with
  | [] -> 0
  | e::rest when (List.head e ) = x -> 1 + CountBikeRides x rest
  | _::rest -> 0 + CountBikeRides x rest

//
//
//  ContainsToStat:
//    Takes parameter x (Int) and L (Int List List)
//    Recursively checks if x exists in L and returns true only when it does
//  
//  
let rec ContainsToStat x (L: int list list) =
  match L with
  | []  -> false
  | e::_  when (List.head e) = x -> true
  | _::rest -> (ContainsToStat x rest)

//
//
//  CountBikeRidesToStat:
//    Takes parameter x (Int) and L (Int List List)
//    Recursively calculates the bike rides with matching ToStationIDs
//    Returns the total count of rides    
//    
//
let rec CountBikeRidesToStat (x: int)(L: int list list) =
  match L with
  | [] -> 0
  | e::rest when (List.head e ) = x -> 1 + (CountBikeRidesToStat x rest)
  | _::rest -> 0 + CountBikeRidesToStat x rest

//
//
//  CountToStatRides:
//    Takes parameter lst (Int List List) and newList (Int List List)
//    If StationID does not exist in newList, recursively adds a list [x,y] to a list of lists (newList) where x is the StationID and y is the number of rides taken to it
//    Returns the list (newList) with lists of [x,y]
//
//
let rec CountToStatRides (lst: int list list) (newList: int list list) =
  match lst with
  | [] -> newList
  | e::rest when (not (ContainsToStat (List.head e) newList)) -> CountToStatRides rest ( [List.head e; (CountBikeRidesToStat (List.head e)(lst))] :: newList)
  | _::rest -> CountToStatRides rest newList

//
//
//  CountToStatDur:
//   Takes parameter x (Int) and L (Int List List) 
//   Recursively calculates the bike ride duration with matching stationIDs(x) 
//   Returns the seconds of duration to a specified station in an int
//  
//
let rec CountToStatDur (x: int)(L: int list list) =
  match L with
  | [] -> 0
  | e::rest when (List.head e ) = x -> List.head(List.tail(List.tail (List.tail (List.tail e)))) + CountToStatDur x rest
  | _::rest -> 0 + (CountToStatDur x rest)

//
//
// GetTail:
//  Takes a list as a parameter and returns its tail 
//
//
let GetTail lst =
  match lst with
  | [] -> []
  | x::xs -> xs

//
//
//  Printstars:
//    Takes the number of stars and recursively prints them
//
//
let rec Printstars n = 
 match n with
 | 0 -> ()
 | 1 -> printf "*"
 | _ -> printf "*"
        Printstars (n-1)

//
//
//  main:
//
//
[<EntryPoint>]
let main argv =
  
  //
  // input file name, then input divvy ride data and build
  // a list of lists:
  //
  printf "filename> "
  let filename = System.Console.ReadLine()
  let contents = System.IO.File.ReadLines(filename)
  let ridedata = ParseInput contents

  //Print the number of rides in the file
  let N = List.length ridedata
  printfn ""
  printfn "# of rides: %A" N
  printfn ""

  //Get 'from station IDS' from the heads
  let fromStationIDs2 = List.map List.head ridedata
  
  //Store tail into new list and get 'to station IDs' from the heads 
  let ridedata = List.map GetTail ridedata
  let toStatRideTail = ridedata
  let toStationIDs2 = List.map List.head ridedata

  //Store tail into new list and get 'Bike ids' from the heads 
  let ridedata = List.map GetTail ridedata
  let bikeIDs = List.map List.head ridedata
  let N = List.length (Distinct bikeIDs)
  
  //Print the length of N that depicts # of bikes
  printfn "# of bikes: %A" N
  printfn ""

  //Prompt user for Bike ID and print # of rides with matching ID
  printf "BikeID> "
  let input = System.Console.ReadLine()
  let bikeID = input |> int

  //Print the Count for the entered bikeID
  printfn ""
  printf "# of rides for BikeID %A" bikeID
  let bikeRides = Count bikeID bikeIDs
  printfn ": %A" bikeRides
  printfn ""
  
  //Print total time spent riding bike with bikeID entered by user
  let bikeIDDuration = CountBikeDur bikeID ridedata 
  printf "Total time spent riding BikeID %A: %A" bikeID (bikeIDDuration/60)
  printf " minutes %A " (bikeIDDuration % 60) 
  printfn "seconds"
  printfn ""

  //Print average time spent riding bike with bikeID entered by user
  let averageDur = float(bikeIDDuration) / float(CountBikeRides bikeID ridedata)
  printf "Average time spent riding BikeID %A: %.2f" bikeID averageDur
  printf " seconds"
  printfn ""

  //Prompt user for Station ID and print # of rides to this station
  printfn ""
  printf "StationID> "
  let input = System.Console.ReadLine()
  let stationID = input |> int
  
  //Print the number of rides to the StationID entered
  let ridesToStation = Count stationID toStationIDs2
  printfn ""
  printfn "# of rides to StationID %A: %A" stationID ridesToStation
  printfn ""
  
  //Print total time spent riding bike with bikeID entered by user
  let totalTimeBikeID = CountToStatDur stationID toStatRideTail 
  let averageTimeSpentBikeID = float(totalTimeBikeID) / float(ridesToStation)
  printf "Average time spent on trips leading to StationID %A: %.2f" stationID averageTimeSpentBikeID
  printfn " seconds"
  printfn ""

  //Store tail into new list and get 'Starting hour' from the heads 
  let ridedata = List.map GetTail ridedata
  let startHr = List.map List.head ridedata

  //Store tail into new list and get 'Starting day' from the heads 
  let ridedata = List.map GetTail ridedata
  let startDay = List.map List.head ridedata

  //Store the total count of trips for each day
  let sundays = (Count 0 startDay)
  let mondays = (Count 1 startDay)
  let tuesdays = (Count 2 startDay)
  let wednesdays = (Count 3 startDay)
  let thursdays = (Count 4 startDay)
  let fridays = (Count 5 startDay)
  let saturdays = (Count 6 startDay)

  //Print the number of trips for each day
  printfn "Number of Trips on Sunday: %A" sundays
  printfn "Number of Trips on Monday: %A"  mondays
  printfn "Number of Trips on Tuesday: %A" tuesdays
  printfn "Number of Trips on Wednesday: %A" wednesdays
  printfn "Number of Trips on Thursday: %A" thursdays
  printfn "Number of Trips on Friday: %A" fridays
  printfn "Number of Trips on Saturday: %A" saturdays
  printfn ""

  //Print the number of stars for sunday
  printf "0: " 
  Printstars (sundays/10)
  printfn " %A" sundays
  //Print the number of stars for monday 
  printf "1: " 
  Printstars (mondays/10)
  printfn " %A" mondays
  //Print the number of stars for tuesday 
  printf "2: " 
  Printstars (tuesdays/10)
  printfn " %A" tuesdays
  //Print the number of stars for wednesday
  printf "3: " 
  Printstars (wednesdays/10)
  printfn " %A" wednesdays
  //Print the number of stars for thursday
  printf "4: " 
  Printstars (thursdays/10)
  printfn " %A" thursdays
  //Print the number of stars for friday
  printf "5: " 
  Printstars (fridays/10)
  printfn " %A" fridays
  //Print the number of stars for saturday
  printf "6: " 
  Printstars (saturdays/10)
  printfn " %A" saturdays

  //Store tail into new list and get 'Trip Duration' from the heads 
  let ridedata = List.map GetTail ridedata
  let tripDur = List.map List.head ridedata

  //Store all of the stationIDs and their total rides respectively
  let listToStatCount = CountToStatRides toStatRideTail []

  //Sort the list of lists that contains [stationID, numRides] descending by numRides first and then ascending by stationID 
  let sortedList = List.sortBy (fun ([stationID ; numRides]) -> -numRides, stationID  ) listToStatCount
 
  //Print out the top 10 lists of stationIDs and their respective count of rides
  let e::rest = sortedList
  printfn ""
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))

  let e::rest = rest
  printfn "# of rides to station %A: %A"  (List.head e) (List.head (List.tail e))
  printfn ""

  0
