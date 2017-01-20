using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Domain;
using ToDoList.Models;
using ToDoList.Data;
using System.Data.Entity;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoModelContext _db = new ToDoModelContext();

        //for mocking and testing purposes
        public HomeController()
        {
            _db = new ToDoModelContext();
        }

        public HomeController(ToDoModelContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var item_list = _db.toDoList.ToList();
            return View(item_list);
        }
        

        public ActionResult DeleteItem (Guid itemId)
        {
            // find the id of the item
            var itemToDelete = _db.toDoList.Find(itemId);

            //Update db
            _db.toDoList.Remove(itemToDelete);
            _db.Entry(itemToDelete).State = EntityState.Deleted;
            _db.SaveChanges();



            return RedirectToAction("Index", "Home");
        }

    }
}