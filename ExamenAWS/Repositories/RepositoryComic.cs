using ExamenAWS.Data;
using ExamenAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenAWS.Repositories
{
    public class RepositoryComic
    {
        private ComicsContext context;
        public RepositoryComic(ComicsContext context)
        {
            this.context = context;
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

        public async Task<Comic> AddComic(Comic comic)
        {
            Comic com = new Comic
            {
                IdComic = await MaxIdAsync(),
                Nombre = comic.Nombre,
                Imagen = comic.Imagen
            };
            context.Comics.Add(com);
            await context.SaveChangesAsync();
            return comic;
        }

    }
}
