using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recipe9.Models;
using System.Data;

namespace Recipe9.Controllers
{
    public class HomeController : Controller
    {
        private EFRecipesEntities context = new EFRecipesEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(context.Movies.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            var movie = context.Movies.Single(m => m.MovieId == id);
            return View(movie);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "MovieId")] Movie movie)
        {
            try
            {
                context.Movies.AddObject(movie);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5
 
        public ActionResult Edit(int id)
        {
            var movie = context.Movies.Single(m => m.MovieId == id);
            return View(movie);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Movie movie)
        {
            try
            {
                movie.MovieId = id;
                context.Movies.Attach(movie);
                context.ObjectStateManager.ChangeObjectState(movie, EntityState.Modified);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id)
        {
            try
            {
                var movie = new Movie { MovieId = id };
                context.Movies.Attach(movie);
                context.Movies.DeleteObject(movie);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
