namespace ReportApp.Models;

public class City
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public string Name { get; set; } = "";

    public long Population { get; set; }
}
