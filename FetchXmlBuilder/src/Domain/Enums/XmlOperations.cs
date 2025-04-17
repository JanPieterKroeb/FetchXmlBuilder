namespace FetchXmlBuilder.Domain.Enums;

internal struct XmlOperations
{
    public const string Equal = "eq";
    public const string NotEqual = "ne";
    public const string IsNull = "null";
    public const string IsNotNull = "not-null";
    public const string Like = "like";
    public const string NotLike = "not-like";
    public const string LessThan = "lt";
    public const string LessThanOrEqual = "le";
    public const string GreaterThan = "gt";
    public const string GreaterThanOrEqual = "ge";
}