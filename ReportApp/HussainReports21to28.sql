-- Author: Hussain Arif
-- Reports 21-28: Language Statistics and Variations
-- Database: world | Tables: country, countrylanguage

-- Report 21: Spanish-speaking countries sorted by speaker count
SELECT
    country.Name AS Country,
    country.Continent,
    country.Region,
    countrylanguage.Language,
    countrylanguage.Percentage,
    ROUND(country.Population * countrylanguage.Percentage / 100) AS Speakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language = 'Spanish'
ORDER BY Speakers DESC;

-- Report 22: Total worldwide speakers of 5 major languages
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;

-- Report 23: World population percentage for each of the 5 major languages
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers,
    ROUND(
        SUM(country.Population * countrylanguage.Percentage / 100) * 100.0 /
        (SELECT SUM(Population) FROM country), -- divides by total world population
        2
    ) AS WorldPopulationPercentage
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY countrylanguage.Language
ORDER BY WorldPopulationPercentage DESC;

-- Report 24: English-speaking countries sorted by speaker count
SELECT
    country.Name AS Country,
    country.Continent,
    country.Region,
    countrylanguage.Language,
    countrylanguage.Percentage,
    ROUND(country.Population * countrylanguage.Percentage / 100) AS Speakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language = 'English'
ORDER BY Speakers DESC;

-- Report 25: Top 10 countries by English speaker count
SELECT
    country.Name AS Country,
    country.Continent,
    country.Region,
    countrylanguage.Language,
    countrylanguage.Percentage,
    ROUND(country.Population * countrylanguage.Percentage / 100) AS Speakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language = 'English'
ORDER BY Speakers DESC
LIMIT 10;

-- Report 26: Official languages ranked by total speakers
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.IsOfficial = 'T' -- T = officially recognized
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;

-- Report 27: Non-official languages ranked by total speakers
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.IsOfficial = 'F' -- F = not officially recognized
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;

-- Report 28: Languages spoken in Asia ranked by total speakers
SELECT
    countrylanguage.Language,
    country.Continent,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE country.Continent = 'Asia'
GROUP BY countrylanguage.Language, country.Continent
ORDER BY TotalSpeakers DESC;
