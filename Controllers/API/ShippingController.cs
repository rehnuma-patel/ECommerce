using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly ManageShipping _manageShipping;

        public ShippingController(ManageShipping manageShipping)
        {
            _manageShipping = manageShipping;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveShip([FromBody] Shipping model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageShipping.SaveShip(model);

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

        [HttpDelete("Delete/{shippingId}")]
        public async Task<IActionResult> DeleteShipping(int shippingId)
        {
            var dbResult = await _manageShipping.DeleteShipping(shippingId);
            return Ok(dbResult);
        }

        [HttpGet("Get/{shippingId}")]
        public async Task<IActionResult> GetData(int shippingId)
        {
            var dbResult = await _manageShipping.GetData(shippingId);
            return Ok(dbResult);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllData()
        {
            var dbResult = await _manageShipping.GetAllData();
            return Ok(dbResult);
        }

        [HttpGet("GetByOrder/{orderId}")]
        public async Task<IActionResult> GetDataByOrderId(int orderId)
        {
            var dbResult = await _manageShipping.GetDataByOrderId(orderId);
            return Ok(dbResult);
        }

    }
}
