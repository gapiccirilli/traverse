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
        private const string OPTIMIZED_CACHE_KEY = "itinerary:{0}:transportation:optimized";
        private const string USER_DEFINED_TRANSPORT_CACHE_KEY = "itinerary:{0}:transportation";
        private static readonly TimeSpan EDGE_CACHE_EXPIRY = new(1, 0, 0);

        public TransportationEdgeService(IMapProvider<EventDto, Transportation> mapProvider, IEventService eventService, 
               IEdgeRepository<Transportation> edgeRepository, ICacheService cacheService)
        {
            _mapProvider = mapProvider;
            _eventService = eventService;
            _edgeRepository = edgeRepository;
            _cacheService = cacheService;
        }

        public async Task BuildEdgesAsync(EventDto node, long id)
        {
            var cacheKey = string.Format(OPTIMIZED_CACHE_KEY, id);

            IEnumerable<EventDto> eventNodes = await _eventService.GetAllEventsAsync(id);

            IEnumerable<Transportation> edges = await _mapProvider.OptimizeAsync(node, eventNodes.Where(n => n.Id != node.Id));

            if (!edges.Any())
                return;
            
            var currentCachedEdges = await GetEdges(id, OPTIMIZED_CACHE_KEY) ?? [];
            var mergedEdges = currentCachedEdges.Concat(edges);

            await _cacheService.SetAsync(cacheKey, mergedEdges, EDGE_CACHE_EXPIRY);
            await _edgeRepository.SaveEdgesAsync(edges);
        }

        public async Task BuildEdgeAsync(EventDto node, long id)
        {
            var cacheKey = string.Format(USER_DEFINED_TRANSPORT_CACHE_KEY, id);

            EventDto previousNode = await _eventService.GetMostRecentEventAsync(id);

            Transportation edge = await _mapProvider.GetEtasAsync(node, previousNode);
            
            var currentCachedEdges = await GetEdges(id, USER_DEFINED_TRANSPORT_CACHE_KEY) ?? [];
            var mergedEdges = currentCachedEdges.Append(edge);

            await _cacheService.SetAsync(cacheKey, mergedEdges, EDGE_CACHE_EXPIRY);
            await _edgeRepository.SaveEdgeAsync(edge);
        }

        public async Task<IEnumerable<Transportation>> GetEdges(long id, string cacheKey)
        {
            var key = string.Format(cacheKey, id);

            var edges = await _cacheService.GetAsync<IEnumerable<Transportation>>(key);

            if (edges is not null)
                return edges;
            
            edges = await _edgeRepository.GetEdgesAsync(id);
            await _cacheService.SetAsync(key, edges, EDGE_CACHE_EXPIRY);

            return edges;
        }

        public Task<Transportation> GetEdge(long id)
        {
            throw new NotImplementedException();
        }
    }
}