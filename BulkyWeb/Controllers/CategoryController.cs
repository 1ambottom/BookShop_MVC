using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            //if (category.Name == category.displayOrder.ToString())
            //{
            //    ModelState.AddModelError("name","The Display Order can't match the Name.");
            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var categories = _db.Categories.FirstOrDefault(x => x.Id == Id); ;
            return View(categories);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["Success"] = "Edited successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Delete(int? Id)
        {
            Category? category = _db.Categories.Find(Id);
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? category = _db.Categories.Find(Id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
