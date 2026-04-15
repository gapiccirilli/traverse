using NetTopologySuite.Geometries;
using Traverse.Models;
using Traverse.Models.Dto;

namespace Traverse.Utility.Impl
{
    public class TripMapper : IMapper<Trip, TripDto>
    {
        public static TripDto MapFrom(Trip input)
        {
            return new TripDto
            {
                Id = input.Id,
                TripName = input.TripName,
                ArrivalLocation = input.ArrivalLocation,
                ArrivalCoordinates = new Models.Records.Coordinate(input.ArrivalCoordinates.X, input.ArrivalCoordinates.Y),
                DepartureLocation = input.DepartureLocation,
                DepartureCoordinates = new Models.Records.Coordinate(input.DepartureCoordinates.X, input.DepartureCoordinates.Y),
                TripStart = input.TripStart,
                TripEnd = input.TripEnd
            };
        }

        public static Trip MapTo(TripDto input)
        {
            return new Trip
            {
                Id = input.Id,
                TripName = input.TripName,
                TripStart = input.TripStart.ToUniversalTime(),
                TripEnd = input.TripEnd.ToUniversalTime(),
                DepartureLocation = input.DepartureLocation,
                DepartureCoordinates = new Point(input.DepartureCoordinates.Latitude, input.DepartureCoordinates.Longitude),
                ArrivalLocation = input.ArrivalLocation,
                ArrivalCoordinates = new Point(input.ArrivalCoordinates.Latitude, input.ArrivalCoordinates.Longitude)
            };
        }
    }
}