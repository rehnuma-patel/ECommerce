using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ManageCart _manageCart;
        public CartController(ManageCart manageCart)
        {
            _manageCart = manageCart;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveCart([FromBody] Cart model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageCart.SaveCart(model);

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
