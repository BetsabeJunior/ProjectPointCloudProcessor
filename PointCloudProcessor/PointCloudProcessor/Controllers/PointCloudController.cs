using Microsoft.AspNetCore.Mvc;
using PointCloudProcessor.Application.Interfaces;
using PointCloudProcessor.Domain.Models;
using System.IO;

namespace PointCloudProcessor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointCloudController : ControllerBase
    {
        private readonly IPointCloudProcessor _pointCloudProcessor;

        public PointCloudController(IPointCloudProcessor pointCloudProcessor)
        {
            _pointCloudProcessor = pointCloudProcessor;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPointCloud([FromBody] PointCloudDto pointCloud)
        {
            if (pointCloud.Points == null || pointCloud.Points.Count == 0)
            {
                return BadRequest("No se proporcionaron puntos.");
            }

            try
            {
                var filePath = await _pointCloudProcessor.Generate3DModelAsync(pointCloud.Points);

                if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                {
                    return StatusCode(500, "No se pudo generar el archivo 3D.");
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                System.IO.File.Delete(filePath);

                return File(fileBytes, "application/octet-stream", "model.obj");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al generar el modelo 3D: {ex.Message}");
            }
        }
    }
}
