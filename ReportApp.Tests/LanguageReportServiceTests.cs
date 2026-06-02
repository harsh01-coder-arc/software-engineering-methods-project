using Xunit;

public class LanguageReportServiceTests
{
    // Connect to the local world database for all tests
    private readonly LanguageReportService _service =
        new LanguageReportService("Server=localhost;Database=world;User=root;Password=root;");

    [Fact]
    // Check that Report 21 filters for Spanish and sorts by speakers
    public void Report21_Should_Filter_Spanish()
    {
        var query = _service.GetReport21SpanishCountries();
        Assert.Contains("Spanish", query);
        Assert.Contains("ORDER BY Speakers DESC", query);
    }

    [Fact]
    // Check that Report 22 includes all 5 major languages
    public void Report22_Should_Include_Major_Languages()
    {
        var query = _service.GetReport22MajorLanguageSpeakers();
        Assert.Contains("Chinese", query);
        Assert.Contains("English", query);
        Assert.Contains("Hindi", query);
        Assert.Contains("Spanish", query);
        Assert.Contains("Arabic", query);
    }

    [Fact]
    // Check that Report 23 calculates world population percentage using a subquery
    public void Report23_Should_Calculate_World_Percentage()
    {
        var query = _service.GetReport23MajorLanguagePercentages();
        Assert.Contains("WorldPopulationPercentage", query);
        Assert.Contains("SELECT SUM(Population) FROM country", query);
    }

    [Fact]
    // Check that Report 24 filters by the given language and sorts by speakers
    public void Report24_Should_Filter_By_Selected_Language()
    {
        var query = _service.GetReport24CountriesByLanguage("English");
        Assert.Contains("English", query);
        Assert.Contains("ORDER BY Speakers DESC", query);
    }

    [Fact]
    // Check that Report 25 applies the row limit correctly
    public void Report25_Should_Use_Limit()
    {
        var query = _service.GetReport25TopNCountriesByLanguage("English", 10);
        Assert.Contains("LIMIT 10", query);
    }

    [Fact]
    // Check that Report 26 only includes officially recognized languages
    public void Report26_Should_Filter_Official_Languages()
    {
        var query = _service.GetReport26OfficialLanguages();
        Assert.Contains("IsOfficial = 'T'", query);
    }

    [Fact]
    // Check that Report 27 only includes non-official languages
    public void Report27_Should_Filter_NonOfficial_Languages()
    {
        var query = _service.GetReport27NonOfficialLanguages();
        Assert.Contains("IsOfficial = 'F'", query);
    }

    [Fact]
    // Check that Report 28 filters by the given continent and groups correctly
    public void Report28_Should_Filter_By_Continent()
    {
        var query = _service.GetReport28LanguagesByContinent("Asia");
        Assert.Contains("Asia", query);
        Assert.Contains("GROUP BY countrylanguage.Language, country.Continent", query);
    }
}
