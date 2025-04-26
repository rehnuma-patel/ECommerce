using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ManageCategory _manageCategory;

        public CategoryController(ManageCategory manageCategory)
        {
            _manageCategory = manageCategory;
        }

        // Save Category
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> SaveCategory([FromBody] Category model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new DBResult("Fail", "Model is required"));
                }

                var dbResult = await _manageCategory.SaveCategory(model);

                return Ok(dbResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DBResult("Error", ex.Message));
            }
        }

        // Delete Category
        [HttpDelete("Delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var dbResult = await _manageCategory.DeleteCategory(categoryId);
                return Ok(dbResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DBResult("Error", ex.Message));
            }
        }

        // Get All Categories
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var dbResult = await _manageCategory.GetAllCategories();
                return Ok(dbResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DBResult("Error", ex.Message));
            }
        }
    }
}
