using FetchXmlBuilder.Domain;

namespace TestProject1;

public class Tests
{
    private readonly FetchXmlQueryCollection<Account> _fetchXmlBuilder = new("account");
    
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void Test1()
    {
        var test = _fetchXmlBuilder
            .Filter(a => a.name.StartsWith("flame S") && a.accountNumber.Contains("alkm"))
            .ToFetchXmlString();
        Assert.Pass();
    }
}

public record Account(string name, string accountNumber);