using Core.Entities;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductService _productService;
        [BindProperty(SupportsGet = true)] public int Id { get; set; }

        public Product MyProduct { get; set; }

        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var product = await _productService.GetProductById(Id);

            MyProduct = product ??= new Product()
            {
                Id = 0,
                Title = string.Empty,
                Description = string.Empty,
                Price = 0,
                DiscountPercentage = 0,
                Rating = 0,
                Stock = 0,
                Brand = string.Empty,
                Category = string.Empty,
                Thumbnail = string.Empty,
                Images = new List<string>()
            };

            return Page();
        }
    }
}
