using BusinessLayer;
using DatabaseLayer.ApplicationContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ECommerce.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly ApplicationDBContext _dBContex;

        public PhotoController(ApplicationDBContext dBContex)
        {
            _dBContex = dBContex;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SavePhoto([FromForm] Photo model, [FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new DBResult("Fail", "No file uploaded"));

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string extension = Path.GetExtension(file.FileName);
                string fileName = $"Img{timestamp}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                model.Imagepath = fileName;

                if (model.PhotoId == 0)
                {
                    _dBContex.Photos.Add(model);
                }
                else
                {
                    var existingPhoto = await _dBContex.Photos.FindAsync(model.PhotoId);
                    if (existingPhoto == null)
                        return NotFound(new DBResult("Fail", "Photo not found for update"));

                    existingPhoto.Imagepath = model.Imagepath;
                    existingPhoto.ProductId = model.ProductId;
                    // Copy other fields as needed
                    _dBContex.Photos.Update(existingPhoto);
                }

                await _dBContex.SaveChangesAsync();
                return Ok(new DBResult("Success", "Photo saved successfully"));
            }
            catch (Exception exp)
            {
                return StatusCode(500, new DBResult("Error", exp.Message));
            }
        }

        [HttpDelete("delete/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            try
            {
                var photo = await _dBContex.Photos.FindAsync(photoId);
                if (photo == null)
                {
                    return NotFound(new DBResult("Fail", "Photo not found"));
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products", photo.Imagepath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _dBContex.Photos.Remove(photo);
                await _dBContex.SaveChangesAsync();
                return Ok(new DBResult("Success", "Photo deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DBResult("Error", ex.Message));
            }
        }
    }
}
