namespace TestProject1.AttributeTests.DataModel;

public record Console(
    Guid Id,
    Guid? ManufacturerId,
    Manufacturer Manufacturer,
    uint Year,
    Guid ParentConsoleId,
    Console ParentConsole);