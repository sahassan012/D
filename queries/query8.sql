-- Project0508.sql
-- 8.	For each hour of the day, list the percentage of bikes checked out during --  that hour relative to the other hours of the day.

SELECT TableA.StartingHour, (NumTrips*100.0/TotalTrips) as Percentage
FROM
    (SELECT t.StartingHour, COUNT(t.StartingHour) as NumTrips
     FROM Trips as t
     GROUP BY t.StartingHour) as TableA,

    (SELECT COUNT(*) as TotalTrips
    FROM Trips) as TableB

ORDER BY TableA.StartingHour ASC