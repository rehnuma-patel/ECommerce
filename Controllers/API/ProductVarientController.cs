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
    public class ProductVarientController : ControllerBase
    {
        private readonly ManageProductvarient _manageProductvarient;

        public ProductVarientController(ApplicationDBContext dBContext)
        {
            _manageProductvarient = new ManageProductvarient(dBContext);
        }

        // Save Product Variant
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

        // Get All Product Variants
        [HttpGet("GetAllVarients")]
        public async Task<IActionResult> GetAllVarients()
        {
            try
            {
                var dbResult = await _manageProductvarient.GetAllVarient();
                return Ok(new
                {
                    Status = dbResult.Status,
                    Result = dbResult.Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }

        // Get Product Variants by ProductId
        [HttpGet("GetData/{productId}")]
        public async Task<IActionResult> GetVarientsByProductId(int productId)
        {
            try
            {
                var dbResult = await _manageProductvarient.GetData(productId);
                return Ok(new
                {
                    Status = dbResult.Status,
                    Result = dbResult.Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }

        // Delete Product Variant
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProductvarient(int id)
        {
            try
            {
                var dbResult = await _manageProductvarient.DeleteProductvarient(id);

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
