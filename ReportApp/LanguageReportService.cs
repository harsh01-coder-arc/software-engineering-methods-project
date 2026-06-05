using MySql.Data.MySqlClient;

namespace ReportApp.Services;

public class LanguageReportService
{
    private readonly string _connectionString;

    public LanguageReportService(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("A database connection string is required.");
        }

        _connectionString = connectionString;
    }

    public List<Dictionary<string, object>> RunQuery(string query, Dictionary<string, object>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("SQL query cannot be empty.");
        }

        var rows = new List<Dictionary<string, object>>();

        try
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            using var command = new MySqlCommand(query, connection);

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }

                rows.Add(row);
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Database error while running language report: " + ex.Message);
            throw;
        }

        return rows;
    }

    private static void CheckTextInput(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{fieldName} cannot be empty.");
        }
    }

    private static void CheckLimit(int limit)
    {
        if (limit <= 0)
        {
            throw new ArgumentException("Limit must be greater than zero.");
        }
    }

    public string GetReport21SpanishCountries()
    {
        return @"
SELECT
    c.Name AS Country,
    c.Continent,
    c.Region,
    cl.Language,
    cl.Percentage,
    ROUND(c.Population * cl.Percentage / 100) AS Speakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.Language = 'Spanish'
ORDER BY Speakers DESC;";
    }

    public string GetReport22MajorLanguageSpeakers()
    {
        return @"
SELECT
    cl.Language,
    ROUND(SUM(c.Population * cl.Percentage / 100)) AS TotalSpeakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY cl.Language
ORDER BY TotalSpeakers DESC;";
    }

    public string GetReport23MajorLanguagePercentages()
    {
        return @"
SELECT
    cl.Language,
    ROUND(SUM(c.Population * cl.Percentage / 100)) AS TotalSpeakers,
    ROUND(
        SUM(c.Population * cl.Percentage / 100) * 100.0 /
        (SELECT SUM(Population) FROM country),
        2
    ) AS WorldPopulationPercentage
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.Language IN ('Chinese', 'English', 'Hindi', 'Spanish', 'Arabic')
GROUP BY cl.Language
ORDER BY WorldPopulationPercentage DESC;";
    }

    public string GetReport24CountriesByLanguage(string language)
    {
        CheckTextInput(language, "Language");

        return @"
SELECT
    c.Name AS Country,
    c.Continent,
    c.Region,
    cl.Language,
    cl.Percentage,
    ROUND(c.Population * cl.Percentage / 100) AS Speakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.Language = @language
ORDER BY Speakers DESC;";
    }

    public string GetReport25TopCountriesByLanguage(string language, int limit)
    {
        CheckTextInput(language, "Language");
        CheckLimit(limit);

        return @"
SELECT
    c.Name AS Country,
    c.Continent,
    c.Region,
    cl.Language,
    cl.Percentage,
    ROUND(c.Population * cl.Percentage / 100) AS Speakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.Language = @language
ORDER BY Speakers DESC
LIMIT @limit;";
    }

    public string GetReport26OfficialLanguages()
    {
        return @"
SELECT
    cl.Language,
    ROUND(SUM(c.Population * cl.Percentage / 100)) AS TotalSpeakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.IsOfficial = 'T'
GROUP BY cl.Language
ORDER BY TotalSpeakers DESC;";
    }

    public string GetReport27UnofficialLanguages()
    {
        return @"
SELECT
    cl.Language,
    ROUND(SUM(c.Population * cl.Percentage / 100)) AS TotalSpeakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE cl.IsOfficial = 'F'
GROUP BY cl.Language
ORDER BY TotalSpeakers DESC;";
    }

    public string GetReport28LanguagesByContinent(string continent)
    {
        CheckTextInput(continent, "Continent");

        return @"
SELECT
    cl.Language,
    c.Continent,
    ROUND(SUM(c.Population * cl.Percentage / 100)) AS TotalSpeakers
FROM country c
JOIN countrylanguage cl ON c.Code = cl.CountryCode
WHERE c.Continent = @continent
GROUP BY cl.Language, c.Continent
ORDER BY TotalSpeakers DESC;";
    }

    public List<Dictionary<string, object>> RunReport24(string language)
    {
        CheckTextInput(language, "Language");

        return RunQuery(
            GetReport24CountriesByLanguage(language),
            new Dictionary<string, object>
            {
                { "@language", language }
            });
    }

    public List<Dictionary<string, object>> RunReport25(string language, int limit)
    {
        CheckTextInput(language, "Language");
        CheckLimit(limit);

        return RunQuery(
            GetReport25TopCountriesByLanguage(language, limit),
            new Dictionary<string, object>
            {
                { "@language", language },
                { "@limit", limit }
            });
    }

    public List<Dictionary<string, object>> RunReport28(string continent)
    {
        CheckTextInput(continent, "Continent");

        return RunQuery(
            GetReport28LanguagesByContinent(continent),
            new Dictionary<string, object>
            {
                { "@continent", continent }
            });
    }
}
