using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMovieDB.Models;
using ZMovieDB.Data;

namespace ZMovieDB.Controllers;

public class HomeController : Controller
{
    private MovieContext context { get; set; }

    public HomeController(MovieContext ctx) => context = ctx;

    public IActionResult Index()
    {
        var movies = context.Movie
            .Include(m => m.Genre)
            .OrderBy(m => m.Name)
            .ToList();

        return View(movies);
    }
}
