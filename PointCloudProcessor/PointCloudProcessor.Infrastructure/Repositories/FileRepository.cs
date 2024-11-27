using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PointCloudProcessor.Application.Interfaces;
using PointCloudProcessor.Domain.Models;

namespace PointCloudProcessor.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        /// <summary>
        /// Guarda un modelo 3D en formato OBJ a partir de una lista de puntos.
        /// </summary>
        /// <param name="points">Lista de puntos 3D</param>
        /// <returns>Ruta al archivo OBJ generado</returns>
        public async Task<string> SaveModelAsync(List<Point3D> points)
        {
            // Creamos la ruta para el archivo OBJ temporal
            var filePath = Path.Combine(Path.GetTempPath(), $"model_{Guid.NewGuid()}.obj");

            // Usamos un StreamWriter para escribir el archivo OBJ
            using (var writer = new StreamWriter(filePath))
            {
                // Escribimos los vértices (puntos) en el formato OBJ
                foreach (var point in points)
                {
                    writer.WriteLine($"v {point.X} {point.Y} {point.Z}");
                }

                // Aquí podrías agregar otras funcionalidades como la escritura de caras o mallas
                // Por ejemplo, puedes agregar un objeto con una malla vacía: "o myMesh"
                writer.WriteLine("o PointCloud");

                // Otras definiciones o configuraciones del objeto podrían ser añadidas aquí

                // Finaliza el archivo
            }

            // Devolvemos la ruta al archivo generado
            return filePath;
        }
    }
}
