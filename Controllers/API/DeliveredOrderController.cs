using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveredOrderController : ControllerBase
    {
        private readonly ManageDelivredOrder _manageDelivredOrder;

        public DeliveredOrderController(ManageDelivredOrder manageDelivredOrder)
        {
            _manageDelivredOrder = manageDelivredOrder;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveDelivery([FromBody] DeliverdOrder model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageDelivredOrder.SaveDelivery(model);

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

        [HttpDelete("Delete/{deliveredOrderId}")]
        public async Task<IActionResult> DeleteDelivery(int deliveredOrderId)
        {
            var dbResult = await _manageDelivredOrder.DeleteDelivery(deliveredOrderId);
            return Ok(dbResult);
        }

        [HttpGet("Get/{deliveredOrderId}")]
        public async Task<IActionResult> GetData(int deliveredOrderId)
        {
            var dbResult = await _manageDelivredOrder.GetData(deliveredOrderId);
            return Ok(dbResult);
        }
    }
}
