using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly ManageCourier _manageCourier;

        public CourierController(ManageCourier manageCourier)
        {
            _manageCourier = manageCourier;
        }

        // Save or update a courier
        [HttpPost("Save")]
        public async Task<IActionResult> SaveCourier([FromBody] Courier model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageCourier.SaveCourier(model);

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

        // Delete a courier by ID
        [HttpDelete("Delete/{courierId}")]
        public async Task<IActionResult> DeleteCourier(int courierId)
        {
            try
            {
                var dbResult = await _manageCourier.DeleteCourier(courierId);

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

        // Get all couriers
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCouriers()
        {
            try
            {
                var dbResult = await _manageCourier.GetAllData();

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

        // Get courier by ID
        [HttpGet("Get/{courierId}")]
        public async Task<IActionResult> GetCourier(int courierId)
        {
            try
            {
                var dbResult = await _manageCourier.GetData(courierId);

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
