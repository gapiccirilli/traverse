using Traverse.Models.Dto;
using Traverse.Providers;
using Traverse.Services.Graph;

namespace Traverse.Models.Graph
{
    public class ItineraryGraphService : IGraphService<ItineraryGraph, EventDto, Transportation>
    {
        // private readonly IMapProvider<EventDto> _mapProvider;
        public ItineraryGraphService()
        {
            // _mapProvider = mapProvider;
        }

        public ItineraryGraph BuildGraph(IEnumerable<EventDto> nodes, IEnumerable<Transportation> edges)
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