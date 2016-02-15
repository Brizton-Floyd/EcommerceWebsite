using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Order
    {
        public Order()
        {
            Catalog = new Catalog();
        }
        public int OrderId { get; set; }
        public string JobType { get; set; }
        public string Media { get; set; }
        public int CatalogNumber { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int CatalogId { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }

        public Catalog Catalog { get; set; }
        public virtual User User { get; set; }
    }
}