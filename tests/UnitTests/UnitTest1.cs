//using System;
//using System.Net;
//using System.Threading.Tasks;
//using AutoFixture;
//using AutoFixture.AutoMoq;
//using FluentAssertions;
//using MembershipCardSystem.DataStore;
//using MembershipCardSystem.Registration;
//using MembershipCardSystem.Registration.Model;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Xunit;
//
//namespace ControllerTests
//{
//    public class ControllerTests
//    {
//        private readonly RegistrationController _controller;
//
//
//        public ControllerTests()
//        {
//            //  var fixture = new Fixture().Customize(new AutoMoqCustomization{ConfigureMembers = true});
////            _membershipcardrepository = fixture.Freeze<Mock<IMembershipCardRepository>>();
//
//            var membershipcardrepository = new Mock<IMembershipCardRepository>();
//
//            _controller = new RegistrationController(membershipcardrepository.Object);
//        }
//
//        [Fact]
//        public async Task Registers_with_valid_request()
//        {
////            _membershipcardrepository.Setup(c => c.SaveRegistrationDetails(
////                It.Is<string>(s => s.Equals("ID1")),
////                It.Is<string>(s => s.Equals("Name")),
////                It.Is<string>(s => s.Equals("Surname")),
////                It.Is<string>(s => s.Equals("0123"))
////            ));
//
//            var requestModel = new CardDetails
//            {
//                EmployeeId = "ID1",
//                FirstName = "Name",
//                SecondName = "Surname",
//                MobileNumber = "01234"
//            };
//
//            var response = await _controller.Add(requestModel) as ObjectResult;
//            
//            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);
//
//        }
//    }
//}