using Microsoft.EntityFrameworkCore;
using OpenDoor.Models;

namespace OpenDoor.Services
{
    public class ComicService : IComicService
    {
        private readonly ApplicationDbContext _context;

        public ComicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comic>> GetAllComicsAsync()
        {
            return await _context.Comics.ToListAsync();
        }

        public async Task<Comic> GetComicByIdAsync(int id)
        {
            return await _context.Comics.FindAsync(id);
        }

        public async Task<Comic> CreateComicAsync(Comic comic)
        {
            _context.Comics.Add(comic);
            await _context.SaveChangesAsync();
            return comic;
        }

        public async Task UpdateComicAsync(int id, Comic comic)
        {
            if (id != comic.Id)
                throw new ArgumentException("ID комикса не совпадает с переданным");

            _context.Entry(comic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComicAsync(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
                throw new KeyNotFoundException("Комикс не найден");

            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();
        }

        public async Task<Comic> PatchComicAsync(int id, Comic comic)
        {
            var existingComic = await _context.Comics.FindAsync(id);
            if (existingComic == null)
                throw new KeyNotFoundException("Комикс не найден");

            // Обновляем только переданные данные
            existingComic.Name = comic.Name ?? existingComic.Name;
            existingComic.ImageUrl = comic.ImageUrl ?? existingComic.ImageUrl;
            existingComic.Price = comic.Price != 0 ? comic.Price : existingComic.Price;
            existingComic.Description = comic.Description ?? existingComic.Description;
            existingComic.PublishedDate = comic.PublishedDate != null
                ? comic.PublishedDate
                : existingComic.PublishedDate;
            existingComic.Stock = comic.Stock != 0 ? comic.Stock : existingComic.Stock;

            await _context.SaveChangesAsync();

            return existingComic;
        }
    }
}
