using System.ComponentModel.DataAnnotations.Schema;
using Traverse.Models.Enum;

namespace Traverse.Models
{
    [Table("ratings", Schema = "core")]
    public class Rating
    {
        public long Id { get; set; }
        public RatingValue Value { get; set; }
        public long EventId { get; set; }

        public Rating()
        {
            this.Value = RatingValue.Unrated;
        }

        public Rating(RatingValue value)
        {
            this.Value = value;
        }

        public Rating(int id, RatingValue value, long eventId)
        {
            this.Id = id;
            this.Value = value;
            this.EventId = eventId;
        }
    }
}