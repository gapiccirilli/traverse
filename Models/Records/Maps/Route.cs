namespace Traverse.Models.Records.Maps
{
    public record Route(
        string Name,
        int DistanceMeters,
        int DurationSeconds,
        string TransportType,
        int[] StepIndexes,
        bool HasTolls
    );

    public record Step(
        int StepPathIndex,
        int DistanceMeters,
        int DurationSeconds,
        string Instructions
    );

    public record StepPath(
        Coordinate Coordinate
    );
}