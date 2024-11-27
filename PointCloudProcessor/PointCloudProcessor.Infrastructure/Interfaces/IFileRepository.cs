using PointCloudProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointCloudProcessor.Application.Interfaces
{
    public interface IFileRepository
    {
        Task<string> SaveModelAsync(List<Point3D> points);
    }

}
