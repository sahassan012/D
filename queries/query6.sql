-- Project0506.sql
-- 6.	For each age group (year) among customers, list the average 
--  ride duration among all trips customers of that age group took.  

SELECT Top 10 Age, AverageTripDurationPerAgeGroup
FROM 
    (SELECT (2019 - BirthYear) as Age, (SUM(Trips.TripDuration) / COUNT(Users.UserID)) as AverageTripDurationPerAgeGroup 
     FROM Users
     LEFT JOIN Trips ON Users.UserID = Trips.UserID
     GROUP BY Users.BirthYear) as TableA
ORDER BY TableA.AverageTripDurationPerAgeGroup DESC