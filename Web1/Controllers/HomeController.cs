using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Microsoft.AspNetCore.Authorization;

namespace Movies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IWebHostEnvironment webHostEnvironment;


    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;

        this.webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

      public IActionResult AddMovie()
    {

        List<MoviesModel> Movies = new List<MoviesModel>();
        using (var db = new MovieContext())
        {
            Movies = db.Movies.ToList();
        }
        TempData["Movies"] = Movies;
        return View("AddMovie");
    }

    [HttpPost]
    public IActionResult AddMovieForm(MoviesModel movie)
    {

        using (var db = new MovieContext())
        {


            // Add file to wwwroot/images/
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string fileName = movie.ImageFile.FileName;
            string path = Path.Combine(wwwrootPath, "images", fileName);
            movie.ImageName = fileName;
            movie.ImageFile.CopyTo(new FileStream(path, FileMode.Create));

            db.Add(movie);
            db.SaveChanges();
        }
        return RedirectToAction("Addmovie");


    }

    public IActionResult Movies()
    {
        List<MoviesModel> Movies = new List<MoviesModel>();
        using (var db = new MovieContext())
        {
            Movies = db.Movies.ToList();
        }
        TempData["Movies"] = Movies;
        return View("Movies");
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteMovie(MoviesModel Movie) {
            using (var db = new MovieContext()) {
                var Currentmovie = db.Movies.Where(m => m.Id == Movie.Id).FirstOrDefault();
                if(Currentmovie != null) {
                    db.Movies.Remove(Currentmovie);
                    db.SaveChanges();
                }
                return RedirectToAction("Movies");
            }

    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Editmovie(MoviesModel Movie)
    {
        using (var db = new MovieContext())
        {
            var movieEdit = db.Movies.Where(u => u.Id == Movie.Id).FirstOrDefault();

            TempData["movieEdit"] = movieEdit;           
        }
        return View();
    }

    public IActionResult EditmovieForm(MoviesModel movie) {
        using (var db = new MovieContext()) {
            var Editmovie = db.Movies.Where(u => u.Id == movie.Id).FirstOrDefault();
            if(Editmovie != null) {
                Editmovie.Title = movie.Title;
                Editmovie.Description = movie.Description;
                Editmovie.Genre = movie.Genre;
                
                // Add file to wwwroot/images/
                string wwwrootPath = webHostEnvironment.WebRootPath;
                string fileName = movie.ImageFile.FileName;
                string path = Path.Combine(wwwrootPath, "images", fileName);
                Editmovie.ImageName = fileName;
                movie.ImageFile.CopyTo(new FileStream(path, FileMode.Create));

                db.Update(Editmovie);
                db.SaveChanges();

                return RedirectToAction("Movies");
            }
        }
        return RedirectToAction("Movies");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
