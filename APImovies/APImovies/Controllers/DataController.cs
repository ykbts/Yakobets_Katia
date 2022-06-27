using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using APImovies.Constatnt;
using APImovies.Model;
using APImovies.Client;
using APImovies.Data;
using Microsoft.EntityFrameworkCore;

namespace APImovies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase

    {
        private readonly MovieTrailerContext _context;

        public DataController(MovieTrailerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieURL>>> GetMovieURL()
        {
            return await _context.MovieURLs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieURL>> GetMovieURL(string id)
        {
            var movieUrl = await _context.MovieURLs.FindAsync(id);

            if (movieUrl == null)
            {
                return NotFound();
            }

            return movieUrl;
        }
        [HttpPost]
        public async Task<ActionResult<MovieURL>> PostMovieURL(MovieURL movieURL)
        {
            _context.MovieURLs.Add(movieURL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovieURL", new { IMDbId = movieURL.IMDbId }, movieURL);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovieURL(string IMDbId)
        {
            var movieURL = await _context.MovieURLs.FindAsync(IMDbId);
            if (movieURL == null)
            {
                return NotFound();
            }

            _context.MovieURLs.Remove(movieURL);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool MovieURLExists(string IMDbId)
        {
            return _context.MovieURLs.Any(e => e.IMDbId == IMDbId);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> PutMovieURL(string IMDbId, MovieURL movieURL)
        {
            if (IMDbId != movieURL.IMDbId)
            {
                return BadRequest();
            }

            _context.Entry(movieURL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieURLExists(IMDbId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }
    }
}