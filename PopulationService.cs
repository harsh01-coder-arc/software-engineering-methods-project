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
        var countries = new List<Country>();

        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        string query = @"
            SELECT *
            FROM Countries
            ORDER BY Population DESC
            LIMIT @topN";

        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@topN", topN);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            countries.Add(new Country
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name"),
                Population = reader.GetInt64("Population")
            });
        }

        return countries;
    }

    public List<City> GetTopCities(int topN)
    {
        var cities = new List<City>();

        using var connection = new MySqlConnection(_connectionString);

        connection.Open();

        string query = @"
            SELECT *
            FROM Cities
            ORDER BY Population DESC
            LIMIT @topN";

        using var cmd = new MySqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@topN", topN);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            cities.Add(new City
            {
                Id = reader.GetInt32("Id"),
                CountryId = reader.GetInt32("CountryId"),
                Name = reader.GetString("Name"),
                Population = reader.GetInt64("Population")
            });
        }

        return cities;
    }
}
