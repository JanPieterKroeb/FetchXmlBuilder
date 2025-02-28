using FetchXmlBuilder;
using FetchXmlBuilder.ParameterOptions;
using FluentAssertions;
using TestProject1.FilterTests.DataModel;

namespace TestProject1.FilterTests;

public class UnderFilterTests
{
    private readonly QueryBuilder _builder = new();

    [Test]
    public void FilterByLowerHierarchyFetchXml()
    {
        var parentCompanyId = Guid.NewGuid();
        var actualXmlString = _builder.For<Company>("companies")
            .FilterByHierarchy(c => c.Id, parentCompanyId, HierarchyFilterOptions.Under)
            .Filter(c => c.Name.StartsWith("BabaBrew I"))
            .ToFetchXmlString();
        var expectedXmlString = $"<fetch returntotalrecordcount=\"true\"><entity name=\"companies\"><all-attributes /><filter><condition attribute=\"Id\" operator=\"under\" value=\"{parentCompanyId}\" /><condition attribute=\"Name\" operator=\"like\" value=\"BabaBrew I%\" /></filter></entity></fetch>";
        expectedXmlString.Should().BeEquivalentTo(actualXmlString);
    }
}