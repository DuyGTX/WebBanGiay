using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanGiay.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderCode { get; set; }
        public int ShoeId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        [ForeignKey("ShoeId")]
        public Shoe? shoe { get; set; }

    }
}
