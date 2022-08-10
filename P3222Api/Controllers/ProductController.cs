using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3222Api.Data;
using P3222Api.Dtos.ProductDtos;
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
            ProductReturnDto productReturnDto = new ProductReturnDto();
            productReturnDto.Name = p.Name;
            productReturnDto.Price = p.Price;
            productReturnDto.IsActive = p.IsActive;
             
            if (p == null)
            {
                return NotFound();
            }
            return Ok(productReturnDto);
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _context.Products.Where(p => !p.IsDeleted);

            ProductListDto productListDto = new ProductListDto();

            productListDto.Items =query.Select(p => new ProductReturnDto
            {
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive

            }).Skip(1).Take(1).ToList();
            productListDto.TotalCount = query.Count();

            return StatusCode(200, productListDto);
        }

        [HttpPost]
        public IActionResult Create(ProductCreateDto productCreateDto)
        {
            Product newP = new Product
            {
                Name = productCreateDto.Name,
                IsActive = productCreateDto.IsActive,
                Price = productCreateDto.Price
            };
         
            _context.Products.Add(newP);
            _context.SaveChanges();
            return StatusCode(201);
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id ,ProductUpdateDto productUpdateDto)
        {
            Product p = _context.Products.FirstOrDefault(p => p.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = productUpdateDto.Name;
            p.Price = productUpdateDto.Price;
            p.IsActive = productUpdateDto.IsActive;
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
