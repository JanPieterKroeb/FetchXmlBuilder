using FluentAssertions;
using TestProject1.FilterTests;

namespace TestProject1.LinkTests;

public class SimpleLinkTests
{
    private readonly FetchXmlBuilder.Builder _entityToFetchXmlBuilder = new();

    [Test]
    public void SingleLinkTest()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .LinkEntity<Artist>(
                s => s.For<Artist>(song => song.Artist, null),
                a => a.ArtistId,
                s => s.CreatedBy)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><link-entity name=\"Artist\" from=\"ArtistId\" to=\"CreatedBy\" link-type=\"outer\" alias=\"Artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void SingleLinkTest_WithAlias()
    {
        var actualXmlString = _entityToFetchXmlBuilder
            .For<Song>("song")
            .LinkEntity<Artist>(
                s => s.For<Artist>(song => song.Artist, "spotify_artist"),
                a => a.ArtistId,
                s => s.CreatedBy)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><link-entity name=\"Artist\" from=\"ArtistId\" to=\"CreatedBy\" link-type=\"outer\" alias=\"spotify_artist\"><all-attributes /></link-entity></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
}