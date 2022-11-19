using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Models;
using SampleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        //private async Task<List<Product>> ProductsFound(int page, int limit)
        //{
        //    int skip = (page - 1) * limit;
        //    var result = await db.GetAllAsync<Product>();
        //    result = result.Skip(skip).Take(limit).OrderBy(x => x.Id).ToList();           
        //    return result;
        //}

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int limit = 5)
        {
            int skip = (page - 1) * limit;
            var result = await db.GetAllAsync<Product>();
            result = result.Skip(skip).Take(limit).OrderBy(x => x.Id).ToList();
            return Ok(result);
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
        public async Task<IActionResult> Get(TypeEnum type, ColorEnum color,int page = 1, int limit = 5)
        {
            int skip = (page - 1) * limit;
            var result = await db.GetAllAsync<Product>();           
            if ((int)type != 0)
            {
                result = result.Where(x => x.Type == type).ToList();
            }
            
            if(color != 0)
            {
                result = result.Where(x => x.Color == color).ToList();
            }
            result = result.Skip(skip).Take(limit).OrderBy(x => x.Id).ToList();
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
        public async Task<IActionResult> Create([FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Information Is Not True");
            }
            await db.CreateAsync<Product>(new Product {Title = model.Title, Price = model.Price, Color = model.Color, Type = model.Type});
            return Ok();
        }

        //[HttpPut()]
        //public async Task<IActionResult> Edit([FromBody] Product model)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest("Information Is Not True");
        //    //}
        //    await db.UpdateAsync<Product>(model);
        //    return Ok();
        //}

        [HttpPut()]
        public async Task<IActionResult> Edit(string id, [FromBody] Product model)
        {
            var entity = await db.GetByIdAsync<Product>(id);
            if (entity == null)
            {
                return NotFound("There Isn't Any Product Whit This Id");
            }
            await db.UpdateAsync<Product>(model);
            //await db.UpdateAsync<Product>(new Product {Id = id, Title = model.Title, Price = model.Price, Color = model.Color, Type = model.Type});
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
