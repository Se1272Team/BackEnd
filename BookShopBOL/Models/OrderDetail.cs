using BookShopBOL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookShopBOL.Models
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Book")]
        public int BookID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}