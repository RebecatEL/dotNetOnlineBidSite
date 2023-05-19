using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject2.Models;

namespace WebProject2.ViewModels
{
    public class ShopProductCategoryViewModel
    {
        public List<ShopProduct>? ShopProducts { get; set; }
        public SelectList? Categories { get; set; }
        public string? ShopProductCategory { get; set; }
        public string? Search { get; set; }
    }
}
