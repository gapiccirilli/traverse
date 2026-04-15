using System.ComponentModel.DataAnnotations.Schema;

namespace Traverse.Models.Dto
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long EventId { get; set; }

        public PostDto()
        {
            this.Title = string.Empty;
            this.Content = string.Empty;
        }

        public PostDto(long id, string title, string content, long eventId)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.EventId = eventId;
        }
    }
}
