using System.ComponentModel.DataAnnotations;

namespace MinProject.Views.Message
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
