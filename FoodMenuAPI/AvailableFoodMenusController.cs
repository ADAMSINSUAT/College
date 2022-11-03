using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodMenuAPI.Models;

namespace FoodMenuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableFoodMenusController : ControllerBase
    {
        private readonly AndroidFoodMenuDBContext _context;

        public AvailableFoodMenusController(AndroidFoodMenuDBContext context)
        {
            _context = context;
        }

        //GET: api/AvailableFoodMenus
       [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailableFoodMenu>>> GetFoodMenu()
        {
            return await _context.AvailableFoodMenu.ToListAsync();
        }

        //GET: api/AvailableFoodMenus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AvailableFoodMenu>> GetFoodMenu(Guid foodid)
        {
            var availableFoodMenu = await _context.AvailableFoodMenu.FindAsync(foodid);

            if (availableFoodMenu == null)
            {
                return NotFound();
            }

            return availableFoodMenu;
        }

        //// PUT: api/AvailableFoodMenus/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAvailableFoodMenu(Guid id, AvailableFoodMenu availableFoodMenu)
        //{
        //    if (id != availableFoodMenu.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(availableFoodMenu).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AvailableFoodMenuExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/AvailableFoodMenus
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<AvailableFoodMenu>> PostAvailableFoodMenu(AvailableFoodMenu availableFoodMenu)
        //{
        //    _context.AvailableFoodMenu.Add(availableFoodMenu);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetAvailableFoodMenu", new { id = availableFoodMenu.id }, availableFoodMenu);
        //}

        //// DELETE: api/AvailableFoodMenus/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<AvailableFoodMenu>> DeleteAvailableFoodMenu(Guid id)
        //{
        //    var availableFoodMenu = await _context.AvailableFoodMenu.FindAsync(id);
        //    if (availableFoodMenu == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.AvailableFoodMenu.Remove(availableFoodMenu);
        //    await _context.SaveChangesAsync();

        //    return availableFoodMenu;
        //}

        //private bool AvailableFoodMenuExists(Guid id)
        //{
        //    return _context.AvailableFoodMenu.Any(e => e.id == id);
        //}
    }
}
