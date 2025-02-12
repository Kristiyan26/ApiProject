using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.DTOs.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; } = DateTime.Now;
        public int StockId { get; set; }

    }
}
