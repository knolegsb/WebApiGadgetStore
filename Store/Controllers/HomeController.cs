using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // Renders Order details as soon as an order has been submitted
        public ActionResult ViewOrder(int id)
        {
            using (var context = new StoreDbContext())
            {
                var order = context.Orders.Find(id);
                var gadgetOrders = context.GadgetOrders.Where(g => g.OrderId == id);

                foreach(GadgetOrder gadgetOrder in gadgetOrders)
                {
                    context.Entry(gadgetOrder).Reference(g => g.Gadget).Load();
                    order.Gadgets.Add(gadgetOrder.Gadget);
                }

                return View(order);
            }
        }
    }
}