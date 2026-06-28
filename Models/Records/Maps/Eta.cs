namespace Traverse.Models.Records.Maps
{
    public record Eta(
        Coordinate Destination,
        int DistanceMeters,
        int ExpectedTravelTimeSeconds,
        int StaticTravelTimeSeconds
    );
}