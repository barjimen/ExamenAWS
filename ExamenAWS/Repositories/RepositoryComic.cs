using ExamenAWS.Data;
using ExamenAWS.Models;
using ExamenAWS.Services;
using Microsoft.EntityFrameworkCore;

namespace ExamenAWS.Repositories
{
    public class RepositoryComic
    {
        private ComicsContext context;
        private ServiceStorageS3 service;
        public RepositoryComic(ComicsContext context, ServiceStorageS3 service)
        {
            this.context = context;
            this.service = service;
        }
        public async Task<List<Comic>> GetComics()
        {
            return context.Comics.ToList();
        }
        public async Task<Comic> GetComic(int id)
        {
            return context.Comics.FirstOrDefault(x => x.IdComic == id);
        }

        public async Task<int> MaxIdAsync()
        {
            var maxId = await this.context.Comics.MaxAsync(x => x.IdComic);
            return maxId + 1;
        }

        public async Task<Comic> AddComic(Comic comic, IFormFile file)
        {
            Comic com = new Comic
            {
                IdComic = await MaxIdAsync(),
                Nombre = comic.Nombre,
                Imagen = file.FileName
            };
            using (Stream stream = file.OpenReadStream())
            {
                await service.UploadFileAsync(file.FileName, stream);
            }
            context.Comics.Add(com);
            await context.SaveChangesAsync();
            return comic;
        }

    }
}
