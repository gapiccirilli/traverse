namespace Traverse.Models.Records.Maps
{
    public record Route(
        string Name,
        int DistanceMeters,
        int DurationSeconds,
        string TransportType,
        IReadOnlyList<int> StepIndexes,
        bool HasTolls
    );

    public record Step(
        int StepPathIndex,
        int DistanceMeters,
        int DurationSeconds,
        string? Instructions
    );

    public record StepPath(
        IReadOnlyList<IReadOnlyList<Coordinate>> PathCoordinates
    );
}