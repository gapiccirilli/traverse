using Traverse.Models;
using Traverse.Models.Records.Maps;
using Traverse.Services.Maps;

namespace Traverse.Services.Graph.Impl
{
    public class RouteOptimizationService : IOptimizationService<Event, EtaResult>
    {
        public Task<IEnumerable<EtaResult>> Optimize(List<Event> values)
        {
            throw new NotImplementedException();
        }
    }
}