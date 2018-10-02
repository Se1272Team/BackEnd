﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEndBookShop.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
        }
        
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("Publisher")]
        public int PulisherID { get; set; }
        public virtual Publisher Publisher { get; set; }

        public int NumOfPages { get; set; }
        public string Intro { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}