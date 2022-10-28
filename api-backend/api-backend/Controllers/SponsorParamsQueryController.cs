using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MazedDB.Data;
using MazedDB.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_backend.Controllers
{
    public class SponsorQueryParamsController : Controller
    {
        private readonly MazedDBContext _context;
        static HttpClient client = new();

        public SponsorQueryParamsController(MazedDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetSongArtist")]
        public async Task<string?> GetSongArtist(string term, string media)
        {
            string? product = null;
            NameValueCollection query= System.Web.HttpUtility.ParseQueryString(String.Empty);
            query.Add("term", term);
            query.Add("media", media);

            string queryString = SponsQueryParam.iTunes_url +  "/search?" + query.ToString();

            HttpResponseMessage response = await client.GetAsync(queryString);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
            }

            return product;
        }
    }
}

