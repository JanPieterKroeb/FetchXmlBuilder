namespace TestProject1.FilterTests.DataModel;

public record Company(
    Guid Id,
    string Name,
    Guid? ParentCompanyId,
    Company? ParentCompany);