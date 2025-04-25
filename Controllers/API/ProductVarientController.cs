using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVarientController : ControllerBase
    {
        private readonly ManageProductvarient _manageProductvarient;
        public ProductVarientController(ManageProductvarient manageProductvarient)
        {
            _manageProductvarient = manageProductvarient;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveProductvarient([FromBody] Productvarient model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageProductvarient.SaveProductvarient(model);

                return Ok(new
                {
                    Status = dbResult.Status == "Success" ? "OK" : "Fail",
                    Result = dbResult.Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }
    }
}
