using Microsoft.AspNetCore.Mvc;
using Resturant_E_Commerce.Data;
using Resturant_E_Commerce.Models;
using Resturant_E_Commerce.Models.IRepositories;
namespace Resturant_E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly Repository<Product> ProductRepo;
        public ProductController(ApplicationDbContext context)
        {
            ProductRepo = new Repository<Product>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await ProductRepo.GetAllAsync());
        }

    }
}
