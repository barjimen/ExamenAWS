using ExamenAWS.Models;
using ExamenAWS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenAWS.Controllers
{
    public class ComicController : Controller
    {
        private RepositoryComic repo;
        public ComicController(RepositoryComic repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var comics = await repo.GetComics();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Comic comic, IFormFile file)
        {
            await this.repo.AddComic(comic, file);
            return RedirectToAction("Index");
        }
    }
}
