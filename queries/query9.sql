-- Project0509.sql
-- 9.	The station contains a location as latitude and longitude.  Compute for 
--  each trip the distance covered by that trip, using the following equation to 
--  approximate: sqrt( (69 miles * difference in latitude)^2 + (52 miles * 
--  difference in longitude)^2 ).  For this computation, use the SQRT function and 
--  SQUARE function in SQL.  For the 10 longest trips, return the trip ID, 
--  starting station ID, ending station ID, and distance travelled as Distance.

SELECT TOP 10 TableA.TripID, TableA.FromStation, TableB.ToStation, SQRT(SQUARE(69 * (TableB.latitude - TableA.latitude)) + SQUARE(52 * (TableB.longitude - TableA.longitude)) ) as Distance
FROM

    (SELECT TripID, FromStation, longitude, latitude
     FROM Trips
     LEFT JOIN Stations ON Stations.StationID = Trips.FromStation) as TableA,

     (SELECT TripID, ToStation, longitude, latitude
     FROM Trips
     LEFT JOIN Stations ON Stations.StationID = Trips.ToStation) as TableB
WHERE TableA.TripID = TableB.TripID
ORDER BY Distance DESC