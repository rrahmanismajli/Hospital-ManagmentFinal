using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public ContactUsController(ApplicationDbContext context,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender; 
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: ContactUs


        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactUs contactUs)
        {
           if (!string.IsNullOrEmpty(contactUs.Message))
            { 
                var htmlMessage = $"<p>{contactUs.Name} send this message to Clinic:</br><span style='color:red;'>{contactUs.Message}</span></p>" +
                    $"<blockquote>These are Details of Sender:" +
                    $"</br>Name:{contactUs.Name}</br>" +
                    $"Phone Number:{contactUs.PhoneNumber}</br>" +
                    $"Email:<a href='mailto:{contactUs.Email}'>{contactUs.Email}</a></blockquote>";
                await _emailSender.SendEmailAsync("infomessages7@gmail.com", $"New Message from {contactUs.Name} with <b>SUBJECT</b>{contactUs.Subject}", htmlMessage);
            }
            return RedirectToAction("Index");
        }

        // GET: ContactUs/Edit/5
     
    }
}
