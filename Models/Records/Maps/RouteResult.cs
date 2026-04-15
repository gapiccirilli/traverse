namespace Traverse.Models.Records.Maps
{
    public record RouteResult(
        Coordinate Origin,
        Coordinate Destination,
        List<Route> Routes,
        List<Step> Steps,
        Dictionary<int, List<StepPath>> StepPaths
    );
}