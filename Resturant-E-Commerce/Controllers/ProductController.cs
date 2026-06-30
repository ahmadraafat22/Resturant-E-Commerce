using Microsoft.AspNetCore.Mvc;
using Resturant_E_Commerce.Data;
using Resturant_E_Commerce.Models;
using Resturant_E_Commerce.Models.IRepositories;
using System.Threading.Tasks;
namespace Resturant_E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly Repository<Product> ProductRepo;
        private readonly Repository<Ingredient> IngredientRepo;
        private readonly Repository<Category> CategoryRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            ProductRepo = new Repository<Product>(context);
            IngredientRepo = new Repository<Ingredient>(context);
            CategoryRepo = new Repository<Category>(context);
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await ProductRepo.GetAllAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            Product instance = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageFile = product.ImageFile

            };
            await ProductRepo.AddAsync(instance);
            await ProductRepo.SaveAsync();
            return View("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Ingredient =await IngredientRepo.GetAllAsync();
            ViewBag.Category =await CategoryRepo.GetAllAsync();
            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                ViewBag.Operation = "Edit";
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(Product product , int[] IngredientIds,int categoryId)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string FolderUplads = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(FolderUplads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = uniqueFileName;
                }
                if (product.ProductId == 0)
                {
                    ViewBag.Ingredients = await IngredientRepo.GetAllAsync();
                    ViewBag.Categories = await CategoryRepo.GetAllAsync();
                    product.CategoryId = categoryId;
                    product.ProductIngredients = new List<ProductIngredient>();
                    foreach(int i in IngredientIds)
                    {
                        product.ProductIngredients.Add(new ProductIngredient { IngredientId = i, ProductId = product.ProductId });
                    }

                    await ProductRepo.AddAsync(product);
                    await ProductRepo.SaveAsync();
                    return RedirectToAction("Index") ;
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(product);
            }
            
        }

    }
}
