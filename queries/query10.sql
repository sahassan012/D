-- Project0510.sql
-- 10.	Compute for each trip the average speed of the bicyclist, by taking the 
--  distance travelled computed in the previous question (which is in miles) and 
--  divide by the length in hours (the length is stored in seconds).  For the 10 
--  fastest trips, return the trip ID, bike ID, and the speed as mph.

SELECT Top 10 TableA.TripID, TableA.BikeID, (TableA.TripDuration*1./60/60) as Hrs, ( SQRT(SQUARE(69 * (TableB.latitude - TableA.latitude)) + SQUARE(52 * (TableB.longitude - TableA.longitude)) ) /  (TableA.TripDuration*1./60/60))as mph
FROM
    (SELECT TripID, FromStation, BikeID, longitude, latitude, TripDuration
     FROM Trips
     LEFT JOIN Stations ON Stations.StationID = Trips.FromStation) as TableA,

     (SELECT TripID, ToStation, longitude, latitude
     FROM Trips
     LEFT JOIN Stations ON Stations.StationID = Trips.ToStation) as TableB
WHERE TableA.TripID = TableB.TripID
ORDER BY mph DESC