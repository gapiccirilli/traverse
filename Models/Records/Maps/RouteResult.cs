using Newtonsoft.Json;

namespace Traverse.Models.Records.Maps
{
    public record RouteResult(
        CoordinateWrapper Origin,
        CoordinateWrapper Destination,
        IReadOnlyList<Route> Routes,
        IReadOnlyList<Step> Steps,
        IReadOnlyList<IReadOnlyList<Coordinate>> StepPaths,
        Dictionary<int, IReadOnlyList<Coordinate>> StepPathsDictionary
    );
}