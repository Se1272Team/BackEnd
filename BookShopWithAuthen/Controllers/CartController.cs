using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWithAuthentication.Service;
using WebWithAuthentication.ViewModel;

namespace BookShopWithAuthen.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private CartService _cartService;
        private BookService _bookService;
        public CartController()
        {
            _cartService = new CartService();
            _bookService = new BookService();
        }
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItemViewModel> listCartItems = _cartService.GetShoppingCart(User.Identity.GetUserId());
            ViewBag.totalMoney = _cartService.GetTotalMoney(User.Identity.GetUserId());
            return View(listCartItems);
        }

        [HttpPost]
        public void AddToCart(int bookID)
        {
            string userID = User.Identity.GetUserId();
            _cartService.AddItemToCart(userID, bookID);
        }


        [HttpPost]
        public JsonResult changeQuantity(int bookID, int quantity)
        {
            
            string userID = User.Identity.GetUserId();
            _cartService.EditQuantityFromCart(userID, bookID, quantity);
            int totalPrice =(int)_bookService.getByID(bookID).Price * quantity;
            int totalMoney = _cartService.GetTotalMoney(userID);
            return Json(new { totalPrice = totalPrice, totalMoney = totalMoney }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult deleteItem(int bookID)
        {
            string userID = User.Identity.GetUserId();
            _cartService.RemoveItemFromCart(userID, bookID);
            int totalMoney = _cartService.GetTotalMoney(userID);
            return Json(new { totalMoney = totalMoney }, JsonRequestBehavior.AllowGet);
        }
    }
}