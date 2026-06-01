-- ============================================================
--  WORLD DATABASE REPORTS 1 - 10
--  Author: Harsh
--  Description: Countries and Cities population reports
-- ============================================================

-- ============================================================
-- Report 1: All Countries Worldwide Sorted by Population
-- ============================================================
SELECT
    c.Code          AS country_code,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    c.Population    AS population,
    ci.Name         AS capital
FROM country c
LEFT JOIN city ci ON ci.ID = c.Capital
ORDER BY c.Population DESC;

-- ============================================================
-- Report 2: All Countries in a Specific Continent Sorted by Population
-- ============================================================
SELECT
    c.Code          AS country_code,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    c.Population    AS population,
    ci.Name         AS capital
FROM country c
LEFT JOIN city ci ON ci.ID = c.Capital
WHERE c.Continent = 'Asia'          -- change continent here
ORDER BY c.Population DESC;

-- ============================================================
-- Report 3: All Countries in a Specific Region Sorted by Population
-- ============================================================
SELECT
    c.Code          AS country_code,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    c.Population    AS population,
    ci.Name         AS capital
FROM country c
LEFT JOIN city ci ON ci.ID = c.Capital
WHERE c.Region = 'Western Europe'   -- change region here
ORDER BY c.Population DESC;

-- ============================================================
-- Report 4: Top N Most Populated Countries Worldwide
-- ============================================================
SELECT
    c.Code          AS country_code,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    c.Population    AS population,
    ci.Name         AS capital
FROM country c
LEFT JOIN city ci ON ci.ID = c.Capital
ORDER BY c.Population DESC
LIMIT 10;                           -- change N here

-- ============================================================
-- Report 5: Top N Most Populated Countries in a Specific Continent
-- ============================================================
SELECT
    c.Code          AS country_code,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    c.Population    AS population,
    ci.Name         AS capital
FROM country c
LEFT JOIN city ci ON ci.ID = c.Capital
WHERE c.Continent = 'Asia'          -- change continent here
ORDER BY c.Population DESC
LIMIT 10;                           -- change N here

-- ============================================================
-- Report 6: All Cities Worldwide Sorted by Population
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.District     AS district,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
ORDER BY ci.Population DESC;

-- ============================================================
-- Report 7: All Cities in a Specific Continent Sorted by Population
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.District     AS district,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Continent = 'Asia'          -- change continent here
ORDER BY ci.Population DESC;

-- ============================================================
-- Report 8: All Cities in a Specific Region Sorted by Population
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.District     AS district,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Region = 'Western Europe'   -- change region here
ORDER BY ci.Population DESC;

-- ============================================================
-- Report 9: Top N Most Populated Cities Worldwide
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.District     AS district,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
ORDER BY ci.Population DESC
LIMIT 10;                           -- change N here

-- ============================================================
-- Report 10: Top N Most Populated Cities in a Specific Continent
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.District     AS district,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Continent = 'Asia'          -- change continent here
ORDER BY ci.Population DESC
LIMIT 10;                           -- change N here
