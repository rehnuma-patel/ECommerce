using BusinessLayer;
using DatabaseLayer.ApplicationContext;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ManageOrder _manageOrder;

        public OrderController(ApplicationDBContext dBContext)
        {
            _manageOrder = new ManageOrder(dBContext);
        }

        // Save order (create or update)
        [HttpPost("Save")]
        public async Task<IActionResult> SaveOrder([FromBody] Order model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageOrder.SaveOrder(model);

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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            var dbResult = await _manageOrder.GetAllData();
            return Ok(dbResult);
        }

        [HttpGet("Details/{regId}")]
        public async Task<IActionResult> GetOrderData(int regId)
        {
            var dbResult = await _manageOrder.GetData(regId);
            return Ok(dbResult);
        }

        [HttpDelete("Delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var dbResult = await _manageOrder.DeleteOrder(orderId);
            return Ok(dbResult);
        }
    }
}
