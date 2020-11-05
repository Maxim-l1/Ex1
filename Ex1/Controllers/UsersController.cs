using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex1.Domain;
using Ex1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ex1.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        DB db = new DB();
        [Route("New")]
        public IActionResult New()
        {
            return View(new UserCreate());
        }

        [Route("New")]
        [HttpPost]
        public IActionResult New(UserCreate userCreate)
        {
            if (ModelState.IsValid)
            {
                db.InsertUser(userCreate);
                return RedirectToAction("Index", "Home");
            }
            return View(userCreate);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            int count = db.GetMaxIdUsers();
            if (id.Trim() == "" || Convert.ToInt32(id) > count)
                return BadRequest();
            else
            {
                ViewBag.Name = db.GetNameUserById(Convert.ToInt32(id));
                ViewBag.Id = id;
                return View(new AddInformation());
            }
        }

        [HttpPost]
        [Route("AddInf")]
        public IActionResult AddInf(AddInformation addInformation)
        {
            if (ModelState.IsValid)
            {
                db.AddInf(addInformation);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
