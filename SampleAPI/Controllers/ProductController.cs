using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Models;
using SampleAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace SampleAPI.Controllers
{

    [Route("[Controller]s")]
    [ApiController]
    //[Authorize]
    //[EnableCors("testApp")]
    public class ProductController : ControllerBase
    {
        private Repository db;

        public ProductController()
        {
            db = new Repository(); 
        }

        private async Task<List<Product>> ProductsFound(int page, int limit)
        {
            int skip = (page - 1) * limit;
            var result = await db.GetAllAsync<Product>();
            result = result.Skip(skip).Take(limit).OrderBy(x => x.Id).ToList();
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int limit = 5)
        {
            return Ok(await ProductsFound(page, limit));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        { 
            var result = await db.GetByIdAsync<Product>(id);
            if (result == null)
            {
                return BadRequest("There is'nt Any product with this information");

            }
            else
            {
                return Ok(result);
            }
        }

        
        [HttpGet("Search/")]
        public async Task<IActionResult> Get(string type, string color,int page = 1, int limit = 5)
        {
            var result = await ProductsFound(page, limit);
            //result = result.Where(x => x.TypeEquals(type)).ToList();
            if (result == null)
            {
                return BadRequest("There is'nt Any product with this information");

            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Information Is Not True");
            }
            await db.CreateAsync<Product>(model);
            return Ok();
        }

        [HttpPut("{model}")]
        public async Task<IActionResult> Edit([FromBody] Product model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Information Is Not True");
            //}
            await db.UpdateAsync<Product>(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("Information Is Not True");
            //}
            await db.DeleteAsync<Product>(id);
            return Ok();
        }


    }
}
