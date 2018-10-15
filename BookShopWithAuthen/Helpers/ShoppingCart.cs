using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWithAuthentication.Helpers
{
    public class ShoppingCart
    {
        public string UserID { get; set; }
        public Dictionary<int, int> Items { get; private set; }

        public void addItemToCart(int productID)
        {
            if (this.Items == null)
            {
                this.Items = new Dictionary<int, int>();
            }
            int quantity = 1;
            if (this.Items.ContainsKey(productID))
            {
                quantity = this.Items[productID] + 1;
            }
            this.Items.Add(productID, quantity);
        }

        public void removeItemFromCart(int productID)
        {
            if (this.Items == null)
            {
                return;
            }
            if (this.Items.ContainsKey(productID))
            {
                this.Items.Remove(productID);
                if (this.Items.Count == 0)
                {
                    this.Items = null;
                }
            }
        }
    }
}