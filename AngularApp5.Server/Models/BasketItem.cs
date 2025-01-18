using System.ComponentModel.DataAnnotations;

namespace AngularApp5.Server.Models
{
    public class BasketItem : BaseModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Cena musi być większa niż 0")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być conajmniej 1")]
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}