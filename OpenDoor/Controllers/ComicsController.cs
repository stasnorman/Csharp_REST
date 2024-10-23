using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenDoor.Models;
using OpenDoor.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Все действия в этом контроллере требуют авторизации
public class ComicsController : ControllerBase
{
    private readonly IComicService _comicService;

    public ComicsController(IComicService comicService)
    {
        _comicService = comicService;
    }

    // GET: api/Comics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comic>>> GetAllComics()
    {
        var comics = await _comicService.GetAllComicsAsync();
        return Ok(comics);
    }

    // GET: api/Comics/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Comic>> GetComicById(int id)
    {
        var comic = await _comicService.GetComicByIdAsync(id);
        if (comic == null)
        {
            return NotFound();
        }
        return Ok(comic);
    }

    // POST: api/Comics
    [HttpPost]
    public async Task<ActionResult<Comic>> PostComic(Comic comic)
    {
        var createdComic = await _comicService.CreateComicAsync(comic);
        return CreatedAtAction(nameof(GetComicById), new { id = createdComic.Id }, createdComic);
    }

    // PUT: api/Comics/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutComic(int id, Comic comic)
    {
        try
        {
            await _comicService.UpdateComicAsync(id, comic);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PATCH: api/Comics/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchComic(int id, Comic comic)
    {
        try
        {
            var updatedComic = await _comicService.PatchComicAsync(id, comic);
            return Ok(updatedComic);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // DELETE: api/Comics/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComic(int id)
    {
        try
        {
            await _comicService.DeleteComicAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
