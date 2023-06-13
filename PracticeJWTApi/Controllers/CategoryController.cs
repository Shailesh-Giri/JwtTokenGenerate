using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practice.Domain.Entities;
using Practice.Infrastructure.IRepository;
using PracticeJWTApi.Model;

namespace PracticeJWTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<List<Category>> response;
            try
            {
                List<Category> categories  = await _categoryRepository.GetAll();

                response = new ApiResponse<List<Category>>(true, "Success", categories);

                return Ok(response);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Category category = await _categoryRepository.GetById(id);
                return Ok(category);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            
            try
            {
                int entry = await _categoryRepository.Add(category);
                return Ok(entry);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            try
            {
                int entry = await _categoryRepository.Update(category);
                return Ok(entry);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int entry = await _categoryRepository.Delete(id);
                return Ok(entry);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
