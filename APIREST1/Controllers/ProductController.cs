using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using APIREST1.Models;

namespace APIREST1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private static List<ProductModel> _products = new List<ProductModel>
        {
            new ProductModel { Id = 1, Name = "Product 1", Price = 20.50M },
            new ProductModel { Id = 2, Name = "Product 2", Price = 30.75M }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            return Ok(_products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductModel> Get(int id)
        {
            var product = _products.Find(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductModel> Post(ProductModel product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProductModel product)
        {
            var existingProduct = _products.Find(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.Find(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);

            return NoContent();
        }
    }
}
