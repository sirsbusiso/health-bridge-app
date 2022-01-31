using HealthbridgeApp.Interface;
using HealthbridgeApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HealthbridgeApp.Tests.Controllers.API
{
    [TestClass]
    public class InvoiceAPIControllerTests
    {
        [TestMethod]
        public void GetInvoices_Returns_Invoices_List_With_One_Invoice_Item()
        {
            // Arrange
            var invList = new List<InvoiceViewModel>
            {
                new InvoiceViewModel{ InvoiceDateTime = new DateTime(2022, 1, 2), InvoiceId = 1, InvoiceTotal = 200, PatientId = 1 }
            };

            var mock = new Mock<IInvoice>();
            mock.Setup(p => p.GetInvoices()).Returns(invList);

            // Act
            var result = mock.Object.GetInvoices();

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void UpdateInvoiceLine_Updates_Successfully()
        {
            // Arrange
            var inv = new UpdateInvoiceViewModel { Qty = 1, Code = "ABCD", Description = "Testing", LineTotal = 200, InvoiceId = 1, InvoiceLineId = 1 };

            var mock = new Mock<IInvoice>();
            mock.Setup(p => p.UpdateInvoiceLine(inv)).Returns("Invoice line updated successfully");

            // Act
            var result = mock.Object.UpdateInvoiceLine(inv);

            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }

        [TestMethod]
        public void Delete_Invoice_Line_Deletes_Successfully()
        {
            var id = 1;

            //Arrange
            var mock = new Mock<IInvoice>();
            mock.Setup(p => p.DeleteInvoiceLine(id)).Returns("Deleted successfully");


            // Act
            var result = mock.Object.DeleteInvoiceLine(id);


            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }

        [TestMethod]
        public void Create_Invoice_Creates_Successfully()
        {
            var invoice = new InvoiceViewModel { InvoiceDateTime = new DateTime(2022, 1, 2), PatientId = 1, InvoiceId = 1, InvoiceTotal = 566, CreateInvoiceLineViewModels = new List<CreateInvoiceLineViewModel> { new CreateInvoiceLineViewModel {
               Code = "RT565456", Description = "Disprin", InvoiceId = 1, InvoiceLineId = 1, LineTotal = 453, Qty = 2
            } } };

            //Arrange
            var mock = new Mock<IInvoice>();
            mock.Setup(p => p.CreateInvoice(invoice)).Returns("Created successfully");


            // Act
            var result = mock.Object.CreateInvoice(invoice);


            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }
        [TestMethod]
        public void Create_Invoice_Line_Creates_Successfully()
        {
            var invoiceLine = new CreateInvoiceLineViewModel { Code = "RT565456", Description = "Disprin", InvoiceId = 1, InvoiceLineId = 1, LineTotal = 453, Qty = 2};

            //Arrange
            var mock = new Mock<IInvoice>();
            mock.Setup(p => p.CreateInvoiceLine(invoiceLine)).Returns("Created successfully");


            // Act
            var result = mock.Object.CreateInvoiceLine(invoiceLine);


            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }
    }
}
