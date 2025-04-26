using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ManageCustomerAddress _manageCustomerAddress;

        public AddressController(ManageCustomerAddress manageCustomerAddress)
        {
            _manageCustomerAddress = manageCustomerAddress;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAddress([FromBody] CustomerAddress model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageCustomerAddress.SaveAddress(model);

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

        [HttpDelete("Delete/{customerAddressId}")]
        public async Task<IActionResult> DeleteAddress(int customerAddressId)
        {
            try
            {
                var dbResult = await _manageCustomerAddress.DeleteAddress(customerAddressId);

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

        [HttpGet("GetAllDetails")]
        public async Task<IActionResult> GetAllDetails()
        {
            try
            {
                var dbResult = await _manageCustomerAddress.AllDetails();

                return Ok(new
                {
                    Status = dbResult.Status == "Success" ? "OK" : "Fail",
                    Result = dbResult.Result,
                    Data = dbResult.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }

        [HttpGet("GetDetailsByRegId/{regId}")]
        public async Task<IActionResult> GetDetailsByRegId(int regId)
        {
            try
            {
                var dbResult = await _manageCustomerAddress.Details(regId);

                return Ok(new
                {
                    Status = dbResult.Status == "Success" ? "OK" : "Fail",
                    Result = dbResult.Result,
                    Data = dbResult.Data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Result = ex.Message });
            }
        }
    }
}
