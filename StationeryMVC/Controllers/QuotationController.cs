using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationeryMVC.Data;
using StationeryMVC.Models;
using System.Linq;
using Rotativa.AspNetCore;

namespace StationeryMVC.Controllers
{
    public class QuotationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Create quotation
        public IActionResult Create()
        {
            ViewBag.Items = _context.StationeryItems.ToList();
            return View();
        }

        // POST: Save quotation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string customerName, int[] itemIds, int[] quantities)
        {
            if (string.IsNullOrEmpty(customerName) || itemIds.Length == 0)
            {
                ModelState.AddModelError("", "Please provide customer name and select at least one item.");
                ViewBag.Items = _context.StationeryItems.ToList();
                return View();
            }

            var quotation = new Quotation
            {
                CustomerName = customerName,
                Date = DateTime.Now
            };

            decimal total = 0;

            for (int i = 0; i < itemIds.Length; i++)
            {
                var item = _context.StationeryItems.Find(itemIds[i]);
                if (item != null)
                {
                    var qItem = new QuotationItem
                    {
                        StationeryItemId = item.Id,
                        Quantity = quantities[i],
                        Price = item.Price * quantities[i]
                    };
                    quotation.Items.Add(qItem);
                    total += qItem.Price;
                }
            }

            quotation.TotalAmount = total;
            _context.Quotations.Add(quotation);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = quotation.Id });
        }

        // GET: Quotation details
        public IActionResult Details(int id)
        {
            var quotation = _context.Quotations
                .Include(q => q.Items)
                .ThenInclude(i => i.StationeryItem)
                .FirstOrDefault(q => q.Id == id);

            if (quotation == null)
                return NotFound();

            return View(quotation);
        }

        // GET: List all quotations
        public IActionResult Index()
        {
            var quotations = _context.Quotations
                .Include(q => q.Items)
                .ThenInclude(i => i.StationeryItem)
                .ToList();
            return View(quotations);
        }
        

public IActionResult GeneratePdf(int id)
    {
        var quotation = _context.Quotations
            .Include(q => q.Items)
            .ThenInclude(i => i.StationeryItem)
            .FirstOrDefault(q => q.Id == id);

        if (quotation == null)
            return NotFound();

        // Use the Details view to render PDF
        return new ViewAsPdf("Details", quotation)
        {
            FileName = $"Quotation_{quotation.Id}.pdf",
            PageSize = Rotativa.AspNetCore.Options.Size.A4,
            PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 10, 10, 10)
        };
    }

}
}
