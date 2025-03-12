namespace TestProject1.OrderTests.DataModel;

public record Dog(
    Guid Id,
    string Name,
    Guid FatherId,
    Dog Father,
    Guid MotherId,
    Dog Mother,
    Guid BreedId,
    Breed Breed);