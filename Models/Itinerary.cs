using System.ComponentModel.DataAnnotations.Schema;

namespace Traverse.Models
{
    [Table("itineraries", Schema = "core")]
    public class Itinerary
    {
        public long Id { get; set; }
        public required string ItineraryName { get; set; }
        public long TripId { get; set; }

        public Itinerary()
        {
            this.ItineraryName = string.Empty;
        }
    }
}