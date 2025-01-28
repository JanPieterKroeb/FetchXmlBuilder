namespace TestProject1.LinkTests.DataModel;

public record Movie(
    string Name, 
    Guid? DirectorId,
    Director? Director);