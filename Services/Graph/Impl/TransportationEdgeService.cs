using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Providers;
using Traverse.Repository;
using Traverse.Repository.Graph;
using Traverse.Services.Cache;
using Traverse.Utility.Impl;

namespace Traverse.Services.Graph.Impl
{
    public class TransportationEdgeService : IEdgeService<EventDto, Transportation>
    {
        private readonly IMapProvider<EventDto, Transportation> _mapProvider;
        private readonly IEventRepository _eventRepository;
        private readonly IEdgeRepository<Transportation> _edgeRepository;
        private readonly ICacheService _cacheService;
        private const string OPTIMIZED_CACHE_KEY = "itinerary:{0}:transportation:optimized";
        private const string USER_DEFINED_TRANSPORT_CACHE_KEY = "itinerary:{0}:transportation";
        private static readonly TimeSpan EDGE_CACHE_EXPIRY = new(1, 0, 0);

        public TransportationEdgeService(IMapProvider<EventDto, Transportation> mapProvider, IEventRepository eventRepository, 
               IEdgeRepository<Transportation> edgeRepository, ICacheService cacheService)
        {
            _mapProvider = mapProvider;
            _eventRepository = eventRepository;
            _edgeRepository = edgeRepository;
            _cacheService = cacheService;
        }

        public async Task BuildEdgesAsync(EventDto node, long id)
        {
            var cacheKey = string.Format(OPTIMIZED_CACHE_KEY, id);

            IEnumerable<EventDto> eventNodes = (await _eventRepository.GetAllEventsAsync(id)).Select(EventMapper.MapFrom);

            IEnumerable<Transportation> edges = await _mapProvider.OptimizeAsync(node, eventNodes.Where(n => n.Id != node.Id));

            if (!edges.Any())
                return;
            
            var currentCachedEdges = await GetEdges(id, OPTIMIZED_CACHE_KEY) ?? [];
            var mergedEdges = currentCachedEdges.Concat(edges);

            await _cacheService.SetAsync(cacheKey, mergedEdges, EDGE_CACHE_EXPIRY);
            await _edgeRepository.SaveEdgesAsync(edges);
        }

        public async Task BuildEdgeAsync(EventDto toNode, EventDto? fromNode, long id)
        {
            if (fromNode is null) return;

            var cacheKey = string.Format(USER_DEFINED_TRANSPORT_CACHE_KEY, id);

            Transportation edge = await _mapProvider.GetEtasAsync(toNode, fromNode);
            
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