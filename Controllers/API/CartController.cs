using BusinessLayer;
using DatabaseLayer.ApplicationContext;
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

        public CartController(ApplicationDBContext dBContext)
        {
            _manageCart = new ManageCart(dBContext);
        }

        // Save or Update Cart
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

        // Delete Cart by Id
        [HttpDelete("Delete/{cartId}")]
        public async Task<IActionResult> DeleteCart(int cartId)
        {
            try
            {
                var dbResult = await _manageCart.DeleteCart(cartId);

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

        // Get All Carts
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var dbResult = await _manageCart.GetAllCarts();

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

        // Get Carts by RegistrationId
        [HttpGet("GetByRegId/{regId}")]
        public async Task<IActionResult> GetData(int regId)
        {
            try
            {
                var dbResult = await _manageCart.GetData(regId);
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
