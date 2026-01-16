using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StationeryMVC.Models
{
    public class Quotation
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        // ✅ This FIXES: 'Quotation' does not contain a definition for 'Date'
        public DateTime Date { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        // Navigation
        public ICollection<QuotationItem> Items { get; set; } = new List<QuotationItem>();
    }
}
