using MySql.Data.MySqlClient;
using ReportApp.Models;

namespace ReportApp.Services;

public class PopulationService
{
    private readonly string _connectionString;

    public PopulationService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Country> GetTopCountries(int topN)
    {
        if (topN <= 0)
        {
            throw new ArgumentException("topN must be greater than 0");
        }

        var countries = new List<Country>();

        try
        {
            using var connection = new MySqlConnection(_connectionString);

            connection.Open();

            string query = @"
                SELECT Code, Name, Population
                FROM country
                ORDER BY Population DESC
                LIMIT @topN";

            using var cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@topN", topN);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                countries.Add(new Country
                {
                    Code = reader.GetString("Code"),
                    Name = reader.GetString("Name"),
                    Population = reader.GetInt64("Population")
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }

        return countries;
    }

    public List<City> GetTopCities(int topN)
    {
        if (topN <= 0)
        {
            throw new ArgumentException("topN must be greater than 0");
        }

        var cities = new List<City>();

        try
        {
            using var connection = new MySqlConnection(_connectionString);

            connection.Open();

            string query = @"
                SELECT ID, Name, CountryCode, Population
                FROM city
                ORDER BY Population DESC
                LIMIT @topN";

            using var cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@topN", topN);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cities.Add(new City
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    CountryCode = reader.GetString("CountryCode"),
                    Population = reader.GetInt64("Population")
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }

        return cities;
    }
}
