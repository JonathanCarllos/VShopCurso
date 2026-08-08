using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTO_s;
using VShop.ProductApi.Services.Interfaces;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICatergoryService _categoryService;

        public CategoriesController(ICatergoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categoriesDTO = await _categoryService.GetCategories();

            if (categoriesDTO is null)
                return NotFound("Categories Not found");

            return Ok(categoriesDTO);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categoriesDTO = await _categoryService.GetCategoriesProducts();

            if (categoriesDTO == null)
            {
                return NotFound("Categories Not found");
            }
            return Ok(categoriesDTO);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var categoryDTO = await _categoryService.GetCategoryById(id);

            if (categoryDTO == null)
            {
                return NotFound("Category Not found");
            }

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Invalid Data");

            await _categoryService.AddCategory(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId }, categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest("Invalid Id");

            if (categoryDTO is null)
                return BadRequest("Invalid Data");

            await _categoryService.UpdateCategory(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDTO = await _categoryService.GetCategoryById(id);

            if (categoryDTO is null)
                return NotFound("Category Not found");

            await _categoryService.RemoveCategory(id);
            return Ok(categoryDTO);

        }
    }
}