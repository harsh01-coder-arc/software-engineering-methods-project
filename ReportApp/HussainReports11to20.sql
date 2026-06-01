-- ============================================================
--  WORLD DATABASE REPORTS 11 - 20
-- ============================================================


-- ============================================================
-- Report 11: Top 10 Most Populated Cities in the World
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
ORDER BY ci.Population DESC
LIMIT 10;


-- ============================================================
-- Report 12: Top 10 Most Populated Cities in a Specific Continent
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Continent = 'Asia'          -- change continent here
ORDER BY ci.Population DESC
LIMIT 10;


-- ============================================================
-- Report 13: Top 10 Most Populated Cities in a Specific Region
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Region = 'Western Europe'   -- change region here
ORDER BY ci.Population DESC
LIMIT 10;


-- ============================================================
-- Report 14: Top 10 Most Populated Cities in a Specific Country
-- ============================================================
SELECT
    ci.Name         AS city_name,
    c.Name          AS country_name,
    c.Continent     AS continent,
    c.Region        AS region,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE c.Name = 'Germany'            -- change country here
ORDER BY ci.Population DESC
LIMIT 10;


-- ============================================================
-- Report 15: Top 10 Most Populated Cities in a Specific District
-- ============================================================
SELECT
    ci.Name         AS city_name,
    ci.District     AS district,
    c.Name          AS country_name,
    ci.Population   AS population
FROM city ci
JOIN country c ON c.Code = ci.CountryCode
WHERE ci.District = 'Scotland'      -- change district here
ORDER BY ci.Population DESC
LIMIT 10;


-- ============================================================
-- Shared CTE for Reports 16 - 20
-- (Aggregates city population per country)
-- ============================================================
WITH city_pop AS (
    SELECT
        cty.CountryCode,
        SUM(cty.Population) AS city_population
    FROM city cty
    GROUP BY cty.CountryCode
)

-- ============================================================
-- Report 16: Population Breakdown by Continent (Cities vs Non-Cities)
-- ============================================================
SELECT
    c.Continent                                                                   AS continent,
    SUM(c.Population)                                                             AS total_population,
    SUM(COALESCE(cp.city_population, 0))                                          AS population_living_in_cities,
    SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))                     AS population_not_living_in_cities,
    ROUND(
        SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population), 2
    )                                                                             AS city_population_percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population), 2
    )                                                                             AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
GROUP BY c.Continent
ORDER BY c.Continent;


-- ============================================================
-- Report 17: Population Breakdown by Country (Cities vs Non-Cities)
-- ============================================================
WITH city_pop AS (
    SELECT
        cty.CountryCode,
        SUM(cty.Population) AS city_population
    FROM city cty
    GROUP BY cty.CountryCode
)
SELECT
    c.Name                                                                        AS country_name,
    c.Continent                                                                   AS continent,
    c.Region                                                                      AS region,
    SUM(c.Population)                                                             AS total_population,
    SUM(COALESCE(cp.city_population, 0))                                          AS population_living_in_cities,
    SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))                     AS population_not_living_in_cities,
    ROUND(
        SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population), 2
    )                                                                             AS city_population_percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population), 2
    )                                                                             AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
GROUP BY c.Code, c.Name, c.Continent, c.Region
ORDER BY c.Name;


-- ============================================================
-- Report 18: Population Breakdown by Region (Cities vs Non-Cities)
-- ============================================================
WITH city_pop AS (
    SELECT
        cty.CountryCode,
        SUM(cty.Population) AS city_population
    FROM city cty
    GROUP BY cty.CountryCode
)
SELECT
    c.Region                                                                      AS region,
    SUM(c.Population)                                                             AS total_population,
    SUM(COALESCE(cp.city_population, 0))                                          AS population_living_in_cities,
    SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))                     AS population_not_living_in_cities,
    ROUND(
        SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population), 2
    )                                                                             AS city_population_percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population), 2
    )                                                                             AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
GROUP BY c.Region
ORDER BY c.Region;


-- ============================================================
-- Report 19: Population Breakdown for a Specific Continent
-- ============================================================
WITH city_pop AS (
    SELECT
        cty.CountryCode,
        SUM(cty.Population) AS city_population
    FROM city cty
    GROUP BY cty.CountryCode
)
SELECT
    c.Continent                                                                   AS continent,
    SUM(c.Population)                                                             AS total_population,
    SUM(COALESCE(cp.city_population, 0))                                          AS population_living_in_cities,
    SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))                     AS population_not_living_in_cities,
    ROUND(
        SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population), 2
    )                                                                             AS city_population_percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population), 2
    )                                                                             AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
WHERE c.Continent = 'Asia'          -- change continent here
GROUP BY c.Continent;


-- ============================================================
-- Report 20: Population Breakdown for a Specific Region
-- ============================================================
WITH city_pop AS (
    SELECT
        cty.CountryCode,
        SUM(cty.Population) AS city_population
    FROM city cty
    GROUP BY cty.CountryCode
)
SELECT
    c.Region                                                                      AS region,
    SUM(c.Population)                                                             AS total_population,
    SUM(COALESCE(cp.city_population, 0))                                          AS population_living_in_cities,
    SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))                     AS population_not_living_in_cities,
    ROUND(
        SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population), 2
    )                                                                             AS city_population_percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population), 2
    )                                                                             AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
WHERE c.Region = 'Western Europe'   -- change region here
GROUP BY c.Region;
