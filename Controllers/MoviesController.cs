using CRUD_Operations_.Net_Core___Movies_Site.Models;
using CRUD_Operations_.Net_Core___Movies_Site.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_.Net_Core___Movies_Site.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification; 
        private new List<string> allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long maxPosterSize = 1048576;
        public MoviesController(ApplicationDbContext context , IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification; 
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.OrderByDescending(r => r.Rate).ToListAsync();
            ViewBag.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();


            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = await _context.Genres.OrderBy(m=>m.Name).ToListAsync()
            };
            return View(viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                return View(model);
            }
            var files = Request.Form.Files;
            if (!files.Any())
            {
                model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please Select Poster");
                return View(model);
            }

            var poster = files.FirstOrDefault();
            if (!allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only JPG , PNG Alowed");
                return View(model);
            }
            if (poster.Length > maxPosterSize)
            {
                model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Can't be More Than 1 MB");
                return View(model);
            }

            using var dataStream = new MemoryStream();
            await poster.CopyToAsync(dataStream);

            var movies = new Movie
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Year = model.Year,
                Rate = model.Rate,
                StoryLine = model.StoryLine,
                Poster = dataStream.ToArray()

            };

            _context.Movies.Add(movies);
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Movie Added Successfully!");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();

            var movie = await _context.Movies.FindAsync(id);

            if (movie == null) return NotFound();


            var viewModel = new MovieFormViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                GenreId = movie.GenreId,
                Rate = movie.Rate,
                Year = movie.Year,
                StoryLine = movie.StoryLine,
                Poster = movie.Poster,
                Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                return View(model);
            }
            var movie = await _context.Movies.FindAsync(model.Id);

            if (movie == null) return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var poster = files.FirstOrDefault();
                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);

                model.Poster = dataStream.ToArray();

                if (!allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Only JPG , PNG Alowed");
                    return View(model);
                }
                if (poster.Length > maxPosterSize)
                {
                    model.Genres = await _context.Genres.OrderBy(m => m.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Can't be More Than 1 MB");
                    return View(model);
                }


                movie.Poster = model.Poster;
            }







            movie.Title = model.Title;
            movie.GenreId = model.GenreId;
            movie.Year = model.Year;
            movie.Rate = model.Rate;
            movie.StoryLine = model.StoryLine;




            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Movie Updated Successfully!");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            //var movie = await _context.Movies.FindAsync(id);
            var movie = await _context.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();


            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok(); //not view  ok => return success in ajax
        }
    }
}
