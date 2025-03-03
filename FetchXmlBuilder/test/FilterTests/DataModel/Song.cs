namespace TestProject1.FilterTests.DataModel;

public record Song(
    string Name,
    Artist Artist,
    Guid? ArtistId,
    Guid CreatedBy,
    bool IsOnSpotify,
    int? ListenAmount);