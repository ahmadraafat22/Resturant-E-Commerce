using Microsoft.AspNetCore.Mvc;
using Resturant_E_Commerce.Data;
using Resturant_E_Commerce.Models;
using Resturant_E_Commerce.Models.IRepositories;
using Resturant_E_Commerce.ViewModels;
using System.Threading.Tasks;
namespace Resturant_E_Commerce.Controllers
{
    public class IngredientController : Controller
    {
        private readonly Repository<Ingredient> IngredientRepo;
        public IngredientController(ApplicationDbContext context)
        {
            IngredientRepo = new Repository<Ingredient>(context);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await IngredientRepo.GetAllAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await IngredientRepo.GetByIdAsync(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                Ingredient instance = new Ingredient { Name = ingredient.Name, Description = ingredient.Description };
                await IngredientRepo.AddAsync(instance);
                await IngredientRepo.SaveAsync();
                return RedirectToAction("Index");
            }
            return View(ingredient);            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await IngredientRepo.GetByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingredient ingredient)
        {
            await IngredientRepo.DeleteAsync(ingredient.IngredientId);
            await IngredientRepo.SaveAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ingredientFromDb = await IngredientRepo.GetByIdAsync(id);
            if (ingredientFromDb == null)
                throw new Exception("this id not exist ");

            return View(ingredientFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingredient ingredient)
        {
            var ingredientFromDb=await IngredientRepo.GetByIdAsync(ingredient.IngredientId);
            if (ingredientFromDb == null)
                throw new Exception("this id not exist ");
            ingredientFromDb.Name = ingredient.Name;
            ingredientFromDb.Description = ingredient.Description;
            await IngredientRepo.UpdateAsync(ingredientFromDb);
            await IngredientRepo.SaveAsync();
            return RedirectToAction("Index");
        }

    }
}
