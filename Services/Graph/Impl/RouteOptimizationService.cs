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
        private readonly IGraphService<ItineraryGraph, EventDto, Transportation> _itineraryGraphService;
        public RouteOptimizationService()
        {
            // _mapProvider = mapProvider;
            // _eventService = eventService;
            // _itineraryGraphService = itineraryGraphService;
        }

        public async Task<ItineraryGraph> Optimize(long optParam)
        {
            // IEnumerable<EventDto> events = await _eventService.GetAllEventsAsync(optParam);
            // Dictionary<long, IEnumerable<EtaWrapper>> etaResults = await _mapProvider.GetEtasAsync(events);

            // // 1. Inject ItineraryGraphService -- DONE
            // // 2. Use etaResults to build out edges (Transportation objects) by finding shortest time between nodes and select that destination node
            // IEnumerable<Transportation> edges = BuildRouteEdges(etaResults);
            // // 3. Pass in event nodes and transportation edges into BuildGraph()
            // return _itineraryGraphService.BuildGraph(events, edges);
            // // 4. Implement GetGraph
            // // 5. Return ItineraryGraph
            // // return await ;
            return new ItineraryGraph();
        }

        private IEnumerable<Transportation> BuildRouteEdges(Dictionary<long, IEnumerable<EtaWrapper>> routeEtas)
        {
            
            return routeEtas
                .Where(entry => entry.Value?.Any() == true)
                .Select(entry =>
                {
                    var closestEvent = entry.Value.MinBy(w => w.Etas?.FirstOrDefault()?.ExpectedTravelTimeSeconds ?? double.MaxValue);

                    var bestEta = closestEvent?.Etas?.FirstOrDefault();

                    if (bestEta == null)
                    {
                        throw new InvalidOperationException($"Event {entry.Key} has no valid ETA segments.");
                    }

                    return new Transportation
                    {
                        FromEventId = entry.Key,
                        ToEventId = closestEvent.EventId,
                        WeightSeconds = bestEta.ExpectedTravelTimeSeconds,
                        TransportMode = TransportMode.Automobile,
                        Distance = bestEta.DistanceMeters,
                        DistanceUnit = DistanceUnit.Meters
                    };
                });

        }
    }
}