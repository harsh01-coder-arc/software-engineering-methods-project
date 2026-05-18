-- Report 11: Every capital city, ranked by size

SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
ORDER BY   city.Population DESC;



SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
WHERE      country.Continent = 'Asia'          -- swap continent name here
ORDER BY   city.Population DESC;



SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
WHERE      country.Region = 'Southern and Central Asia'  -- swap region name here
ORDER BY   city.Population DESC;



SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
ORDER BY   city.Population DESC
LIMIT      10;                                 -- change N here


-- -----------------------------------------------
-- Report 15: Top 10 most populated capitals
--            in a given continent (example: Europe)
-- -----------------------------------------------
SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
WHERE      country.Continent = 'Europe'        -- swap continent name here
ORDER BY   city.Population DESC
LIMIT      10;                                 -- change N here


-- -----------------------------------------------
-- Report 16: Top 10 most populated capitals
--            in a given region (example: Western Europe)
-- -----------------------------------------------
SELECT
    city.Name        AS "Capital City",
    country.Name     AS "Country",
    country.Continent,
    country.Region,
    city.Population  AS "City Population"
FROM       country
INNER JOIN city ON country.Capital = city.ID
WHERE      country.Region = 'Western Europe'   -- swap region name here
ORDER BY   city.Population DESC
LIMIT      10;                                 -- change N here




-- -----------------------------------------------
-- Report 17: Urban vs Rural split — by continent
-- -----------------------------------------------
SELECT
    country.Continent,
    SUM(country.Population)                                                          AS "Total Population",
    SUM(cities.TotalCityPop)                                                         AS "Living in Cities",
    SUM(country.Population) - SUM(cities.TotalCityPop)                              AS "Living Outside Cities",
    ROUND( SUM(cities.TotalCityPop)                          / SUM(country.Population) * 100, 2) AS "% In Cities",
    ROUND((SUM(country.Population) - SUM(cities.TotalCityPop)) / SUM(country.Population) * 100, 2) AS "% Outside Cities"
FROM country
INNER JOIN (
    -- Pre-aggregate city populations at the country level
    SELECT  CountryCode,
            SUM(Populatio
