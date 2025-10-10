using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FMMI_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DataController : Controller
    {
        private readonly ILogger<DataController> _logger;

        public DataController(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
        }

        //[HttpGet]
        //public ProductsDTO GetProductById(int id)
        //{
        //    ProductsDTO product = _productServices.GetProductById(id);
        //    if (product != null)
        //    {
        //        return product;
        //    }
        //    else
        //    {
        //        return product = new ProductsDTO();
        //    }
        //}


        //[HttpPost]
        //public async Task CreateProduct(ProductsDTO newProduct)
        //{
        //    await _productServices.AddProductAsync(newProduct);
        //}


        //[HttpPut]
        //public async Task UpdateProductById(ProductsDTO productsDTO)
        //{
        //    await _productServices.UpdateProductAsync(productsDTO);
        //}

        //[HttpDelete]
        //public async Task DeleteProductById(int id)
        //{
        //    await _productServices.DeleteProductAsync(id);
        //}
    }
}
