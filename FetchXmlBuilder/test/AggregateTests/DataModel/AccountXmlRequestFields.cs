namespace TestProject1.AggregateTests.DataModel;

public record AccountXmlRequestFields(
    string name,
    string accountnumber,
    Guid? fnv_sector,
    Guid? fnv_subsector,
    Guid? fnv_businesssubgroup,
    string address1_line1,
    string address1_postalcode,
    string address1_city,
    Guid? fnv_unionrepresentative,
    Guid? fnv_previousunionrepresentative);