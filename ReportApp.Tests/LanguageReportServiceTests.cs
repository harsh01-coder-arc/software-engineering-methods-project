using ReportApp.Services;
using Xunit;

namespace ReportApp.Tests;

public class LanguageReportServiceTests
{
    private readonly LanguageReportService _service =
        new LanguageReportService("Server=localhost;Database=world;User=root;Password=root;");

    [Fact]
    public void Report21_Should_Show_Spanish_Countries()
    {
        var sql = _service.GetReport21SpanishCountries();

        Assert.Contains("Spanish", sql);
        Assert.Contains("ORDER BY Speakers DESC", sql);
    }

    [Fact]
    public void Report22_Should_Use_Five_Main_Languages()
    {
        var sql = _service.GetReport22MajorLanguageSpeakers();

        Assert.Contains("Chinese", sql);
        Assert.Contains("English", sql);
        Assert.Contains("Hindi", sql);
        Assert.Contains("Spanish", sql);
        Assert.Contains("Arabic", sql);
    }

    [Fact]
    public void Report23_Should_Calculate_Global_Percentage()
    {
        var sql = _service.GetReport23MajorLanguagePercentages();

        Assert.Contains("WorldPopulationPercentage", sql);
        Assert.Contains("SELECT SUM(Population) FROM country", sql);
    }

    [Fact]
    public void Report24_Should_Use_Language_Parameter()
    {
        var sql = _service.GetReport24CountriesByLanguage("English");

        Assert.Contains("@language", sql);
        Assert.DoesNotContain("'English'", sql);
    }

    [Fact]
    public void Report24_Should_Not_Accept_Empty_Language()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.GetReport24CountriesByLanguage(""));
    }

    [Fact]
    public void Report25_Should_Use_Language_And_Limit_Parameters()
    {
        var sql = _service.GetReport25TopCountriesByLanguage("English", 10);

        Assert.Contains("@language", sql);
        Assert.Contains("LIMIT @limit", sql);
    }

    [Fact]
    public void Report25_Should_Not_Accept_Zero_Limit()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.GetReport25TopCountriesByLanguage("English", 0));
    }

    [Fact]
    public void Report26_Should_Filter_Official_Languages()
    {
        var sql = _service.GetReport26OfficialLanguages();

        Assert.Contains("IsOfficial = 'T'", sql);
    }

    [Fact]
    public void Report27_Should_Filter_Unofficial_Languages()
    {
        var sql = _service.GetReport27UnofficialLanguages();

        Assert.Contains("IsOfficial = 'F'", sql);
    }

    [Fact]
    public void Report28_Should_Use_Continent_Parameter()
    {
        var sql = _service.GetReport28LanguagesByContinent("Asia");

        Assert.Contains("@continent", sql);
        Assert.Contains("GROUP BY cl.Language, c.Continent", sql);
    }

    [Fact]
    public void Report28_Should_Not_Accept_Empty_Continent()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.GetReport28LanguagesByContinent(""));
    }
}
