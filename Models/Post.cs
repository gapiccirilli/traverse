using System.ComponentModel.DataAnnotations.Schema;

namespace Traverse.Models
{
    [Table("posts", Schema = "core")]
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public long EventId { get; set; }

        public Post()
        {
            this.Title = string.Empty;
            this.Content = string.Empty;
            InitializeTimestamps();
        }

        public Post(long id, string title, string content, long eventId)
        {
            this.Id = id;
            this.Title = title;
            this.Content = content;
            this.EventId = eventId;
            InitializeTimestamps();
        }

        private void InitializeTimestamps() 
        {
            this.CreatedOn = DateTime.UtcNow;
            this.UpdatedOn = DateTime.UtcNow;
        }
    }
}
