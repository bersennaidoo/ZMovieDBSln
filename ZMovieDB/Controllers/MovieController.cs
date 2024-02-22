using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZMovieDB.Data;
using ZMovieDB.Models;

namespace ZMovieDB.Controllers
{
    public class MovieController : Controller
    {
        private MovieContext context { get; set; }

        public MovieController(MovieContext ctx) => context = ctx;

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Movie());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Action = "Edit";
            var movie = context.Movie.Find(id);

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0)
                    context.Movie.Add(movie);
                else
                    context.Movie.Update(movie);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
             }
             else
             {
                 ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";

                 return View(movie);
             }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var movie = context.Movie.Find(id);

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Movie movie)
        {
            context.Movie.Remove(movie);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
