using BusinessLayer;
using DatabaseLayer.DBOperation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ManageRegistration _manageRegistration;
        public RegistrationController(ManageRegistration manageRegistration)
        {
            _manageRegistration = manageRegistration;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveUser([FromBody] Registration model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is required" });
                }

                var dbResult = await _manageRegistration.SaveRegistration(model);

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
