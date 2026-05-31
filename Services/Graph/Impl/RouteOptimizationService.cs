using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Models.Records.Maps;
using Traverse.Providers;
using Traverse.Services.Maps;
using Traverse.Utility.Impl;

namespace Traverse.Services.Graph.Impl
{
    public class RouteOptimizationService : IOptimizationService<long, ItineraryGraph>
    {
        // private readonly IMapProvider<EventDto> _mapProvider;
        private readonly IEventService _eventService;
        private readonly IEdgeService<EventDto, Transportation> _transportationEdgeService;
        private readonly IGraphService<ItineraryGraph, EventDto, Transportation> _itineraryGraphService;
        public RouteOptimizationService(IEventService eventService, IEdgeService<EventDto, Transportation> transportationEdgeService, 
                IGraphService<ItineraryGraph, EventDto, Transportation> itineraryGraphService)
        {
            _eventService = eventService;
            _transportationEdgeService = transportationEdgeService;
            _itineraryGraphService = itineraryGraphService;
        }

        public async Task<ItineraryGraph> Optimize(long optParam)
        {
            IEnumerable<EventDto> events = await _eventService.GetAllEventsAsync(optParam);
            IEnumerable<Transportation> edges = await _transportationEdgeService.GetEdges(optParam);

            var itineraryGraph = _itineraryGraphService.BuildGraph(events, edges);
            
            return new ItineraryGraph();
        }

        private IEnumerable<Transportation> BuildRouteEdges(Dictionary<long, IEnumerable<EtaWrapper>> routeEtas)
        {
            return [];
        }
    }
}