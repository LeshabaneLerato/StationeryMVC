using Microsoft.AspNetCore.Mvc;
using StationeryMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace StationeryMVC.Controllers
{
    public class StationeryController : Controller
    {
        // In-memory stationery items
        private static List<StationeryItem> items = new()
        {
            new StationeryItem { Id = 1, Name = "Pen", Category = "Writing", Quantity = 10, Price = 5 },
            new StationeryItem { Id = 2, Name = "Notebook", Category = "Books", Quantity = 5, Price = 25 },
            new StationeryItem { Id = 2, Name = "Pocket Files", Category = "Books", Quantity = 6, Price = 30 },
            new StationeryItem { Id = 2, Name = "Pencil", Category = "Writing", Quantity = 10, Price = 2 },
            new StationeryItem { Id = 2, Name = "Scissor", Category = "Other", Quantity = 4, Price = 15 },
            new StationeryItem { Id = 2, Name = "Chart", Category = "Art", Quantity = 10, Price = 100 },
            new StationeryItem { Id = 2, Name = "Pencil case", Category = "Other", Quantity = 2, Price = 50 },


        };

        // Categories
        private static readonly List<string> categories = new()
        {
            "Writing",
            "Books",
            "Office",
            "Art",
            "Other"
        };

        // INDEX - List with Search
        public IActionResult Index(string searchString, string category)
        {
            var filteredItems = items.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                filteredItems = filteredItems.Where(i => i.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category))
                filteredItems = filteredItems.Where(i => i.Category == category);

            ViewBag.Categories = categories;
            ViewBag.SearchString = searchString;
            ViewBag.SelectedCategory = category;

            return View(filteredItems.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            ViewBag.Categories = categories;
            return View();
        }

        // POST CREATE
        [HttpPost]
        public IActionResult Create(StationeryItem item)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categories;
                return View(item);
            }

            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);
            return RedirectToAction(nameof(Index));
        }

        // GET EDIT
        // GET: /Stationery/Edit/{id}
        public IActionResult Edit(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            ViewBag.Categories = categories; // important!
            return View(item);
        }

        // POST: /Stationery/Edit
        [HttpPost]
        public IActionResult Create(StationeryItem item, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categories;
                return View(item);
            }

            // Handle image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                item.ImageUrl = "/images/" + fileName;
            }

            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);

            return RedirectToAction(nameof(Index));
        }

        private readonly IWebHostEnvironment _env;
        public StationeryController(IWebHostEnvironment env)
        {
            _env = env;
        }



        // GET DELETE - confirmation
        public IActionResult Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            return View(item);
        }

        // POST DELETE
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
                items.Remove(item);

            return RedirectToAction(nameof(Index));
        }

        // DASHBOARD
        public IActionResult Dashboard()
        {
            ViewBag.TotalItems = items.Count;
            ViewBag.TotalStock = items.Sum(i => i.Quantity);
            ViewBag.CategoryCount = items.GroupBy(i => i.Category).Count();

            return View();
        }
    }
}
