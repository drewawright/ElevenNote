﻿using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index(int? SelectedCategory)
        {
            var categories = new CategoryService().GetCategories().OrderBy(c => c.CategoryName).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "CategoryId", "CategoryName", SelectedCategory);
            int categoryId = SelectedCategory.GetValueOrDefault();

            var service = CreateNoteService();
            //var model = service.GetNotes();

            IQueryable<NoteListItem> model = service.GetNotes().AsQueryable()
                .Where(n => !SelectedCategory.HasValue || n.Category.CategoryId == categoryId)
                .OrderBy(x => x.NoteId)
                .Include(x => x.Category.CategoryId);
            return View(model);
        }

        //GET: Note/Create
        public ActionResult Create()
        {
            var service = new CategoryService();
            ViewBag.CategoryId = new SelectList(service.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        //POST: Note/Create
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Create(NoteCreate model)
        {
            var svc = new CategoryService();
            ViewBag.CategoryId = new SelectList(svc.GetCategories(), "CategoryId", "CategoryName");
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";
               return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");

            return View(model);
        }

        //GET: Notes/Detail/{id}
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //GET: Notes/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            var svc = new CategoryService();
            ViewBag.CategoryId = new SelectList(svc.GetCategories(), "CategoryId", "CategoryName", detail.Category);
            return View(model);
        }

        //POST: Notes/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            var svc = new CategoryService();
            ViewBag.CategoryId = new SelectList(svc.GetCategories(), "CategoryId", "CategoryName", model.Category);
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        //GET: Notes/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }

        //POST: Notes/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();
            service.DeleteNote(id);
            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}