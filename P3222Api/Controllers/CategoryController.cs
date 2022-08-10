using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P3222Api.Data;
using P3222Api.Dtos.CategoryDtos;
using P3222Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3222Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;


        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {

            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            CategoryReturnDto categoryReturnDto = new CategoryReturnDto
            {
                Name = category.Name,

            };

            return Ok(categoryReturnDto);

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _context.Categories.AsQueryable();
            CategoryListDto cetogoryList = new CategoryListDto();
            cetogoryList.Items = query.Select(p => new CategoryReturnDto
            {
                Name = p.Name,

            }).ToList();
            cetogoryList.TotalCount = query.Count();
            return Ok(cetogoryList);

        }
        [HttpPost]
        public IActionResult Create(CategoryCreateDto categoryCreate)
        {
            Category category = new Category
            {
                Name = categoryCreate.Name,

            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto CategoryUpdateDto)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = CategoryUpdateDto.Name;
            return Ok(category);


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);


            _context.SaveChanges();
            return Ok();
        }
    }
}
