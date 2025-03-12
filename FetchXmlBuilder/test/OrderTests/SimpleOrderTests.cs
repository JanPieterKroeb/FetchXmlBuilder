using FetchXmlBuilder;
using FluentAssertions;
using TestProject1.OrderTests.DataModel;

namespace TestProject1.OrderTests;

public class SimpleOrderTests
{
    private readonly QueryBuilder _entityToFetchXmlBuilder = new();
    
    [Test]
    public void FetchRequest_SimpleAscendingOrder()
    {
        var request = _entityToFetchXmlBuilder.For<Dog>("dogs")
            .OrderBy(d => d.Name)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"dogs\"><all-attributes /><order attribute=\"Name\" descending=\"False\" /></entity></fetch>";
        request.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void FetchRequest_FilterLinkEntityAndDescendingOrder()
    {
        var request = _entityToFetchXmlBuilder.For<Dog>("dogs")
            .Filter(d => d.Name.StartsWith("Las"))
            .OrderBy(d => d.Name, true)
            .LinkEntity<Dog>(
                d => d.For<Dog>(d => d.Father),
                f => f.Id,
                d => d.FatherId)
            .ToFetchXmlString();
        const string expected = "<fetch returntotalrecordcount=\"true\"><entity name=\"dogs\"><all-attributes /><filter><condition attribute=\"Name\" operator=\"like\" value=\"Las%\" /></filter><order attribute=\"Name\" descending=\"True\" /><link-entity name=\"Father\" from=\"Id\" to=\"FatherId\" link-type=\"outer\" alias=\"Father\"><all-attributes /></link-entity></entity></fetch>";
        request.Should().BeEquivalentTo(expected);
    }
}