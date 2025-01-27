using FluentAssertions;

namespace TestProject1.FilterTests;

public class SimpleFilterTests
{
    private readonly FetchXmlBuilder.Builder _entityToFetchXmlBuilder = new();

    [Test]
    public void BinaryExpressionTest_EqualToNonNull()
    {

    }
    
    [Test]
    public void BinaryExpressionTest_()
    {
        var actual = _entityToFetchXmlBuilder
            .For<Song>("song")
            .Filter(s => s.Name == "Tyler The Creator")
            .ToFetchXmlString();
        const string expected1 = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"eq\" value=\"Tyler The Creator\" /></filter></entity></fetch>";
        actual.Should().Be(expected1);
    }
    
    [Test]
    public void BoolField_Test()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .Filter(a => a.IsOnSpotify)
            .ToFetchXmlString();
        const string expected2 = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"1\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected2);
    }

    [Test]
    public void BoolField_NegationTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .Filter(a => !a.IsOnSpotify)
            .ToFetchXmlString();
        const string expected2 = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"0\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected2);
    }

    [Test]
    public void MultipleConditionsTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .LinkEntity<Artist>(s => s
                    .For<Artist>(song => song.Artist),
                a => a.ArtistId,
                s => s.CreatedBy)
            .Filter(a => a.Name.StartsWith("Cry for") && a.IsOnSpotify)
            .ToFetchXmlString();
        const string expected3 = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"like\" value=\"Cry for%\" /><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"1\" /></filter><link-entity name=\"Artist\" from=\"ArtistId\" to=\"CreatedBy\" link-type=\"outer\" alias=\"Artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected3);
    }
}

public record Song(
    string Name,
    Artist Artist,
    Guid CreatedBy,
    bool IsOnSpotify);

public record Artist(
    Guid ArtistId,
    string Name);