using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;

namespace Ecomerce_test1.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ICompositeViewEngine _viewEngine;
        public CardController(ApplicationDbContext _applicationDbContext, ICompositeViewEngine viewEngine)

        {
            this.applicationDbContext = _applicationDbContext;
            _viewEngine = viewEngine;

        }
        public IActionResult AddToCard(int productId, string color, int quantity, string userId)
        {
            ShoppingSession oldshoppingSession = applicationDbContext.ShoppingSessions.Where(s => s.UserId == userId).FirstOrDefault();
            if (oldshoppingSession != null)
            {
                CartItem cartItemForProduct = applicationDbContext.CartItems.Where(s => s.SessionId == oldshoppingSession.Id)
                                      .Where(s => s.ProductId == productId).FirstOrDefault();

                if (cartItemForProduct != null)
                {
                    cartItemForProduct.Quantity = quantity;
                    applicationDbContext.SaveChanges();
                    HttpContext.Session.SetInt32("shoppingSession", oldshoppingSession.Id);
                }

                else
                {
                    CartItem newcartItem = new CartItem();
                    newcartItem.ProductId = productId;
                    newcartItem.color = color;
                    newcartItem.Quantity = quantity;
                    newcartItem.SessionId = applicationDbContext.ShoppingSessions.Where(s => s.UserId == userId).FirstOrDefault().Id;

                    applicationDbContext.CartItems.Add(newcartItem);
                    applicationDbContext.SaveChanges();
                    HttpContext.Session.SetInt32("shoppingSession", oldshoppingSession.Id);
                }
                getInfo();
               
                return Json(HttpContext.Session.GetInt32("allProductQuantity")); 
            }
            else
            {

                ShoppingSession shoppingSession = new ShoppingSession();
                shoppingSession.UserId = userId;
                applicationDbContext.ShoppingSessions.Add(shoppingSession);
                applicationDbContext.SaveChanges();

                CartItem cartItem = new CartItem();
                cartItem.ProductId = productId;
                cartItem.color = color;
                cartItem.Quantity = quantity;
                int sessionId = applicationDbContext.ShoppingSessions.Where(s => s.UserId == userId).FirstOrDefault().Id;
                cartItem.SessionId = sessionId;

                applicationDbContext.CartItems.Add(cartItem);
                applicationDbContext.SaveChanges();

                HttpContext.Session.SetInt32("shoppingSession", sessionId);

                getInfo();

                ViewBag.number = HttpContext.Session.GetInt32("allProductQuantity");
                 return Json(HttpContext.Session.GetInt32("allProductQuantity"));
               // return PartialView("_Layout", Json(HttpContext.Session.GetInt32("allProductQuantity")));
            }
        }
        public IActionResult goToCard()
        {
            int? shoppingSessionId = HttpContext.Session.GetInt32("shoppingSession");
            if (shoppingSessionId == null || shoppingSessionId==0) {

                ViewData["total"] = 0;
                ViewData["allProductQuantity"] = 0;
                ViewData["Products"] = null;

                return View("Index" );

            }
            else { 
            Dictionary<string, int> data = getInfo();

            ViewData["total"] = data["total"];
            ViewData["allProductQuantity"] = data["allProductQuantity"];
            ViewData["Products"] = GetProductsForSession();
            return View("Index", applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId).ToList() );
            }
        }
        //public IActionResult updateOnCard(int idproduct, int quantity)
        //{
        //    int? shoppingSessionId = HttpContext.Session.GetInt32("shoppingSession");
        //    ShoppingSession oldshoppingSession = applicationDbContext.ShoppingSessions.Where(s => s.Id == shoppingSessionId).FirstOrDefault();
        //    List<CartItem> cartItems = applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId).ToList();
        //    CartItem cart = cartItems.Where(s => s.ProductId == idproduct).FirstOrDefault();

        //    if (quantity == 0 )
        //    {
        //        applicationDbContext.CartItems.Remove(cart);
        //        applicationDbContext.SaveChanges();
        //    }
        //    else
        //    {
        //        cart.Quantity = quantity;
        //    }

        //    applicationDbContext.SaveChanges();
        //    Dictionary<string, int> data = getInfo();
        //    ViewData["total"] = data["total"];
        //    ViewData["allProductQuantity"] = data["allProductQuantity"];

        //    return View();
        //}
        
        private Dictionary<string,int> getInfo() {
            int? shoppingSessionId = HttpContext.Session.GetInt32("shoppingSession");
            List<CartItem> cartItems = applicationDbContext.CartItems.Include(a=>a.Product).Where(s => s.SessionId == shoppingSessionId).ToList();
            ShoppingSession oldshoppingSession = applicationDbContext.ShoppingSessions.Where(s => s.Id == shoppingSessionId).FirstOrDefault();
            int totalPrice = 0;
            int allProductQuantity = 0;
            for (int i=0;i<cartItems.Count; i++)
            {
                CartItem cartItem = cartItems[i];
                int price = applicationDbContext.Products.Where(s => s.Id == cartItem.ProductId).FirstOrDefault().Price;
                totalPrice += price*cartItem.Quantity;
                allProductQuantity += cartItem.Quantity;
            }
            oldshoppingSession.Total = totalPrice;
            applicationDbContext.SaveChanges();
            Dictionary <string, int> info = new Dictionary<string, int>();
            info["total"] = totalPrice;
            info["allProductQuantity"] = allProductQuantity;

            ViewData["total"] = info["total"];
            ViewData["allProductQuantity"] = info["allProductQuantity"];

            HttpContext.Session.SetInt32("allProductQuantity", allProductQuantity);

            return info;


        }
        public IActionResult deletOnCard(int id)
        {
            int? shoppingSessionId = HttpContext.Session.GetInt32("shoppingSession");
            CartItem cartItem = applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId&&s.Id==id).FirstOrDefault();
            applicationDbContext.Remove(cartItem);
            applicationDbContext.SaveChanges();
            getInfo();

            ViewBag.number = HttpContext.Session.GetInt32("allProductQuantity");

            #region send partial view and session as json object 
            // return Json(HttpContext.Session.GetInt32("allProductQuantity"));
            //return Json( PartialView("_CartItem", applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId).ToList()) );
            PartialViewResult partialViewResult = 
                PartialView("_CartItem", applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId).ToList());
            string viewContent = ConvertViewToString(this.ControllerContext, partialViewResult, _viewEngine);

            #endregion

            return Json(new { message = HttpContext.Session.GetInt32("allProductQuantity"), viewP = viewContent });
        }

        private List<Product> GetProductsForSession() {
            int? shoppingSessionId = HttpContext.Session.GetInt32("shoppingSession");
            List<Product> products = new List<Product>();
            foreach (CartItem item in applicationDbContext.CartItems.Where(s => s.SessionId == shoppingSessionId).ToList())
            {
                products.Add(item.Product);
            }
            return products;
        }

        #region Convert View To String
        private string ConvertViewToString(ControllerContext controllerContext, PartialViewResult pvr, ICompositeViewEngine _viewEngine)
        {
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = _viewEngine.FindView(controllerContext, pvr.ViewName, false);
                ViewContext viewContext = new ViewContext
                    (controllerContext, vResult.View, pvr.ViewData, pvr.TempData, writer, new HtmlHelperOptions());

                vResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}
