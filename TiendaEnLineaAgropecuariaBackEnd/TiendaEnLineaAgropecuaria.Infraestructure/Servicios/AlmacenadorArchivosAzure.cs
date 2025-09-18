using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaEnLineaAgropecuaria.Infraestructure.Servicios
{
    public class AlmacenadorArchivosAzure : IAlmacenadorArchivos
    {
        private readonly string connectionString;

        public AlmacenadorArchivosAzure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorageConnection")!;
        }

        public async Task<string> Almacenar(string contenedor, IFormFile foto)
        {
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();

            cliente.SetAccessPolicy(PublicAccessType.Blob);

            var extension = Path.GetExtension(foto.FileName);
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(nombreArchivo);
            var blobHttpHeaders = new BlobHttpHeaders();
            blobHttpHeaders.ContentType = foto.ContentType;
            await blob.UploadAsync(foto.OpenReadStream(), blobHttpHeaders);

            return blob.Uri.ToString();
        }

        public async Task Borrar(string urlFoto, string contenedor)
        {
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();

            var nombreArchivo = Path.GetFileName(urlFoto);
            var blob = cliente.GetBlobClient(nombreArchivo);
            await blob.DeleteIfExistsAsync();
        }
    }
}
