using ContactManagement.Lib.AbstractRepository;
using ContactManagement.Lib.Model;
using ContactManagement.Service.Controllers;
using ContactManagement.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ContactManagement.Test
{
    [TestClass]
    public class ContactControllerTest
    {

        ContactsController _controller = null;
        Mock<IContactRepository> _mockRepo = new Mock<IContactRepository>();

        //[TestInitialize]
        public ContactControllerTest()
        {
            _mockRepo = new Mock<IContactRepository>();
            _controller = new ContactsController(_mockRepo.Object);
        }

        [TestMethod]
        public void Create_ReturnsBadRequest_GivenInvalidModel()
        {
            _controller.ModelState.AddModelError("error", "some error");
            var result = _controller.Create(contactApiModel: null);
            Assert.IsInstanceOfType(result, typeof (BadRequestObjectResult)); 
            
        }

        private void SimulateValidation(ContactApiModel model)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        [TestMethod]
        public void Create_ReturnsBadRequest_GivenInvalidModelData()
        {

            var contactApiModel = new ContactApiModel();
            contactApiModel.Email = string.Empty;
            contactApiModel.FirstName = "TestName";
            contactApiModel.LastName = "TestLastName";
            contactApiModel.PhoneNumber = "9011534816";
            contactApiModel.Status = Lib.Model.ContactStatus.Active;

            SimulateValidation(contactApiModel);

            var result = _controller.Create(contactApiModel);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

        }

        [TestMethod]
        public void Create_GivenValidModel()
        {
            var mockRepo = new Mock<IContactRepository>();
            var controller = new ContactsController(mockRepo.Object);
            var contactApiModel = new ContactApiModel();
            contactApiModel.Email = "test.abcd@test.com";
            contactApiModel.FirstName = "TestName";
            contactApiModel.LastName = "TestLastName";
            contactApiModel.PhoneNumber = "9011534816";
            contactApiModel.Status = Lib.Model.ContactStatus.Active;

            CreatedAtRouteResult result = (CreatedAtRouteResult) controller.Create(contactApiModel);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteResult));
            Contact contact = (Contact)result.Value;
            Assert.AreEqual("test.abcd@test.com", contact.Email);
            Assert.AreEqual("TestName", contact.FirstName);
            Assert.AreEqual("TestLastName", contact.LastName);
            Assert.AreEqual("9011534816", contact.PhoneNumber);
            Assert.AreEqual(ContactStatus.Active, contact.Status);

        }

    }
}
