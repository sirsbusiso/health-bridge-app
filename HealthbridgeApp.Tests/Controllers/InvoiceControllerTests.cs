using HealthbridgeApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace HealthbridgeApp.Tests.Controllers
{
    [TestClass]
    public class InvoiceControllerTests
    {
        [TestMethod]
        public void Index_Action_Should_Render_A_View_Called_Index()
        {
            // Arrange
            var controller = new InvoiceController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
