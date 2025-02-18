using FetchXmlBuilder;
using FetchXmlBuilder.Domain.Enums;
using FluentAssertions;
using TestProject1.AggregateTests.DataModel;

namespace TestProject1.AggregateTests;

public class SimpleAggregateTests
{
    private readonly QueryBuilder _aggregateQueryBuilder = new();
    
    [Test]
    public void Aggregate_DistinctColumnCount()
    {
        var actual = _aggregateQueryBuilder
            .AggregateFor<Company>("company", true)
            .Aggregate(c => c.CompanyName, AggregateOperation.CountColumn, "uniqueCompanyNameAmount", true)
            .ToFetchXmlString();

        var expected = "<fetch distinct=\"True\" aggregate=\"true\"><entity name=\"company\"><attribute name=\"CompanyName\" alias=\"uniqueCompanyNameAmount\" aggregate=\"countcolumn\" distinct=\"True\"/></entity></fetch>";
        expected.Should().BeEquivalentTo(actual);
    }

    [Test]
    public void Aggregate_Average()
    {
        var actual = _aggregateQueryBuilder
            .AggregateFor<Person>("person", false)
            .Aggregate(p => p.Salary, AggregateOperation.Average, "averageSalary", false)
            .ToFetchXmlString();

        const string expected = "<fetch distinct=\"False\" aggregate=\"true\"><entity name=\"person\"><attribute name=\"Salary\" alias=\"averageSalary\" aggregate=\"avg\" distinct=\"False\"/></entity></fetch>";
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void Aggregate_HasFilter_MaxAvgAndMinRange()
    {
        var actual = _aggregateQueryBuilder
            .AggregateFor<Person>("person", false)
            .Aggregate(p => p.Salary, AggregateOperation.Min, "minSalary", false)
            .Aggregate(p => p.Salary, AggregateOperation.Max, "maxSalary", false)
            .Aggregate(p => p.Salary, AggregateOperation.Average, "averageSalary", false)
            .Filter(p => p.Gender == 1)
            .Filter(p => p.Function == "Office Manager")
            .ToFetchXmlString();
        
        var expected = "<fetch distinct=\"False\" aggregate=\"true\"><entity name=\"person\"><filter><condition attribute=\"Gender\" operator=\"eq\" value=\"1\" /><condition attribute=\"Function\" operator=\"eq\" value=\"Office Manager\" /></filter><attribute name=\"Salary\" alias=\"minSalary\" aggregate=\"min\" distinct=\"False\"/><attribute name=\"Salary\" alias=\"maxSalary\" aggregate=\"max\" distinct=\"False\"/><attribute name=\"Salary\" alias=\"averageSalary\" aggregate=\"avg\" distinct=\"False\"/></entity></fetch>";
        actual.Should().BeEquivalentTo(expected);
    }
}