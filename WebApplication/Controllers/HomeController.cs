using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.contacts.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            Contact contact = new Contact();
            return PartialView("CreateContact", contact);
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            db.contacts.Add(contact);
            db.SaveChanges();
            return PartialView("CreateContact", contact);
        }

        public IActionResult Edit(int? id)
        {
            var contact = db.contacts.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("EditContact", contact);
        }

        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            db.contacts.Update(contact);
            db.SaveChanges();
            return PartialView("EditContact", contact);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var contact = db.contacts.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("DeleteContact", contact);
        }
        [HttpPost]
        public IActionResult Delete(Contact cont)
        {
            var contact = db.contacts.Where(x => x.Id == cont.Id).FirstOrDefault();
            db.contacts.Remove(contact);
            db.SaveChanges();
            return PartialView("DeleteContact", contact);
        }
    }
}
