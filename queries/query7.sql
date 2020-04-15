-- Project0507.sql
-- 7.	For each hour of the day, list how many bikes were checked out during that time.

SELECT *
FROM
    (SELECT t.StartingHour, COUNT(t.StartingHour) as NumTrips
     FROM Trips as t
     GROUP BY t.StartingHour) as TableA
ORDER BY TableA.StartingHour ASC