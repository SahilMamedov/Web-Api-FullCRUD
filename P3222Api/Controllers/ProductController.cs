using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3222Api.Data;
using P3222Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3222Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

       
public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            Product p =_context.Products.Where(p => p.IsActive).FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(200,_context.Products.Where(p=>p.IsActive).ToList());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
         
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201);
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id ,Product product)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = product.Name;
            p.Price = product.Price;
            p.IsActive = product.IsActive;
            _context.SaveChanges();
            return StatusCode(200, p);

        }

        [HttpPatch("{id}")]
        public IActionResult ChangeIsActive(int id ,bool isActive)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            p.IsActive = isActive;
            _context.SaveChanges();
            return StatusCode(200, p);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            _context.Products.Remove(p);
            _context.SaveChanges();
            return StatusCode(200);
        }

    }
}
