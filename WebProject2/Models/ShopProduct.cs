using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebProject2.Models
{
    public class ShopProduct
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        public string? Category { get; set; }
        /*[EnumDataType(typeof(ProductCategory))]
        public ProductCategory Category { get; set; }*/
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime AuctionStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime AuctionEndDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string? SellerUserId { get; set; }

        // Define a list of available categories 
        
       /* public enum ProductCategory
        {
           Laptops,
           Smartphones,
           Cameras,
           iPad,
           Headsets

        }*/
        [NotMapped]
        public int RemainingDays
        {
            get
            {
                TimeSpan remainingTime = AuctionEndDate - DateTime.Today;
                return remainingTime.Days;
            }
        }
    }
}
