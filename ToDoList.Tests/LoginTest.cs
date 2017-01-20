using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Core;
using Moq;
using NUnit.Framework;
using ToDoList.Controllers;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestFixture]
    public class LoginTest
    {
        [Test]
        public async void LoginSuccess()
        {
            var lgVm = new LoginViewModel();
            lgVm.Email = "wmay@example.com";
            lgVm.Password = "Test123!";
            lgVm.RememberMe = false;
            var lg = new AccountController();
            var result =  await lg.Login(lgVm, null);
            //Assert.AreEqual();
        }


        [Test]
        public void PasswordMissing()
        {
            var pwdm = new AccountController();
        }

        [Test]
        public void UserNameMissing()
        {
            var use_miss = new AccountController();

        }
    }
}
