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




-- Report 17: City vs. non-city population by continent
SELECT
    c.Continent AS Continent,
    SUM(c.Population) AS Total_Population,
    SUM(COALESCE(cp.CityPopulation, 0)) AS Population_Living_In_Cities,
    SUM(c.Population) - SUM(COALESCE(cp.CityPopulation, 0)) AS Population_Not_Living_In_Cities,
    ROUND(
        SUM(COALESCE(cp.CityPopulation, 0)) * 100.0 / SUM(c.Population),
        2
    ) AS City_Population_Percentage,
    ROUND(
        (SUM(c.Population) - SUM(COALESCE(cp.CityPopulation, 0))) * 100.0 / SUM(c.Population),
        2
    ) AS Non_City_Population_Percentage
FROM country AS c
LEFT JOIN (
    SELECT
        CountryCode,
        SUM(Population) AS CityPopulation
    FROM city
    GROUP BY CountryCode
) AS cp
    ON c.Code = cp.CountryCode
GROUP BY c.Continent
ORDER BY c.Continent;

-- Report 18 : Population breakdown of people living in cities and not living in cities in each region
WITH city_pop AS (
  SELECT
    cty.CountryCode,
    SUM(cty.Population) AS city_population
  FROM city cty
  GROUP BY cty.CountryCode
)
SELECT
  c.Region AS region,
  SUM(c.Population) AS total_population,
  SUM(COALESCE(cp.city_population, 0)) AS population_living_in_cities,
  SUM(c.Population) - SUM(COALESCE(cp.city_population, 0)) AS population_not_living_in_cities,
  ROUND(
    SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population),
    2
  ) AS city_population_percentage,
  ROUND(
    (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population),
    2
  ) AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
GROUP BY c.Region
ORDER BY c.Region;

-- Report 19: Population breakdown of people living in cities and not living in cities in a specific continent
WITH city_pop AS (
  SELECT
    cty.CountryCode,
    SUM(cty.Population) AS city_population
  FROM city cty
  GROUP BY cty.CountryCode
)
SELECT
  c.Continent AS continent,
  SUM(c.Population) AS total_population,
  SUM(COALESCE(cp.city_population, 0)) AS population_living_in_cities,
  SUM(c.Population) - SUM(COALESCE(cp.city_population, 0)) AS population_not_living_in_cities,
  ROUND(
    SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population),
    2
  ) AS city_population_percentage,
  ROUND(
    (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population),
    2
  ) AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
WHERE c.Continent = 'Asia'
GROUP BY c.Continent;

-- Report 20: Population breakdown of people living in cities and not living in cities in a specific region
WITH city_pop AS (
  SELECT
    cty.CountryCode,
    SUM(cty.Population) AS city_population
  FROM city cty
  GROUP BY cty.CountryCode
)
SELECT
  c.Region AS region,
  SUM(c.Population) AS total_population,
  SUM(COALESCE(cp.city_population, 0)) AS population_living_in_cities,
  SUM(c.Population) - SUM(COALESCE(cp.city_population, 0)) AS population_not_living_in_cities,
  ROUND(
    SUM(COALESCE(cp.city_population, 0)) * 100.0 / SUM(c.Population),
    2
  ) AS city_population_percentage,
  ROUND(
    (SUM(c.Population) - SUM(COALESCE(cp.city_population, 0))) * 100.0 / SUM(c.Population),
    2
  ) AS non_city_population_percentage
FROM country c
LEFT JOIN city_pop cp ON cp.CountryCode = c.Code
WHERE c.Region = 'Western Europe'
GROUP BY c.Region;

