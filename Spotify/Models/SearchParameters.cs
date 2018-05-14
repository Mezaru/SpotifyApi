using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Models
{
    public class SearchParameters
    {
        public string Genre { get; set; }
        public string Text { get; set; }
        public int? Popularity { get; set; }
        public double? Valence { get; set; }
        public string Type { get; set; }
    }
}
