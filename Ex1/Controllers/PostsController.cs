using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ex1.Domain;
using Ex1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ex1.Controllers
{
    [Route("[controller]")]
    public class PostsController : Controller
    {
        DB db = new DB();
        [Route("New")]
        public IActionResult New()
        {
            ViewBag.Authors = db.GetNameAuthors();
            return View(new PostCreate());
        }

        [Route("New")]
        [HttpPost]
        public IActionResult New(PostCreate postCreate)
        {
            if (ModelState.IsValid)
            {
                db.InsertPost(postCreate);
                return RedirectToAction("Index", "Home");
            }
            return View(postCreate);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            int count = db.GetMaxIdPosts();
            if (id.Trim() == "" || Convert.ToInt32(id) > count)
                return BadRequest();
            else
            {
                ViewBag.Name = db.GetNamePostById(Convert.ToInt32(id));
                ViewBag.AllTags = db.GetNameTags();
                ViewBag.Tags = db.GetTagsPostById(id);
                ViewBag.Id = id;
                return View(new AddTag());
            }
        }

        [HttpPost]
        [Route("AddTag")]
        public IActionResult AddTag(AddTag addTag)
        {
            if (ModelState.IsValid)
            {
                db.AddTagToPost(addTag.Name, addTag.Id);
                return RedirectToAction(addTag.Id, "Posts");
            }
            return RedirectToAction(addTag.Id, "Posts");
        }
    }
}
