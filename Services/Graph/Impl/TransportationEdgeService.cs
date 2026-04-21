using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Providers;

namespace Traverse.Services.Graph.Impl
{
    public class TransportationEdgeService : IEdgeService<EventDto, Transportation>
    {
        private readonly IMapProvider<EventDto, Transportation> _mapProvider;
        private readonly IEventService _eventService;

        public TransportationEdgeService(IMapProvider<EventDto, Transportation> mapProvider, IEventService eventService)
        {
            _mapProvider = mapProvider;
            _eventService = eventService;
        }

        public async Task BuildEdgesAsync(IEnumerable<EventDto> nodes)
        {
            throw new NotImplementedException();
        }

        public async Task BuildEdgesAsync(EventDto node, long id)
        {
            IEnumerable<EventDto> eventNodes = await _eventService.GetAllEventsAsync(id);

            IEnumerable<Transportation> edges = await _mapProvider.GetEtasAsync(node, eventNodes.Where(n => n.Id != node.Id));

            if (!edges.Any())
                return;
            
            
        }
    }
}