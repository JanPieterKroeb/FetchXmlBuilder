namespace FetchXmlBuilder.Domain.EntityProperties.Attributes;

internal class BaseAttribute(string name, string? alias)
{
    private protected string Name { get; } = name;
    private protected string? Alias { get; } = alias;

    public override string ToString() => $"<attribute name=\"{Name}\" alias=\"{Alias}\"/>";
}