using Traverse.Models.Dto;

namespace Traverse.Models.Graph
{
    public class ItineraryGraph
    {
        private readonly Dictionary<long, EventDto> _nodes = [];
        private readonly Dictionary<long, List<Transportation>> _edges = [];

        public Dictionary<long, EventDto> Nodes { get { return _nodes; } }
        public Dictionary<long, List<Transportation>> Edges { get { return _edges; } }

        public void AddNode(EventDto node)
        {
            _nodes[node.Id] = node;
            _edges[node.Id] = [];
        }

        public void AddEdge(Transportation edge)
        {
            _edges[edge.FromEventId].Add(edge);
        }

        public EventDto? GetNode(long eventId)
        {
            return _nodes.TryGetValue(eventId, out var node) ? node : null;
        }

        public IEnumerable<Transportation> GetEdgesFromNode(long eventId)
        {
            return _edges.TryGetValue(eventId, out var edges) ? edges : [];
        }
    }
}