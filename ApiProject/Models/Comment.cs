using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Comment
    {

        public int Id { get; set; }

        public string Title { get; set; }   

        public string Content { get; set; }

        public DateTime CreateOn { get; set; }
        public int StockId { get; set; }


        [ForeignKey("StockId")]
        public Stock Stock { get; set; }    
    }
}
