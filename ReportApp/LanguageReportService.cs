using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class LanguageReportService
{
    private readonly string _connectionString;

    // Store the database connection string when the service is created
    public LanguageReportService(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Runs any SQL query and returns the results as a list of rows
    public List<Dictionary<string, object>> ExecuteReport(string query)
    {
        var results = new List<Dictionary<string, object>>();

        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        // Read each row and store column name + value as a dictionary
        while (reader.Read())
        {
            var row = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }

            results.Add(row);
        }

        return results;
    }

    // Report 21: Spanish-speaking countries sorted by speaker count
    public string GetReport21SpanishCountries()
    {
        return @"
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
ORDER BY Speakers DESC;";
    }

    // Report 22: Total worldwide speakers of 5 major languages
    public string GetReport22MajorLanguageSpeakers()
    {
        return @"
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;";
    }

    // Report 23: What percentage of the world speaks each major language
    public string GetReport23MajorLanguagePercentages()
    {
        return @"
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers,
    ROUND(
        SUM(country.Population * countrylanguage.Percentage / 100) * 100.0 /
        (SELECT SUM(Population) FROM country),
        2
    ) AS WorldPopulationPercentage
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY countrylanguage.Language
ORDER BY WorldPopulationPercentage DESC;";
    }

    // Report 24: Countries where a given language is spoken, sorted by speaker count
    public string GetReport24CountriesByLanguage(string language)
    {
        return $@"
SELECT
    country.Name AS Country,
    country.Continent,
    country.Region,
    countrylanguage.Language,
    countrylanguage.Percentage,
    ROUND(country.Population * countrylanguage.Percentage / 100) AS Speakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language = '{language}'
ORDER BY Speakers DESC;";
    }

    // Report 25: Top N countries by speaker count for a given language
    public string GetReport25TopNCountriesByLanguage(string language, int limit)
    {
        return $@"
SELECT
    country.Name AS Country,
    country.Continent,
    country.Region,
    countrylanguage.Language,
    countrylanguage.Percentage,
    ROUND(country.Population * countrylanguage.Percentage / 100) AS Speakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.Language = '{language}'
ORDER BY Speakers DESC
LIMIT {limit};";
    }

    // Report 26: Officially recognized languages ranked by total speakers
    public string GetReport26OfficialLanguages()
    {
        return @"
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.IsOfficial = 'T'
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;";
    }

    // Report 27: Non-official languages ranked by total speakers
    public string GetReport27NonOfficialLanguages()
    {
        return @"
SELECT
    countrylanguage.Language,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE countrylanguage.IsOfficial = 'F'
GROUP BY countrylanguage.Language
ORDER BY TotalSpeakers DESC;";
    }

    // Report 28: Languages spoken in a given continent, ranked by total speakers
    public string GetReport28LanguagesByContinent(string continent)
    {
        return $@"
SELECT
    countrylanguage.Language,
    country.Continent,
    ROUND(SUM(country.Population * countrylanguage.Percentage / 100)) AS TotalSpeakers
FROM country
INNER JOIN countrylanguage ON country.Code = countrylanguage.CountryCode
WHERE country.Continent = '{continent}'
GROUP BY countrylanguage.Language, country.Continent
ORDER BY TotalSpeakers DESC;";
    }
}
