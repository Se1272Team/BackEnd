using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;

namespace BookShopWithAuthen.Service
{
    public class CartService:BaseService<CartDetail>
    {
        public List<CartItemViewModel> GetShoppingCart(string UserID)
        {
            BookService bookService = new BookService();
            List<CartItemViewModel> listCartItems = new List<CartItemViewModel>();
            var allCartDetails = _repo.Get(cd => cd.UserID.Equals(UserID)).ToList();
            foreach (var item in allCartDetails)
            {
                Book book = bookService.getByID(item.BookID);
                CartItemViewModel cartItem = new CartItemViewModel()
                {
                    BookId = book.ID,
                    BookName = book.Name,
                    Image = book.Image,
                    Price = book.Price,
                    Quantity = item.Quantity,
                };
                listCartItems.Add(cartItem);
            }
            return listCartItems;
        }

        public int GetTotalMoney(string UserID)
        {
            int totalMoney = 0;
            List<CartItemViewModel> listCartItems = GetShoppingCart(UserID);
            foreach(var item in listCartItems)
            {
                totalMoney +=(int) (item.Price * item.Quantity);
            }
            return totalMoney;
        }

        public void AddItemToCart(string userID, int bookID, int quantity = 1)
        {
            CartDetail current = _repo.Get(cd => cd.BookID == bookID && cd.UserID == userID).SingleOrDefault();
            if (current == null)
            {
                CartDetail cartDetail = new CartDetail() {
                    UserID = userID,
                    BookID = bookID,
                    Quantity = quantity
                };
                _repo.Create(cartDetail);
                _repo.SaveChanges();
            }
            else
            {
                current.Quantity = current.Quantity + quantity;
                _repo.SaveChanges();
            }
        }

        public void RemoveItemFromCart(string userID, int bookID)
        {
            CartDetail current = _repo.Get(cd => cd.BookID == bookID && cd.UserID == userID).SingleOrDefault();
            if (current != null)
            {
                _repo.Delete(current);
                _repo.SaveChanges();
            }
        }



        public void EditQuantityFromCart(string userID, int bookID, int quantity)
        {
            CartDetail current = _repo.Get(cd => cd.BookID == bookID && cd.UserID == userID).SingleOrDefault();
            if (current != null)
            {
                current.Quantity = quantity;
                _repo.SaveChanges();
            }
        }
    }
}