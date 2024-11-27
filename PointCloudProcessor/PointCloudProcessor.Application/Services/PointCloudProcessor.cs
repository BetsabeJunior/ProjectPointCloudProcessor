using PointCloudProcessor.Application.Interfaces;
using PointCloudProcessor.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointCloudProcessor.Application.Services
{
    public class PointCloudProcessorService : IPointCloudProcessor
    {
        private readonly IFileRepository _fileRepository;

        public PointCloudProcessorService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<string> Generate3DModelAsync(List<Point3D> points)
        {
            return await _fileRepository.SaveModelAsync(points);
        }
    }
}
