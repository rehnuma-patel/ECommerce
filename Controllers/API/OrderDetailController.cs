using BusinessLayer;
using DatabaseLayer.ApplicationContext;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ManageOrderDetail _manageOrderDetail;

        public OrderDetailController(ApplicationDBContext dBContext)
        {
            _manageOrderDetail = new ManageOrderDetail(dBContext);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveDetail([FromBody] OrderDetail model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageOrderDetail.SaveDetail(model);

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

        [HttpDelete("Delete/{orderDetailId}")]
        public async Task<IActionResult> DeleteDetail(int orderDetailId)
        {
            try
            {
                var dbResult = await _manageOrderDetail.DeleteDetail(orderDetailId);

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
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var dbResult = await _manageOrderDetail.GetAllData();

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

        [HttpGet("Get/{orderDetailId}")]
        public async Task<IActionResult> GetData(int orderDetailId)
        {
            try
            {
                var dbResult = await _manageOrderDetail.GetData(orderDetailId);

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
