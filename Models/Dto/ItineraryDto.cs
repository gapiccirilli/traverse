namespace Traverse.Models.Dto
{
    public class ItineraryDto
    {
        public long Id { get; set; }
        public string ItineraryName { get; set; }
        public long TripId { get; set; }

        public ItineraryDto()
        {
            this.ItineraryName = string.Empty;
        }
        
        public ItineraryDto(string itineraryName)
        {
            this.ItineraryName = itineraryName;
        }

        public ItineraryDto(int id, string itineraryName, long tripId)
        {
            this.Id = id;
            this.ItineraryName = itineraryName;
            this.TripId = tripId;
        }
    }
}