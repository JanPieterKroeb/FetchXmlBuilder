using FluentAssertions;
using TestProject1.FilterTests.DataModel;
using TestProject1.LinkTests.DataModel;

namespace TestProject1.LinkTests;

public class SimpleLinkTests
{
    private readonly FetchXmlBuilder.QueryBuilder _entityToFetchXmlBuilder = new();

    [Test]
    public void SingleLinkTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .LinkEntity<Artist>(
                s => s.For<Artist>(song => song.Artist),
                a => a.Id,
                s => s.CreatedBy)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><link-entity name=\"Artist\" from=\"Id\" to=\"CreatedBy\" link-type=\"outer\" alias=\"Artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void SingleLinkTest_WithAlias()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .LinkEntity<Artist>(
                s => s.For<Artist>(song => song.Artist, "spotify_artist"),
                a => a.Id,
                s => s.CreatedBy)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><link-entity name=\"Artist\" from=\"Id\" to=\"CreatedBy\" link-type=\"outer\" alias=\"spotify_artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void SingleLinkTest_WithNullableEntity()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Movie>("movie")
            .LinkEntity<Director>(
                s => s.For<Director>(m => m.Director, "test"),
                d => d.PersonId,
                m => m.DirectorId)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"movie\"><all-attributes /><link-entity name=\"Director\" from=\"PersonId\" to=\"DirectorId\" link-type=\"outer\" alias=\"test\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
}