using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using Hospital_Managment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Hospital_Managment.Areas.Costumer.Controllers { 
    [Area("Costumer")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderVM OrderVM { get; set; }
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int? orderId)
        {
            var orderHeader = new OrderHeader();
            OrderVM = new OrderVM()
            {
                OrderHeader = _context.OrderHeader.Include(u => u.ApplicationUser).FirstOrDefault(u => u.Id == orderId),
                OrderDetails = _context.OrderDetail.Where(u => u.OrderId == orderId).Include(u => u.Product).ToList(),
            };
            return View(OrderVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderDetail()
        {
            var orderFromDB = _context.OrderHeader.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderFromDB.Name = OrderVM.OrderHeader.Name;
            orderFromDB.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderFromDB.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderFromDB.City = OrderVM.OrderHeader.City;
            orderFromDB.State = OrderVM.OrderHeader.State;
            orderFromDB.PostalCode = OrderVM.OrderHeader.PostalCode;
            if (OrderVM.OrderHeader.Carrier != null)
            {
                orderFromDB.Carrier = OrderVM.OrderHeader.Carrier;

            }
            if (OrderVM.OrderHeader.TrackingNumber != null)
            {
                orderFromDB.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;

            }
            _context.OrderHeader.Update(orderFromDB);
            _context.SaveChanges();



            return RedirectToAction("Details", "Order", new { orderId = orderFromDB.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProcessing()
        {
            var orderFromDB = _context.OrderHeader.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderFromDB.OrderStatus = RolesStrings.StatusInProcess;

            _context.OrderHeader.Update(orderFromDB);
            _context.SaveChanges();



            return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShipOrder()
        {
            var orderHeader = _context.OrderHeader.FirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeader.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier = OrderVM.OrderHeader.Carrier;
            orderHeader.OrderStatus = RolesStrings.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;
            if (orderHeader.PaymentStatus == RolesStrings.PaymentStatusDelayedPayment)
            {
                orderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
            }
            _context.OrderHeader.Update(orderHeader);
            _context.SaveChanges();
            TempData["Success"] = "Order Shipped Successfully.";
            return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });





        }
        #region API CAllS
        [HttpGet]
        public IActionResult GetAll( string status)
        {
            IEnumerable<OrderHeader> orderHeaders;
            if (User.IsInRole(RolesStrings.Role_User_Admin))
            {
                orderHeaders = _context.OrderHeader.Include(u => u.ApplicationUser).ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _context.OrderHeader.Where(u=>u.ApplicationUserId == claim.Value).Include(u=>u.ApplicationUser).ToList();
            }
            

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == RolesStrings.StatusPending);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == RolesStrings.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == RolesStrings.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == RolesStrings.StatusApproved);
                    break;
                default:
                   
                    break;
            }
            return Json(new { data = orderHeaders });

        }

        #endregion
    }
}
