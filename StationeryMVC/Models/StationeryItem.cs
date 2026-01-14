using System.ComponentModel.DataAnnotations;

namespace StationeryMVC.Models
{
    public class StationeryItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
