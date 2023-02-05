using Hospital_Managment.Data;
using Hospital_Managment.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hospital_Managment.ViewComponents
{
    public class ShoppingCartViewComponent :ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartViewComponent(ApplicationDbContext context)
        {
                  _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
           
            if (claim!= null)
            {
                if (HttpContext.Session.GetInt32(RolesStrings.SessionCart) != null)
                {
                    return View(HttpContext.Session.GetInt32(RolesStrings.SessionCart));
                }
                else
                {
                    HttpContext.Session.SetInt32(RolesStrings.SessionCart, _context.ShoppingCarts.Where(u => u.UserId == claim.Value).ToList().Count);
                    return View(HttpContext.Session.GetInt32(RolesStrings.SessionCart));

                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
