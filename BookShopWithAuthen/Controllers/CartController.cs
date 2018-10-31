using AutoMapper;
using BookShopWithAuthen.Helpers.EmailTemplate;
using BookShopWithAuthen.Models;
using BookShopWithAuthen.Service;
using BookShopWithAuthen.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

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
            if (TempData["errorMessage"] != null)
            {
                ViewBag.errorMessage = TempData["errorMessage"].ToString();
            }
            
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
        public ActionResult Checkout(ShippingDetailViewModel shippingDetailViewModel)
        {
            string userId = User.Identity.GetUserId();
            // Kiem tra tinh hop le cua gio hang
            bool flagValid = true;
            List<CartItemViewModel> listCartItems = _cartService.GetShoppingCart(User.Identity.GetUserId());
            foreach (var item in listCartItems)
            {
                int wareHouseQuantity = (int)_bookService.getByID(item.BookId).Quantity;
                if (item.Quantity > wareHouseQuantity)
                {
                    _cartService.EditQuantityFromCart(userId, item.BookId, wareHouseQuantity);
                    flagValid = false;
                }
            }
            if (flagValid == false)
            {
                TempData["errorMessage"] = "Một số sách bạn đặt có số lượng không đủ, chúng tôi đã cập nhật " +
                    " lại số lượng sách của vài sản phẩm trong giỏ hàng, mời bạn xem và đặt hàng lại";
                return RedirectToAction("Index");
            }
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
            foreach (var item in listCartItems)
            {
                int wareHouseQuantity = (int)_bookService.getByID(item.BookId).Quantity;
                _bookService.UpdateQuantityBook(item.BookId, wareHouseQuantity - item.Quantity);
                OrderDetail orderDetail = new OrderDetail()
                {
                    Order = order,
                    BookID = item.BookId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    BookName = item.BookName,
                    Image = item.Image
                };
                _orderDetailService.Create(orderDetail);

            }


            // update book quantity


            // send mail to customer
            string subject = "Notification about your order at DTBook";
            ICollection<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>();
            var orderDetails = order.OrderDetails;
            foreach (var item in orderDetails)
            {
                OrderDetailViewModel tmpOrDeViewModel = Mapper.Map<OrderDetailViewModel>(item);
                orderDetailViewModels.Add(tmpOrDeViewModel);
            }
            var model = new OrderConfirmEmailModel()
            {
                Order = order,
                OrderDetailViewModels = orderDetailViewModels
            };
            var path = Path.Combine(Server.MapPath("~/Helpers/EmailTemplate"), "OrderConfirmTemplate.cshtml");
            var templateSerivce = new TemplateService();
            try
            {
                string emailHtmlBody = templateSerivce.Parse(System.IO.File.ReadAllText(path), model, null, null);
                OtherServices.SendMail(shippingDetailViewModel.Email, subject, emailHtmlBody);
            }
            catch (Exception ex)
            {
                Debug.Write("This is inner exeption: " + ex.Message);
                Debug.Write("This is inner exeption: " + ex.InnerException);
            }
            return View();
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