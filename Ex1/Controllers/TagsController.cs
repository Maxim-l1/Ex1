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
    public class TagsController : Controller
    {
        DB db = new DB();
        [Route("New")]
        public IActionResult New()
        {
            return View(new TagCreate());
        }

        [Route("New")]
        [HttpPost]
        public IActionResult New(TagCreate tagCreate)
        {
            if (ModelState.IsValid)
            {
                db.InsertTag(tagCreate.Name);
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Name = tagCreate.Name;
            return View(tagCreate);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            int count = db.GetMaxIdTags();
            if (id.Trim() == "" || Convert.ToInt32(id) > count)
                return BadRequest();
            else
            {
                ViewBag.Name = db.GetNameTagById(Convert.ToInt32(id));
                ViewBag.AllPosts = db.GetNamePosts();
                ViewBag.Posts = db.GetPostsTagById(id);
                ViewBag.Id = id;
                return View(new AddPost());
            }
        }

        [HttpPost]
        [Route("AddPost")]
        public IActionResult AddPost(AddPost addPost)
        {
            if (ModelState.IsValid)
            {
                db.AddPostToTag(addPost.Name, addPost.Id);
                return RedirectToAction(addPost.Id, "Tags");
            }
            return RedirectToAction(addPost.Id, "Tags");
        }
    }
}
