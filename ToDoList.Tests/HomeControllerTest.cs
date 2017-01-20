using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToDoList.Controllers;
using ToDoList.Models;
using Moq;
using ToDoList.Domain;
using System.Data.Entity;
using ToDoList.Data;

namespace ToDoList.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void ShowItems()
        {
            var mdl = new List<ToDoListItem>
            {
                 new ToDoListItem { id = Guid.NewGuid() , description = "Test1", isCompleted = true},
                new ToDoListItem {id = Guid.NewGuid() , description = "Test2", isCompleted = false},
                new ToDoListItem { id = Guid.NewGuid(), description = "Test3", isCompleted = false},
            }.AsQueryable();

            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.As<IQueryable<ToDoListItem>>().Setup(m => m.Provider).Returns(mdl.Provider);
            mockItem.As<IQueryable<ToDoListItem>>().Setup(m => m.Expression).Returns(mdl.Expression);
            mockItem.As<IQueryable<ToDoListItem>>().Setup(m => m.ElementType).Returns(mdl.ElementType);
            mockItem.As<IQueryable<ToDoListItem>>().Setup(m => m.GetEnumerator()).Returns(mdl.GetEnumerator());

            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(c => c.toDoList).Returns(mockItem.Object);

            var controller = new HomeController(mockContext.Object);
            controller.Index();

           // Assert.AreEqual(mockItem.Object.Count(), 3);
            //Assert.AreEqual(mockItem.Object.Where(m => m.description == "Test1").Count(), 1);
           // Assert.AreEqual(mockItem.Object.Where(m => m.isCompleted == false).Count(), 2);
            Assert.AreEqual(mockContext.Object.toDoList.Where(m => m.description == "Test1").Count(), 1);
            Assert.AreEqual(mockContext.Object.toDoList.Where(m => m.isCompleted == false).Count(), 2);
            Assert.AreEqual(mockContext.Object.toDoList.Count(), 3);
        }

        [Test]
        public void ShowItemsNull()
        {
            var showItems = new HomeController();
            var result = showItems.Index();
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteItem()
        {
            var db = new ToDoModelContext();
            var deletingItem = db.toDoList.Where(m => m.description.Contains("Update Item")).First();

            var controller = new HomeController();
            controller.DeleteItem(deletingItem.id);

            var db1 = new ToDoModelContext();
            var result = db1.toDoList.Find(deletingItem.id);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void DeleteItemMoq()
        {
            // create existing item
            var itemId = Guid.NewGuid();
            var deletingItem = new ToDoListItem
            {
                id = itemId,
                description = "Delete item",
                isCompleted = false

            };

            var vm = new ToDoListViewModel(deletingItem);

            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.Setup(x => x.Find(itemId)).Returns(deletingItem);


            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new HomeController(mockContext.Object);
            controller.DeleteItem(itemId);

            mockContext.Verify(x => x.SaveChanges());

        }

    }
}
