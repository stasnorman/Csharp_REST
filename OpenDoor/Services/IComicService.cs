using OpenDoor.Models;

namespace OpenDoor.Services
{
    public interface IComicService
    {
        Task<IEnumerable<Comic>> GetAllComicsAsync();
        Task<Comic> GetComicByIdAsync(int id);
        Task<Comic> CreateComicAsync(Comic comic);
        Task UpdateComicAsync(int id, Comic comic);
        Task DeleteComicAsync(int id);
        Task<Comic> PatchComicAsync(int id, Comic comic);
    }
}
