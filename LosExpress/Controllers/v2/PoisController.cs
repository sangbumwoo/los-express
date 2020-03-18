using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btc.Swagger;
using LosExpress.Models;
using LosExpress.Services;
using LosExpress.ViewModels;
using Btc.Web.Logger;
using Btc.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
//using Microsoft.AspNetCore.Components;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LosExpress.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("los/v{version:apiVersion}/[controller]")]
    [BtcSwaggerTag("Get POIs")]
    public class PoisController : Controller
    {
        private readonly IPoiService _poiService;
        private readonly IBtcLogger<PoisController> _logger;
        private readonly IErrorResponseHelper _errorResponseHelper;

        //[Inject]
        //public IHttpClientFactory HttpClientFactory { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;

        public PoisController(IPoiService poiService, 
            IErrorResponseHelper errorResponseHelper, 
            IBtcLogger<PoisController> logger,
            IHttpClientFactory httpClientFactory)
        {
            _poiService = poiService;
            _errorResponseHelper = errorResponseHelper;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        
        // GET: api/<controller>
        [HttpGet]
        public async Task<string> Get()
        {
            string query = "searchType=all&radius=0&reqCoordType=WGS84GEO&centerLon=126.9012528&centerLat=37.4341335&count=20&version=1&searchKeyword=스타벅스";// "foxpro87";
            var httpClient = this._httpClientFactory.CreateClient("GitHub");
            var accountInfo = await httpClient.GetStringAsync($"/fts/pois?{query}");

            Console.WriteLine("###################################test###");
            //return new string[] { "value1", "value2" };
            return null;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
