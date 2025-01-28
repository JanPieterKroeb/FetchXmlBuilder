namespace TestProject1.FilterTests.DataModel;

public record Song(
    string Name,
    Artist Artist,
    Guid CreatedBy,
    bool IsOnSpotify);