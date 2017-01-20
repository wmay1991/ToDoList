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
    public class ToDoListTest
    {
       // [Setup]
        [Test]
        public void AddItems()
        {
            var mdl = new ToDoListItem();
            mdl.id = Guid.NewGuid();
            mdl.description = "Testing WM";
            mdl.isCompleted = false;
            var vm = new ToDoListViewModel(mdl);
            var controller = new ToDoListItemController();
            controller.AddItem(vm);
            var db = new ToDoModelContext();
            var result = db.toDoList.Find(vm.id);
            Assert.AreEqual("Testing WM",result.description);
            Assert.AreEqual(false, result.isCompleted);
        }

        [Test]
        public void AddItemsWithoutDescription()
        {
            var mdl = new ToDoListItem
            {
                id = Guid.NewGuid(),
                description = null,
                isCompleted = false
            };

            var vm = new ToDoListViewModel(mdl);
            var mockItem = new Mock<DbSet<ToDoListItem>>();

            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new ToDoListItemController(mockContext.Object);
            controller.AddItem(vm);
            Assert.AreEqual(mockContext.Object.toDoList.Where(x => x.description == null).Count(), 0);
        }

        [Test]
        public void EditItem()
        {

            var db = new ToDoModelContext();
            var existingItem = db.toDoList.Where(m => m.description.Contains("Update Item")).First();
            var updatedItem = new ToDoListItem
            {
                id = existingItem.id,
                description = "Update item 123",
                isCompleted = false
            };


            var vm1 = new ToDoListViewModel(updatedItem);
            var controller = new ToDoListItemController();
            controller.EditItem(vm1);

            var db1 = new ToDoModelContext();
            var result = db1.toDoList.Find(vm1.id);

            Assert.AreEqual("Update item 123", result.description);
            Assert.AreEqual(false, result.isCompleted);

        }

        [Test]
        public void EditItemsWithoutDescription()
        {
            var itemId = Guid.NewGuid();
            var existingItem = new ToDoListItem
            {
                id = itemId,
                description = "Update item",
                isCompleted = false

            };

            var updatedItem = new ToDoListItem
            {
                id = itemId,
                description =  null,
                isCompleted = false
            };

            var vm = new ToDoListViewModel(updatedItem);

            // update item
            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.Setup(x => x.Find(itemId)).Returns(existingItem);


            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new ToDoListItemController(mockContext.Object);
            controller.EditItem(vm);

            Assert.AreEqual(mockContext.Object.toDoList.Where(m => m.description == "Update item").Count(), 1);
        }


        [Test]
        public void ViewEditItem()
        {
            var itemId = Guid.NewGuid();
            var existingBlog = new ToDoListItem
            {
                id = itemId,
                description = "Happy happy happy",
                isCompleted = true
            };


            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.Setup(x => x.Find(itemId)).Returns(existingBlog);

            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new ToDoListItemController(mockContext.Object);

            var result = controller.EditItem(itemId) as ViewResult;

            Assert.IsAssignableFrom(typeof(ViewResult), result);
        }

      
        // Moq tests
        [Test]
        public void AddItemsMoq()
        {
            var mdl = new ToDoListItem {
                id = Guid.NewGuid(),
                description = "Testing WM",
                isCompleted = false
            };

            var vm = new ToDoListViewModel(mdl);
            var mockItem = new Mock<DbSet<ToDoListItem>>();

            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

           var controller = new ToDoListItemController(mockContext.Object);
           controller.AddItem(vm);
            mockItem.Verify(m => m.Add(It.IsAny<ToDoListItem>()), Times.Once());

        }


        [Test]
        public void EditItemMoq()
        {
            // create existing item
            var itemId = Guid.NewGuid();
            var existingItem = new ToDoListItem
            {
                id = itemId,
                description = "Update item",
                isCompleted = false

            };

            var updatedItem = new ToDoListItem
            {
                id = itemId,
                description = "Update item 123",
                isCompleted = false
            };

            var vm = new ToDoListViewModel(updatedItem);

            // update item
            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.Setup(x => x.Find(itemId)).Returns(existingItem);


            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new ToDoListItemController(mockContext.Object);
            controller.EditItem(vm);

            mockContext.Verify(x => x.SaveChanges());

        }

        [Test]
        public void EditItemMoqNoId()
        {
            // create existing item
            var itemId = 0;
            var existingItem = new ToDoListItem
            {
                description = "Update item",
                isCompleted = false

            };

            var updatedItem = new ToDoListItem
            {
                description = "Update item 123",
                isCompleted = false
            };

            var vm = new ToDoListViewModel(updatedItem);

            // update item
            var mockItem = new Mock<DbSet<ToDoListItem>>();
            mockItem.Setup(x => x.Find(itemId)).Returns(existingItem);


            var mockContext = new Mock<ToDoModelContext>();
            mockContext.Setup(x => x.toDoList).Returns(mockItem.Object);

            var controller = new ToDoListItemController(mockContext.Object);
            controller.EditItem(vm);

            mockContext.Verify(x => x.SaveChanges());

        }

    }
}
