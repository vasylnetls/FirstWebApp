using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Core.Enums;
using Core.ExtensionMethods;
using Core.Service;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication1.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;

        public IEnumerable<Product>? Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        public ProductsModel(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> OnGet()
        {
            Console.WriteLine(WeekDay.Tuesday.GetDisplayName());
            Products = await _productService.GetProducts(SearchString);

            return Page();
        }
    }
}