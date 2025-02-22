using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class Portfolio
        //or AppUserStock : Many to many table
    {
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public int StockId { get; set; }


        [ForeignKey("StockId")]
        public Stock Stock { get; set; }    


    }
}
