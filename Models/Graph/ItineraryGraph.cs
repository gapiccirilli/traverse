namespace Traverse.Models.Graph
{
    public class ItineraryGraph
    {
        private readonly Dictionary<long, Event> _nodes = [];
        private readonly Dictionary<long, List<Transportation>> _edges = [];

        public void AddNode(Event node)
        {
            _nodes[node.Id] = node;
            _edges[node.Id] = [];
        }

        public void AddEdge(Transportation edge)
        {
            _edges[edge.FromEventId].Add(edge);
        }

        public Event? GetNode(long eventId)
        {
            return _nodes.TryGetValue(eventId, out var node) ? node : null;
        }

        public IEnumerable<Transportation> GetEdgesFromNode(long eventId)
        {
            return _edges.TryGetValue(eventId, out var edges) ? edges : [];
        }
    }
}