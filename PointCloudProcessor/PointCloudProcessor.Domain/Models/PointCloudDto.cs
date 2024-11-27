using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointCloudProcessor.Domain.Models
{
    public class PointCloudDto
    {
        public List<Point3D> Points { get; set; } = new List<Point3D>();
    }
}
