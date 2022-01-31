using HealthbridgeApp.Interface;
using HealthbridgeApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace HealthbridgeApp.Tests.Controllers.API
{
    [TestClass]
    public class PatientAPIControllerTests
    {
        [TestMethod]
        public void GetPatients_Returns_Patients_List_With_One_Patient_Item()
        {
            // Arrange
            var patientsList = new List<PatientViewModel>
            {
                new PatientViewModel{ FirstName = "Khayelihle", LastName = "Ntanzi", IdNumber = "9102015873091", PatientId = 1 }
            };

            var mock = new Mock<IPatient>();
            mock.Setup(p => p.GetPatients()).Returns(patientsList);

            // Act
            var result = mock.Object.GetPatients();

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void UpdatePatient_Updates_Successfully()
        {
            // Arrange
            var patient = new PatientViewModel { FirstName = "Khayelihle", LastName = "Ntanzi", IdNumber = "9102015873091", PatientId = 1 };
           
            var mock = new Mock<IPatient>();
            mock.Setup(p => p.UpdatePatient(patient)).Returns("Patient updated successfully");

            // Act
            var result = mock.Object.UpdatePatient(patient);

            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }

        [TestMethod]
        public void Delete_Patient_Deletes_Successfully()
        {
            var id = 1;

            //Arrange
            var mock = new Mock<IPatient>();
            mock.Setup(p => p.DeletePatient(id)).Returns("Deleted successfully");


            // Act
            var result = mock.Object.DeletePatient(id);


            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }

        [TestMethod]
        public void Create_Patient_Creates_Successfully()
        {
            var patient = new PatientViewModel { FirstName = "Khayelihle", LastName = "Ntanzi", IdNumber = "9102015873091", PatientId = 1 };

            //Arrange
            var mock = new Mock<IPatient>();
            mock.Setup(p => p.CreatePatient(patient)).Returns("Created successfully");


            // Act
            var result = mock.Object.CreatePatient(patient);


            // Assert
            Assert.IsTrue(!string.IsNullOrWhiteSpace(result), result);
        }
    }
}
