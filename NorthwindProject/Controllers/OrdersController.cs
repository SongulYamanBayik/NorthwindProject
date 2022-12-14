using Microsoft.AspNetCore.Mvc;
using NorthwindProject.Models;
using NorthwindProject.Models.DTO;

namespace NorthwindProject.Controllers
{
    public class OrdersController : Controller
    {
        NORTHWNDContext context = new NORTHWNDContext();
        public IActionResult Index()
        {
            var value = from o in context.Orders
                        join c in context.Customers on o.CustomerId equals c.CustomerId
                        join od in context.OrderDetails on o.OrderId equals od.OrderId
                        join e in context.Employees on o.EmployeeId equals e.EmployeeId
                        group od by new {o.OrderId,o.OrderDate,c.ContactName, e.FirstName, e.LastName} into g
                        select new OrdersViewModel
                        {
                            OrderID = g.Key.OrderId,
                            OrderDate = g.Key.OrderDate,
                            ContactName = g.Key.ContactName,
                            EmployeeName = g.Key.FirstName + " " + g.Key.LastName,
                            TotalPrice = g.Sum(x=>x.UnitPrice*x.Quantity)

                        };

            List<OrdersViewModel> returnValue = new List<OrdersViewModel>();
            returnValue = value.ToList();
  

            return View(returnValue);
        }
        public IActionResult OrderDetail(int id)
        {
            var value = from od in context.OrderDetails
                        where od.OrderId==id
                        join p in context.Products on od.ProductId equals p.ProductId
                        select new OrderDetailViewModel
                        {
                            ProductName = p.ProductName,
                            Quantity = od.Quantity,
                            Price = od.UnitPrice - Convert.ToDecimal(od.Discount)
                        };
            List<OrderDetailViewModel> returnDetail = new List<OrderDetailViewModel>();
            returnDetail = value.ToList();
            return View(returnDetail);
        }
        [HttpGet]
        public IActionResult AddShipper()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddShipper(Shipper shipper)
        {
            context.Shippers.Add(shipper);
            context.SaveChanges();
            return RedirectToAction("ListShipper");
        }
        public IActionResult ListShipper()
        {
            var value = context.Shippers.ToList();
            return View(value);
        }
    }
}
