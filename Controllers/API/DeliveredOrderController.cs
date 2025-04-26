using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveredOrderController : ControllerBase
    {
        private readonly ManageDelivredOrder _manageOrder;
        public DeliveredOrderController(ManageDelivredOrder manageOrder)
        {
            _manageOrder = manageOrder;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] DeliverdOrder model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageOrder.SaveDelivery(model);

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
