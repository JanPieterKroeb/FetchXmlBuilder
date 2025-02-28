namespace FetchXmlBuilder.Domain.Enums;

public struct HierarchyFilterOperation
{
    public const string Under = "under";
    public const string UnderOrEqual = "eq-or-under";
    public const string Above = "above";
    public const string AboveOrEqual = "eq-or-above";
    public const string NotUnder = "not-under";
}