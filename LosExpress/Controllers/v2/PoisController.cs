using System.Threading.Tasks;
using Btc.Swagger;
using LosExpress.Services;
using Btc.Web.Logger;
using Btc.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using LosExpress.Models.Bmw;
using LosExpress.Models.Skt;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace LosExpress.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("los/v{version:apiVersion}/[controller]")]
    [BtcSwaggerTag("Get POIs")]
    public class PoisController : Controller
    {
        //private readonly IPoiService _poiService;
        private readonly IBtcLogger<PoisController> _logger;
        private readonly IErrorResponseHelper _errorResponseHelper;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PoisController(
            //IPoiService poiService, 
            IErrorResponseHelper errorResponseHelper, 
            IBtcLogger<PoisController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
            )
        {
            //_poiService = poiService;
            _errorResponseHelper = errorResponseHelper;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        
        // GET: api/<controller>
        [HttpGet]
        public async Task<BmwLos> Get()
        {

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string query = "searchType=all&radius=0&reqCoordType=WGS84GEO&centerLon=126.9012528&centerLat=37.4341335&count=20&version=1&searchKeyword=스타벅스";// "foxpro87";
            var httpClient = this._httpClientFactory.CreateClient("SktLos");
            var jsonString = await httpClient.GetStringAsync($"/fts/pois?{query}");
            SktLos sktLos = JsonSerializer.Deserialize<SktLos>(jsonString, options);
            var bmwPois = new List<Models.Bmw.Pois>()
            {
                new Models.Bmw.Pois()
                {
                    Provider = _configuration.GetValue<string>("SKT:Provider"),
                    ProviderId = _configuration.GetValue<string>("SKT:ProviderId")
                }
            };
            BmwLos bmwLos = new BmwLos()
            {
                Status = 200,
                RequestId = 0,
                AsyncRequestId = null,
                Response = new Response()
                {
                    Pois = bmwPois,
                    Translations = new Translations()
                    {
                        PoisBusinessHoursHours = "Opening hours",
                        PoisDetailsChargingAuthentications = "Authentification methods",
                        PoisDetailsChargingFreeCharging = "Free charging",
                        PoisDetailsChargingGreenElectricity = "Green electricity",
                        PoisDetailsChargingHighPowerCharging = "High power charging",
                        PoisDetailsChargingLocation = "Location",
                        PoisDetailsChargingPowerConnectorsPlugType = "Power level",
                        PoisDetailsChargingReservable = "Reservable",
                        PoisDetailsChargingStationOperator = "Service provider",
                        PoisDetailsChargingTypeOfService = "Services",
                        PoisDetailsParkingAvailabilityStatus = "Availability",
                        PoisDetailsParkingFreeSpotsNumber = "Number of free spots",
                        PoisDetailsParkingPaymentMethods = "Payment methods",
                        PoisDetailsParkingPrices = "Prices",
                        PoisDetailsParkingRestrictions = "Restrictions",
                        PoisDetailsRefuelingPrices = "Fuel prices",
                        PoisDetailsRefuelingServices = "Services",
                        PoisDetailsRefuelingTypes = "Fuel type",
                        PoisProvider = "Source of data",
                    },
                    Sorting = new List<Sorting>()
                    {
                        new Sorting()
                        {
                            Name = "Relevance",
                            Identifier = 100,
                            Description = "Sort search results by: Relevance",
                            Enabled = true
                        },
                        new Sorting()
                        {
                            Name = "Distance",
                            Identifier = 101,
                            Description = "Sort search results by: Distance",
                            Enabled = false
                        },
                        new Sorting()
                        {
                            Name = "Name A-Z",
                            Identifier = 102,
                            Description = "Sort search results by: Name A-Z",
                            Enabled = false
                        },
                        new Sorting()
                        {
                            Name = "Category",
                            Identifier = 103,
                            Description = "Sort search results by: Category",
                            Enabled = false
                        },
                        new Sorting()
                        {
                            Name = "Price",
                            Identifier = 104,
                            Description = "Sort search results by: Price",
                            Enabled = false
                        },
                        new Sorting()
                        {
                            Name = "Fuel prices",
                            Identifier = 105,
                            Description = "Sort search results by: Fuel prices",
                            Enabled = false
                        },
                        new Sorting()
                        {
                            Name = "Rating",
                            Identifier = 106,
                            Description = "Sort search results by: Rating",
                            Enabled = false
                        }
                    },
                    Filters = new List<object>(),
                    Size = 0
                }
            };

            //return new string[] { "value1", "value2" };
            return bmwLos;
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
