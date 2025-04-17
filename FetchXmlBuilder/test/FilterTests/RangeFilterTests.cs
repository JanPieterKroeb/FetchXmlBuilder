using System.Linq.Expressions;
using FetchXmlBuilder;
using FluentAssertions;
using TestProject1.FilterTests.DataModel;

namespace TestProject1.FilterTests;

public class RangeFilterTests
{
    private readonly QueryBuilder _entityToFetchXmlBuilder = new();

    
    [Test]
    public void BetweenExclusiveFilter()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(s => s.ListenAmount > 500 && s.Name.StartsWith("Da") && s.ListenAmount < 5000)
            .ToFetchXmlString();
        const string expected =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"ListenAmount\" operator=\"gt\" value=\"500\" /><condition attribute=\"Name\" operator=\"like\" value=\"Da%\" /><condition attribute=\"ListenAmount\" operator=\"lt\" value=\"5000\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void BetweenInclusiveFilter()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Song>("song")
            .Filter(s => s.ListenAmount >= 500 && s.Name.StartsWith("Da") && s.ListenAmount <= 5000)
            .ToFetchXmlString();
        const string expected =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"song\"><all-attributes /><filter><condition attribute=\"ListenAmount\" operator=\"ge\" value=\"500\" /><condition attribute=\"Name\" operator=\"like\" value=\"Da%\" /><condition attribute=\"ListenAmount\" operator=\"le\" value=\"5000\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void BetweenInclusiveFilterWithStrings()
    {
        var actualXmlString = _entityToFetchXmlBuilder.For<Company>("company")
            .Filter(s => s.PostalCode, ExpressionType.GreaterThanOrEqual, "1211")
            .ToFetchXmlString();
        const string expected =
            "<fetch returntotalrecordcount=\"true\"><entity name=\"company\"><all-attributes /><filter><condition attribute=\"PostalCode\" operator=\"ge\" value=\"1211\" /></filter></entity></fetch>";
        actualXmlString.Should().BeEquivalentTo(expected);
    }
}