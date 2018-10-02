using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndBookShop.Models
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}