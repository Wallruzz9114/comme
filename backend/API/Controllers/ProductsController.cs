using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            Ok(await _databaseContext.Products.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) =>
            await _databaseContext.Products.FindAsync(id);
    }
}