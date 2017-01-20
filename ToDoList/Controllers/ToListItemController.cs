using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Data;
using ToDoList.Domain;
using ToDoList.Models;


namespace ToDoList.Controllers
{
    public class ToDoListItemController : Controller
    {
        private ToDoModelContext _db = new ToDoModelContext();

        //for mocking and testing purposes
        public ToDoListItemController()
        {
            _db = new ToDoModelContext();
        }

        public ToDoListItemController(ToDoModelContext db)
        {
            _db = db;
        }

        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(ToDoListViewModel toDoViewModel)
        {
            if (ModelState.IsValid)
            {
                ToDoListItem toDoModel = new ToDoListItem();
                toDoViewModel.id = Guid.NewGuid();
                var model = new ToDoListViewModel(toDoModel, toDoViewModel);

                // Add to db
                _db.toDoList.Add(toDoModel);
                _db.SaveChanges();

                // refresh to main page
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult EditItem(Guid itemId)
        {
            var model = _db.toDoList.Find(itemId);
            var viewModel = new ToDoListViewModel(model);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditItem(ToDoListViewModel toDoViewModel)
        {
            // find the id of the item
          if (ModelState.IsValid)
            {
                var itemId = toDoViewModel.id;
                var itemToUpdate = _db.toDoList.Find(toDoViewModel.id);
                var model = new ToDoListViewModel(itemToUpdate, toDoViewModel);

                //Update db
                _db.Entry(itemToUpdate).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

           return View();
        }
    }
}