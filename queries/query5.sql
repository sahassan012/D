-- Project0505.sql
-- 5.	For each customer list the number of trips they have taken.
--   Restrict the results to the 10 users who have taken the most trips.

SELECT Top 10 UserID, NumTrips
FROM 
    (SELECT Users.UserID, COUNT(Trips.UserID) as NumTrips
     FROM Users
     INNER JOIN Trips ON Users.UserID = Trips.UserID
     GROUP BY Users.UserID) as TableA
ORDER BY TableA.NumTrips DESC, UserID ASC