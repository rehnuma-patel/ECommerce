using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ManageStock _manageStock;

        public StockController(ManageStock manageStock)
        {
            _manageStock = manageStock;
        }

        // Save Stock (POST)
        [HttpPost("Save")]
        public async Task<IActionResult> SaveStock([FromBody] Stock model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageStock.SaveStock(model);

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

        // Get All Products (GET)
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var dbResult = await _manageStock.GetAllProducts();

                if (dbResult.Status == "Success")
                {
                    return Ok(new
                    {
                        Status = "OK",
                        Result = dbResult.Result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = dbResult.Result
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }

        // Delete Stock (DELETE)
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            try
            {
                var dbResult = await _manageStock.DeleteStock(id);

                if (dbResult.Status == "Success")
                {
                    return Ok(new
                    {
                        Status = "OK",
                        Result = dbResult.Result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = dbResult.Result
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }
    }
}
