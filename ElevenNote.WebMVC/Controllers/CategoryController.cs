using ElevenNote.Models;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var service = new CategoryService();
            var model = service.GetCategories();
            return View(model);
        }

        //GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new CategoryService();
            if (service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //GET: Category/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = new CategoryService();
            var update = service.GetCategoryById(id);

            var model = new CategoryEdit
            {
                CategoryId = update.CategoryId,
                CategoryName = update.CategoryName
            };
            return View(model);
        }

        //POST: Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new CategoryService();
            if (service.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Category Updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Category could not be updated");
            return View(model);
        }

        //GET: Category/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = new CategoryService();
            var model = service.GetCategoryById(id);

            return View(model);
        }

        //POST: Category/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            var service = new CategoryService();
            service.DeleteCategory(id);
            TempData["SaveResult"] = "Category Deleted";

            return RedirectToAction("Index");
        }
    }
}