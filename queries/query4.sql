-- Project0504.sql
-- 4.	For each StationID, retrieve the number of trips taken 
-- from that station and to that station (as separate values).
-- Order the results in descending order by the total
-- number of trips both from and to the station. 

SELECT TOP 10 TableA.StationID, NumTripsFrom, NumTripsTo

FROM 
    (SELECT StationID, COUNT(Trips.FromStation) as NumTripsFrom   
    FROM Stations 
    INNER JOIN Trips ON StationID = Trips.FromStation
    GROUP BY StationID) as TableA,

    (SELECT StationID, COUNT(Trips.ToStation) as NumTripsTo
    FROM Stations 
    INNER JOIN Trips ON StationID = Trips.ToStation
    GROUP BY StationID) as TableB
    
WHERE TableA.StationID = TableB.StationID

ORDER BY TableB.NumTripsTo + TableA.NumTripsFrom DESC