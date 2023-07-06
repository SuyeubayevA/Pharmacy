using Microsoft.AspNetCore.Mvc.Testing;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Pharmacy.API.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]

        public async Task CreateProductHandler_AddsNewProduct()
        {
            var fakeModel = Helper.GetFaker<ProductModel>().Generate();
            var app = new WebApplicationFactory<Program>();
            var httpClient = app.CreateClient();
            string payload = JsonSerializer.Serialize(fakeModel);
            HttpContent ctx = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"/api/Product", ctx);

            response.StatusCode.Equals((int)HttpStatusCode.InternalServerError);
        }
    } 

}
