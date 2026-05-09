using System;
using MySql.Data.MySqlClient;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string connectionString =
Environment.GetEnvironmentVariable("DB_CONNECTION");
        int maxRetries = 20;
        int delayBetweenRetries = 5000;
        
        Console.WriteLine("Attempting to connect to MySQL...");
        using (var connection = new MySqlConnection(connectionString))
        {
            int attempt = 0;
            bool connected = false;
            
            while (attempt < maxRetries && !connected)
            {
                try
                {
                    connection.Open();
                    connected = true;
                    Console.WriteLine("Connected to MySQL!");
                }
                catch (MySqlException ex)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt} failed. Retrying in {delayBetweenRetries / 1000} seconds...");
                    Thread.Sleep(delayBetweenRetries);
                }
            }
            
            if (!connected)
            {
                Console.WriteLine("Unable to connect to MySQL after multiple attempts.");
                return;
            }
            
            try
            {
                string query = "SELECT ID, Name, CountryCode, Population FROM city LIMIT 10";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("ID | Name | CountryCode | Population");
                    Console.WriteLine("-----------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["ID"]} | {reader["Name"]} | {reader["CountryCode"]} | {reader["Population"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
