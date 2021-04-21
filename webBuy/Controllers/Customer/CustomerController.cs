using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBuy.Models;
using webBuy.Models.Home;
using webBuy.Repositories;

namespace webBuy.Controllers.Customer
{
    public class CustomerController : Controller
    {
        ProductRepository pr = new ProductRepository();
        // GET: Customer
        public ActionResult Index()
        {
            return View(pr.GetAll());
        }
        [HttpPost]
        public ActionResult Index(string search)
        {
            return View(pr.GetAll().Where(x => x.name==search).ToList());
        }
        public ActionResult Details(int id)
        {
            return View(pr.Get(id));
        }
       public ActionResult Product()
        {
            var list = pr.GetAll().OrderByDescending(date => date.unitPrice).Take(3).ToList();
            return View(list);
        }

        public ActionResult Electronic()
        {
            var list = pr.GetAll().Where(x => x.categoryId==1).ToList();
            return View(list);
        }
        public ActionResult Cloths()
        {
            var list = pr.GetAll().Where(x => x.categoryId == 2).ToList();
            return View(list);
        }
        public ActionResult Search(string search)
        {
            return View(pr.GetAll().Where(x => x.name.Contains(search) || search == null).ToList());
        }
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = pr.Get(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = pr.Get(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.productId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.productId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.productId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }




        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = pr.Get(productId);
                foreach (var item in cart)
                {
                    if (item.Product.productId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
    }
}