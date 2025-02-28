using FetchXmlBuilder;
using FetchXmlBuilder.ParameterOptions;
using FluentAssertions;
using Console = TestProject1.AttributeTests.DataModel.Console;

namespace TestProject1.AttributeTests;

public class RowAggregateTests
{
    private static readonly QueryBuilder QueryBuilder = new();

    [Test]
    public void SimpleRequest_WithRowAggregateAttribute()
    {
        var segaCompanyId = Guid.NewGuid();
        var genesisId = Guid.NewGuid();
        var actualString = QueryBuilder.For<Console>("consoles")
            .CountChildren(c => c.Id, "amountOfChildrenConsoles")
            .Filter(c => c.ManufacturerId == segaCompanyId)
            .FilterByHierarchy(c => c.Id, genesisId, HierarchyFilterOptions.Under)
            .ToFetchXmlString();
        var expectedString = $"<fetch returntotalrecordcount=\"true\"><entity name=\"consoles\"><all-attributes /><filter><condition attribute=\"ManufacturerId\" operator=\"eq\" value=\"{segaCompanyId}\" /><condition attribute=\"Id\" operator=\"under\" value=\"{genesisId}\" /></filter><attribute name=\"Id\" alias=\"amountOfChildrenConsoles\" rowaggregate=\"CountChildren\"/></entity></fetch>";
        actualString.Should().BeEquivalentTo(expectedString);
    }
}