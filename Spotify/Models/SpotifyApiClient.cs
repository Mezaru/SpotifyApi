using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Flurl;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Collections.Generic;

namespace Spotify.Models
{
    public class SpotifyApiClient
    {
        private const string ClientId = "996d0037680544c987287a9b0470fdbb";
        private const string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

        protected const string BaseUrl = "https://api.spotify.com/";
        private HttpClient GetDefaultClient(IMemoryCache cache)
        {
            var authHandler = new SpotifyAuthClientCredentialsHttpMessageHandler(
                ClientId,
                ClientSecret,
                new HttpClientHandler(), cache);

            var client = new HttpClient(authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            return client;
        }

        internal async Task<GenreResponse> GetAvailableGenreSeeds(IMemoryCache cache)
        {
            var client = GetDefaultClient(cache);
            var url = new Url("/v1/recommendations/available-genre-seeds");

            var response = await client.GetStringAsync(url);
            var genreResponse = JsonConvert.DeserializeObject<GenreResponse>(response);


            return genreResponse;

        }

        public async Task<string> SearchArtistsAsync(string artistName, IMemoryCache cache, int? limit = null, int? offset = null)
        {
            var client = GetDefaultClient(cache);

            var url = new Url("/v1/search");
            url = url.SetQueryParam("q", artistName);
            url = url.SetQueryParam("type", "artist");

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            if (offset != null)
                url = url.SetQueryParam("offset", offset);

            var response = await client.GetStringAsync(url);
            return response;
            //var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);
            //return artistResponse;
        }

        public async Task<string> Search(IMemoryCache cache, SearchParameters parameters, int? limit = null, int? offset = null)
        {
            var client = GetDefaultClient(cache);
            var url = new Url("/v1/recommendations");
            if (parameters.Genre != null)
                url = url.SetQueryParam("seed_genres", parameters.Genre);

            if (parameters.Popularity != null)
                url = url.SetQueryParam("target_popularity", parameters.Popularity);

            if (parameters.Text != null)
            {
                var searchArtist = await SearchArtistsAsync(parameters.Text, cache, 1);
                var artist = JsonConvert.DeserializeObject<SearchArtistResponse>(searchArtist);
                url = url.SetQueryParam("seed_artists", artist.Artists.Items.First().Id);
            }
            if (parameters.Valence != null)
            {
                var valence = parameters.Valence / 100;
                url = url.SetQueryParam("target_valence", valence);
            }

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            if (offset != null)
                url = url.SetQueryParam("offset", offset);

            var response = await client.GetStringAsync(url);

            return response;
        }
    }
}
