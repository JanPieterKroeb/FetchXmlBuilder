namespace TestProject1.LinkTests.DataModel;

public abstract record Person(
    Guid Id,
    Guid? MarriedToId);