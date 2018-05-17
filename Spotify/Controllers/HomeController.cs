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

            return View(new HomeIndexVM { GenreSeed = await repository.GetAvailableGenreSeeds(cache) });
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeIndexVM model)
        {
            //var response = await repository.SearchArtistsAsync(model.SearchResult, cache);
            var response = await repository.Search(cache, model.SearchParameters);

            HttpContext.Session.SetString("session", JsonConvert.SerializeObject(response));
            return RedirectToAction(nameof(About));
        }

        [HttpGet]
        public IActionResult About()
        {
            var searchResult = JsonConvert.DeserializeObject<SearchTrackResponce>(HttpContext.Session.GetString("session"));
            return View(new HomeAboutVM { Search = searchResult });
        }
    }
}
