using Microsoft.AspNetCore.Mvc;
using StationeryMVC.Models;

namespace StationeryMVC.Controllers
{
    public class StationeryController : Controller
    {
        private static List<StationeryItem> items = new()
        {
            new StationeryItem { Id = 1, Name = "Pen", Quantity = 10, Price = 5 },
            new StationeryItem { Id = 2, Name = "Notebook", Quantity = 5, Price = 25 }
        };

        public IActionResult Index()
        {
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StationeryItem item)
        {
            item.Id = items.Max(i => i.Id) + 1;
            items.Add(item);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(StationeryItem item)
        {
            var existing = items.First(i => i.Id == item.Id);
            existing.Name = item.Name;
            existing.Quantity = item.Quantity;
            existing.Price = item.Price;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var item = items.First(i => i.Id == id);
            items.Remove(item);
            return RedirectToAction(nameof(Index));
        }
    }
}
