namespace Traverse.Models.Records.Maps
{
    public record EtaResult(
        Coordinate Destination,
        int DistanceMeters,
        int ExpectedTravelTimeSeconds,
        int StaticTravelTimeSeconds
    );
}