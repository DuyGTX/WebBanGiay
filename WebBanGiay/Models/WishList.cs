using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiay.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public string UserId { get; set;}
        [NotMapped] // Không ánh xạ vào database
        public List<string>? ImageUrls { get; set; }

        [ForeignKey("ShoeId")]
        public Shoe? Shoe { get; set;}

    }
}
