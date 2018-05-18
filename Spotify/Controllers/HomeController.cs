using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Spotify.Models;
using Spotify.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Spotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpotifyApiClient repository;
        private readonly IMemoryCache cache;

        public HomeController(SpotifyApiClient repository, IMemoryCache cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var seeds = await repository.GetAvailableGenreSeeds(cache);
            HttpContext.Session.SetString("seeds", JsonConvert.SerializeObject(seeds));
            return View(new HomeIndexVM { GenreSeed = seeds });
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexVM model)
        {
            if (model.SearchParameters.Text == null && model.SearchParameters.Genre == null ||( model.SearchParameters.Genre != null ? model.SearchParameters.Genre.Count > 4 : false ))
            {
                var seeds = JsonConvert.DeserializeObject<GenreResponse>(HttpContext.Session.GetString("seeds"));
                model.GenreSeed = seeds;
                return View(model);
            }

            var response = await repository.Search(cache, model.SearchParameters, 99);

            HttpContext.Session.SetString("session", response);
            return RedirectToAction(nameof(SearchResult));
        }

        [HttpGet]
        public IActionResult SearchResult()
        {
            var searchResult = JsonConvert.DeserializeObject<SearchTrackResponce>(HttpContext.Session.GetString("session"));
            return View(new HomeAboutVM { Search = searchResult });
        }
    }
}
