using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StationeryMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StationeryMVC.Controllers
{
    public class StationeryController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public StationeryController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // In-memory stationery items
        private static List<StationeryItem> items = new()
        {
            new StationeryItem { Id = 1, Name = "Pen", Category = "Writing", Quantity = 10, Price = 5, ImageUrl = "/images/default.png" },
            new StationeryItem { Id = 2, Name = "Notebook", Category = "Books", Quantity = 5, Price = 25, ImageUrl = "/images/default.png" },
            new StationeryItem { Id = 3, Name = "Pencil", Category = "Writing", Quantity = 10, Price = 2, ImageUrl = "/images/default.png" }
        };

        private static readonly List<string> categories = new()
        {
            "Writing", "Books", "Office", "Art", "Other"
        };

        // INDEX
        public IActionResult Index(string searchString, string category)
        {
            var filtered = items.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                filtered = filtered.Where(i => i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category))
                filtered = filtered.Where(i => i.Category == category);

            ViewBag.Categories = categories;
            return View(filtered.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            ViewBag.Categories = categories;
            return View();
        }

        // POST CREATE (IMAGE UPLOAD WORKS HERE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StationeryItem item, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categories;
                return View(item);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                imageFile.CopyTo(stream);

                item.ImageUrl = "/images/" + fileName;
            }
            else
            {
                item.ImageUrl = "/images/default.png";
            }

            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(item);

            return RedirectToAction(nameof(Index));
        }

        // GET EDIT
        public IActionResult Edit(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            ViewBag.Categories = categories;
            return View(item);
        }

        // POST EDIT (IMAGE UPDATE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StationeryItem item, IFormFile imageFile)
        {
            var existing = items.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null) return NotFound();

            existing.Name = item.Name;
            existing.Category = item.Category;
            existing.Quantity = item.Quantity;
            existing.Price = item.Price;

            if (imageFile != null && imageFile.Length > 0)
            {
                string folder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                imageFile.CopyTo(stream);

                existing.ImageUrl = "/images/" + fileName;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET DELETE
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
            if (item != null) items.Remove(item);

            return RedirectToAction(nameof(Index));
        }

        // DASHBOARD
        public IActionResult Dashboard()
        {
            ViewBag.TotalItems = items.Count;
            ViewBag.TotalStock = items.Sum(i => i.Quantity);
            ViewBag.CategoryCount = items.Select(i => i.Category).Distinct().Count();
            return View();
        }
    }
}
