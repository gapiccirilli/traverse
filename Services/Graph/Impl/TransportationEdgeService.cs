using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Providers;
using Traverse.Repository.Graph;
using Traverse.Services.Cache;

namespace Traverse.Services.Graph.Impl
{
    public class TransportationEdgeService : IEdgeService<EventDto, Transportation>
    {
        private readonly IMapProvider<EventDto, Transportation> _mapProvider;
        private readonly IEventService _eventService;
        private readonly IEdgeRepository<Transportation> _edgeRepository;
        private readonly ICacheService _cacheService;

        private static readonly TimeSpan EDGE_CACHE_EXPIRY = new(1, 0, 0);

        public TransportationEdgeService(IMapProvider<EventDto, Transportation> mapProvider, IEventService eventService, 
               IEdgeRepository<Transportation> edgeRepository, ICacheService cacheService)
        {
            _mapProvider = mapProvider;
            _eventService = eventService;
            _edgeRepository = edgeRepository;
            _cacheService = cacheService;
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
            
            await _cacheService.SetAsync<IEnumerable<Transportation>>("", edges, EDGE_CACHE_EXPIRY);
            await _edgeRepository.SaveEdgesAsync(edges);
        }
    }
}