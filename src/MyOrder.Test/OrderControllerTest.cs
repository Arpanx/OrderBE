using AngularWebpackVisualStudio.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyOrder.Data.Abstract;
using MyOrder.Service;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using MyOrder.Model;
using System.Collections.Generic;
using MyOrder.API.ViewModels.Mappings;

namespace MyOrder
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void Get_TestMethod()
        {
            AutoMapperConfiguration.Configure();
            // Arrange mocks
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockItemRepository = new Mock<IItemRepository>();
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
                .Setup(c => c.Get(1, 10))
                .Returns(new Orders[]
                {
                    new Orders  { Id = 1, Name = "Megan	Fox", Address = "Bennelong Point, 1", City = "Sidney", Items = new List<Item>() },
                    new Orders  { Id = 1, Name = "Megan	Fox", Address = "Bennelong Point, 1", City = "Sidney", Items = new List<Item>() }
                });

            // Target object
            OrderController controller = new OrderController(mockOrderRepository.Object, mockItemRepository.Object, mockOrderService.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Headers["Pagination"] = "1,10";

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var viewResult = (OkObjectResult)result;
            Assert.IsNotNull(viewResult.StatusCode);
            // Assert.That(viewResult.ViewData.Model is ProductsListViewData);
        }
    }
}
