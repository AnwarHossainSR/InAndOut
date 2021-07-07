using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> objectList = _db.Items;
            return View(objectList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item obj)
        {
           if(ModelState.IsValid)
            {
                _db.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Items.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Item obj)
        {
            if(ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int ? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Items.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Items.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
