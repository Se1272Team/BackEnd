using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebWithAuthentication.Models
{
    public class CartDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public string UserID { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public int Quantity { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}