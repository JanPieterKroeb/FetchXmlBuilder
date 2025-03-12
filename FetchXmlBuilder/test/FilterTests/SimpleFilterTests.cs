using FetchXmlBuilder;
using FluentAssertions;
using TestProject1.FilterTests.DataModel;

namespace TestProject1.FilterTests;

public class SimpleFilterTests
{
    private readonly QueryBuilder _entityToFetchXmlBuilder = new();

    [Test]
    public void BinaryExpressionTest_()
    {
        var artistId = Guid.NewGuid();
        var actual = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(s => s.Name == "Tyler The Creator")
            .Filter(s => s.ArtistId == artistId)
            .ToFetchXmlString();
        var expected1 =
            $"<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"eq\" value=\"Tyler The Creator\" /><condition attribute=\"ArtistId\" operator=\"eq\" value=\"{artistId}\" /></filter></entity></fetch>";
        actual.Should().Be(expected1);
    }

    [Test]
    public void BoolField_Test()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(a => a.IsOnSpotify)
            .ToFetchXmlString();
        const string expected2 =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"1\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected2);
    }

    [Test]
    public void BoolField_NegationTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(a => !a.IsOnSpotify)
            .ToFetchXmlString();
        const string expected2 =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"0\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected2);
    }

    [Test]
    public void MultipleConditionsTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(a => a.Name.StartsWith("Cry for") && a.IsOnSpotify)
            .LinkEntity<Artist>(
                s => s.For<Artist>(song => song.Artist, null),
                a => a.Id,
                s => s.CreatedBy)
            .ToFetchXmlString();
        const string expected3 =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"like\" value=\"Cry for%\" /><condition attribute=\"IsOnSpotify\" operator=\"eq\" value=\"1\" /></filter><link-entity name=\"Artist\" from=\"Id\" to=\"CreatedBy\" link-type=\"outer\" alias=\"Artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected3);
    }

    [Test]
    public void NullableConditionTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(a => a.ListenAmount == 500)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"ListenAmount\" operator=\"eq\" value=\"500\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void FilterByAnonymousObjectsFieldValue()
    {
        var anonymousMatchingObject = new { Name = "Pandora" };
        var actualXmlString = _entityToFetchXmlBuilder.For<Company>("company")
            .Filter(c => c.Name == anonymousMatchingObject.Name)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"company\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"eq\" value=\"Pandora\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void FilterByObjectsFieldValue()
    {
        var matchingObject = new Company(Guid.NewGuid(), "Name", null, null);
        var actualXmlString = _entityToFetchXmlBuilder.For<Company>("company")
            .Filter(c => c.Name!.StartsWith(matchingObject.Name!))
            .ToFetchXmlString();
        var t = "";
    }
}
