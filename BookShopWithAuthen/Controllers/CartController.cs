using AutoMapper;
using BookShopWithAuthen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using WebWithAuthentication.Models;
using WebWithAuthentication.Service;
using WebWithAuthentication.ViewModel;

namespace BookShopWithAuthen.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private CartService _cartService;
        private BookService _bookService;
        private OrderService _orderService;
        private OrderDetailService _orderDetailService;
        private UserManager<ApplicationUser> _userManager;
        public CartController()
        {
            _orderDetailService = new OrderDetailService();
            _orderService = new OrderService();
            _cartService = new CartService();
            _bookService = new BookService();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Cart
        public ActionResult Index()
        {
            // get cart
            List<CartItemViewModel> listCartItems = _cartService.GetShoppingCart(User.Identity.GetUserId());
            ViewBag.totalMoney = _cartService.GetTotalMoney(User.Identity.GetUserId());
            ViewBag.listCartItems = listCartItems;

            // get user infor
            ApplicationUser user = _userManager.FindByName(User.Identity.Name);
            var shippingDetailViewModel = Mapper.Map<ShippingDetailViewModel>(user);
            return View(shippingDetailViewModel);
        }
        [HttpPost]
        public void Checkout(ShippingDetailViewModel shippingDetailViewModel)
        {
            // create order
            int totalMoney = _cartService.GetTotalMoney(User.Identity.GetUserId());
            Order order = new Order()
            {
                UserId = User.Identity.GetUserId(),
                Name = shippingDetailViewModel.Name,
                Address = shippingDetailViewModel.Address,
                PhoneNumber = shippingDetailViewModel.PhoneNumber,
                Email = shippingDetailViewModel.Email,
                Status = (int)StatusOrder.New,
                Amount = totalMoney,
                OrderDate = DateTime.Today
            };
            try
            {
                _orderService.Create(order);
            }
            catch (Exception ex)
            {
                Debug.Write("This also is a error:  " + ex.Message);
                Debug.Write("This is error: " + ex.InnerException);
            }

            // create order details
            List<CartItemViewModel> listCartItems = _cartService.GetShoppingCart(User.Identity.GetUserId());

            foreach (var item in listCartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderID = order.Id,
                    BookID = item.BookId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                _orderDetailService.Create(orderDetail);
            }

            // send mail to customer
            string subject = "Notification about your order";
            string message = "Your order is " + totalMoney + ".000 đ and will be sent soon, please visit our website to " +
                " view your order status";
            try
            {
                OtherServices.SendMail(shippingDetailViewModel.Email, subject, message);
            }
            catch (Exception ex)
            {
                Debug.Write("This is inner exeption: " + ex.InnerException);
            }
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
            int totalPrice = (int)_bookService.getByID(bookID).Price * quantity;
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