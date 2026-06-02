using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ReportApp.Services
{
    public class CountryCityReportService
    {
        private readonly string _connectionString;

        public CountryCityReportService(string connectionString)
        {
            _connectionString = connectionString;
        }

        // ============================================================
        // Report 1: All Countries Worldwide Sorted by Population
        // ============================================================
        public List<Dictionary<string, object>> GetAllCountriesByPopulation()
        {
            string query = @"
                SELECT c.Code, c.Name, c.Continent, c.Region, c.Population, ci.Name AS Capital
                FROM country c
                LEFT JOIN city ci ON ci.ID = c.Capital
                ORDER BY c.Population DESC";

            return ExecuteQuery(query);
        }

        // ============================================================
        // Report 2: All Countries in a Specific Continent
        // ============================================================
        public List<Dictionary<string, object>> GetCountriesByContinent(string continent)
        {
            if (string.IsNullOrWhiteSpace(continent))
                throw new ArgumentException("Continent cannot be empty");

            string query = @"
                SELECT c.Code, c.Name, c.Continent, c.Region, c.Population, ci.Name AS Capital
                FROM country c
                LEFT JOIN city ci ON ci.ID = c.Capital
                WHERE c.Continent = @continent
                ORDER BY c.Population DESC";

            return ExecuteQuery(query, ("@continent", continent));
        }

        // ============================================================
        // Report 3: All Countries in a Specific Region
        // ============================================================
        public List<Dictionary<string, object>> GetCountriesByRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region cannot be empty");

            string query = @"
                SELECT c.Code, c.Name, c.Continent, c.Region, c.Population, ci.Name AS Capital
                FROM country c
                LEFT JOIN city ci ON ci.ID = c.Capital
                WHERE c.Region = @region
                ORDER BY c.Population DESC";

            return ExecuteQuery(query, ("@region", region));
        }

        // ============================================================
        // Report 4: Top N Most Populated Countries Worldwide
        // ============================================================
        public List<Dictionary<string, object>> GetTopNCountries(int topN)
        {
            if (topN <= 0)
                throw new ArgumentException("N must be greater than 0");

            string query = @"
                SELECT c.Code, c.Name, c.Continent, c.Region, c.Population, ci.Name AS Capital
                FROM country c
                LEFT JOIN city ci ON ci.ID = c.Capital
                ORDER BY c.Population DESC
                LIMIT @topN";

            return ExecuteQuery(query, ("@topN", topN));
        }

        // ============================================================
        // Report 5: Top N Countries in a Specific Continent
        // ============================================================
        public List<Dictionary<string, object>> GetTopNCountriesByContinent(string continent, int topN)
        {
            if (string.IsNullOrWhiteSpace(continent))
                throw new ArgumentException("Continent cannot be empty");
            if (topN <= 0)
                throw new ArgumentException("N must be greater than 0");

            string query = @"
                SELECT c.Code, c.Name, c.Continent, c.Region, c.Population, ci.Name AS Capital
                FROM country c
                LEFT JOIN city ci ON ci.ID = c.Capital
                WHERE c.Continent = @continent
                ORDER BY c.Population DESC
                LIMIT @topN";

            return ExecuteQuery(query, ("@continent", continent), ("@topN", topN));
        }

        // ============================================================
        // Report 6: All Cities Worldwide Sorted by Population
        // ============================================================
        public List<Dictionary<string, object>> GetAllCitiesByPopulation()
        {
            string query = @"
                SELECT ci.Name AS City, c.Name AS Country, c.Continent, c.Region, ci.District, ci.Population
                FROM city ci
                JOIN country c ON c.Code = ci.CountryCode
                ORDER BY ci.Population DESC";

            return ExecuteQuery(query);
        }

        // ============================================================
        // Report 7: All Cities in a Specific Continent
        // ============================================================
        public List<Dictionary<string, object>> GetCitiesByContinent(string continent)
        {
            if (string.IsNullOrWhiteSpace(continent))
                throw new ArgumentException("Continent cannot be empty");

            string query = @"
                SELECT ci.Name AS City, c.Name AS Country, c.Continent, c.Region, ci.District, ci.Population
                FROM city ci
                JOIN country c ON c.Code = ci.CountryCode
                WHERE c.Continent = @continent
                ORDER BY ci.Population DESC";

            return ExecuteQuery(query, ("@continent", continent));
        }

        // ============================================================
        // Report 8: All Cities in a Specific Region
        // ============================================================
        public List<Dictionary<string, object>> GetCitiesByRegion(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
                throw new ArgumentException("Region cannot be empty");

            string query = @"
                SELECT ci.Name AS City, c.Name AS Country, c.Continent, c.Region, ci.District, ci.Population
                FROM city ci
                JOIN country c ON c.Code = ci.CountryCode
                WHERE c.Region = @region
                ORDER BY ci.Population DESC";

            return ExecuteQuery(query, ("@region", region));
        }

        // ============================================================
        // Report 9: Top N Most Populated Cities Worldwide
        // ============================================================
        public List<Dictionary<string, object>> GetTopNCities(int topN)
        {
            if (topN <= 0)
                throw new ArgumentException("N must be greater than 0");

            string query = @"
                SELECT ci.Name AS City, c.Name AS Country, c.Continent, c.Region, ci.District, ci.Population
                FROM city ci
                JOIN country c ON c.Code = ci.CountryCode
                ORDER BY ci.Population DESC
                LIMIT @topN";

            return ExecuteQuery(query, ("@topN", topN));
        }

        // ============================================================
        // Report 10: Top N Most Populated Cities in a Specific Continent
        // ============================================================
        public List<Dictionary<string, object>> GetTopNCitiesByContinent(string continent, int topN)
        {
            if (string.IsNullOrWhiteSpace(continent))
                throw new ArgumentException("Continent cannot be empty");
            if (topN <= 0)
                throw new ArgumentException("N must be greater than 0");

            string query = @"
                SELECT ci.Name AS City, c.Name AS Country, c.Continent, c.Region, ci.District, ci.Population
                FROM city ci
                JOIN country c ON c.Code = ci.CountryCode
                WHERE c.Continent = @continent
                ORDER BY ci.Population DESC
                LIMIT @topN";

            return ExecuteQuery(query, ("@continent", continent), ("@topN", topN));
        }

        // ============================================================
        // Helper: Execute Query and Return Results
        // ============================================================
        private List<Dictionary<string, object>> ExecuteQuery(string query, params (string, object)[] parameters)
        {
            var results = new List<Dictionary<string, object>>();

            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                using var cmd = new MySqlCommand(query, connection);

                foreach (var (name, value) in parameters)
                    cmd.Parameters.AddWithValue(name, value);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader.IsDBNull(i) ? "N/A" : reader.GetValue(i);
                    results.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }

            return results;
        }

        // ============================================================
        // Helper: Display Results in Console
        // ============================================================
        public void DisplayResults(List<Dictionary<string, object>> results, string reportTitle)
        {
            Console.WriteLine($"\n{'=',60}");
            Console.WriteLine($" {reportTitle}");
            Console.WriteLine($"{'=',60}");

            if (results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            foreach (var row in results)
            {
                foreach (var col in row)
                    Console.Write($"{col.Key}: {col.Value,-20} | ");
                Console.WriteLine();
            }

            Console.WriteLine($"\nTotal results: {results.Count}");
        }
    }
}
