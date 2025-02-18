namespace TestProject1.AggregateTests.DataModel;

public record Person(
    Guid Id,
    int Gender,
    string Function,
    string FirstName,
    double Salary,
    int ProjectAmount);
    