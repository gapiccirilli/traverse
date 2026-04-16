using Traverse.Providers;
using Traverse.Services.Graph;

namespace Traverse.Models.Graph
{
    public class ItineraryGraphService : IGraphService<ItineraryGraph, Event, Transportation>
    {
        private readonly IMapProvider<Event> _mapProvider;
        public ItineraryGraphService(IMapProvider<Event> mapProvider)
        {
            _mapProvider = mapProvider;
        }

        public ItineraryGraph BuildGraph(IEnumerable<Event> nodes, IEnumerable<Transportation> edges)
        {
            
            var graph = new ItineraryGraph();

            foreach (var node in nodes)
            {
                graph.AddNode(node);
            }

            foreach (var edge in edges)
            {
                graph.AddEdge(edge);
            }

            return graph;
        }

        public Task<ItineraryGraph> GetGraphAsync(long itineraryId)
        {
            throw new NotImplementedException();
        }

        // private 
    }
}