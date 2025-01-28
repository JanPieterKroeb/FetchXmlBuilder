namespace TestProject1.LinkTests.DataModel;

public record Director(Guid PersonId, Guid? MarriedTo) : Person(PersonId, MarriedTo);