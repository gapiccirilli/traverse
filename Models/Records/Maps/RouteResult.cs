namespace Traverse.Models.Records.Maps
{
    public record RouteResult(
        Coordinate Origin,
        Coordinate Destination,
        IReadOnlyList<Route> Routes,
        IReadOnlyList<Step> Steps,
        Dictionary<int, IReadOnlyList<Coordinate>> StepPaths
    );
}