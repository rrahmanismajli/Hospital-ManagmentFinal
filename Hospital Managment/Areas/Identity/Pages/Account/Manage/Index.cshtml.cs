// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable
    
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Hospital_Managment.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
     

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment hostEnvironment, ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _context = context;
          
              
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public string Role { get; set; }
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Adress { get; set; }
            public string FullName { get; set; }
            public string ImageUrl { get; set; }
            public int orderNumber { get; set; }
            public int appointmentCount { get; set; }
            public int orderApproved { get; set; }
            public int orderPending { get; set; }
            public int orderCancelled { get; set; }
            public int orderProcesed { get; set; }
            public int orderShipped { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var thisUser = await _userManager.FindByIdAsync(user.Id);
            var role    =await _userManager.GetRolesAsync(thisUser);
            var email = user.Email;
            var fullAdress = user.StreetAdress + " " + user.City + " " + user.PostalCode;
            var fullName = user.Name;
            var orderList = _context.OrderHeader.Where(u => u.ApplicationUserId == thisUser.Id).Count();
            var appointmentList = _context.appointmentsList.Where(u => u.UserId == thisUser.Id).Count();
            var ordersbyStatusApproved = _context.OrderHeader.Where(u=>u.ApplicationUserId == thisUser.Id && u.OrderStatus==RolesStrings.StatusApproved).Count();
            var ordersbyStatusPending = _context.OrderHeader.Where(u=>u.ApplicationUserId == thisUser.Id && u.OrderStatus==RolesStrings.StatusPending).Count();
            var ordersbyStatusCancelled = _context.OrderHeader.Where(u=>u.ApplicationUserId == thisUser.Id && u.OrderStatus==RolesStrings.StatusCancelled).Count();
            var ordersbyStatusProcess = _context.OrderHeader.Where(u=>u.ApplicationUserId == thisUser.Id && u.OrderStatus==RolesStrings.StatusInProcess).Count();
            var ordersbyStatusShipped = _context.OrderHeader.Where(u=>u.ApplicationUserId == thisUser.Id && u.OrderStatus==RolesStrings.StatusShipped).Count();


            Username = userName;
            Role = role.First();

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Email = email,
                Adress = fullAdress,
                FullName = fullName,
                ImageUrl = user.ImageUrl,
                orderNumber=orderList,
                appointmentCount = appointmentList,
                orderApproved=ordersbyStatusApproved,
                orderCancelled=ordersbyStatusCancelled,
                orderPending=ordersbyStatusPending,
                orderProcesed =ordersbyStatusProcess,
                orderShipped=ordersbyStatusShipped,
               
           
            };
        }
     

        public async Task<IActionResult> OnGetAsync()
        {
          
            //{
            //    Value = d.DoctorId.ToString(),
            //    Text = d.FirstName + " " + d.LastName
            //}).ToList();
            //ViewData["DoctorId"] = doctorList;
            var user = await _userManager.GetUserAsync(User);
            var orderList = _context.OrderHeader.Where(u => u.ApplicationUserId == user.Id).Count();


            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {

                await LoadAsync(user);
                return Page();
            }
            var oldName = user.Name;
            if (oldName == Input.FullName || Input.FullName == null)
            {
                StatusMessage = "Your Name is same or empty";
                
            }
            else
            {
                user.Name = Input.FullName;
                 await _userManager.UpdateAsync(user);
               
                    
               

            }
            //var imageUrl = user.ImageUrl;

            string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\users");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                   
                    user.ImageUrl = @"\images\users\" + fileName + extension;
                     await _userManager.UpdateAsync(user);
                   
                

            }



            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            //await _signInManager.RefreshSignInAsync(user);
            //StatusMessage = "Your profile has been updated";
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();

        }
    }
}
