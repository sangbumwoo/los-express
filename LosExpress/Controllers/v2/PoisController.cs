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
using System;

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
            //var sktPoiList = sktLos.SearchPoiInfo.Pois.Poi;

            var bmwPois = new List<Models.Bmw.Pois>();
            foreach (var sktPoi in sktLos.SearchPoiInfo.Pois.Poi)
            {
                var bmwPoi = new Models.Bmw.Pois()
                {
                    Provider = _configuration.GetValue<string>("SKT:Provider"),
                    ProviderId = _configuration.GetValue<string>("SKT:ProviderId"),
                    ProviderPOIid = sktPoi.Id,
                    Id = -1,
                    Name = sktPoi.Name,
                    Address = new Models.Bmw.Address() {
                        Street = "TODO",
                        //    "street": "Museumsinsel",
                        //"intersectingStreets": [],
                        //"houseNumber": "1",
                        //"postalCode": "80538",
                        //"city": "Munich",
                        //"country": "Germany",
                        //"countryCode": "DEU",
                        //"region": "Bavaria",
                        //"regionCode": null,
                        //"settlement": null
                    },
                    Latitude = Convert.ToDouble(sktPoi.NoorLat),
                    Longitude = Convert.ToDouble(sktPoi.NoorLon),
                    Entrances = new List<Entrance>()
                    {
                        new Entrance()
                        {
                            Latitude = Convert.ToDouble(sktPoi.FrontLat),
                            Longitude = Convert.ToDouble(sktPoi.FrontLon),
                            Clearance = null,
                            Name = null
                        }
                    },
                    Exits = new List<object>(),




/*
                "provider": "Here",
                "providerId": "75",
                "providerPOIid": "276u281z-da9f60bd0ba14ba7a1fbfb6afe13b2c9",
                "id": -1,
                "name": "Deutsches Museum",
                "address": {
                    "street": "Museumsinsel",
                    "intersectingStreets": [],
                    "houseNumber": "1",
                    "postalCode": "80538",
                    "city": "Munich",
                    "country": "Germany",
                    "countryCode": "DEU",
                    "region": "Bavaria",
                    "regionCode": null,
                    "settlement": null
                },
                "latitude": 48.13034,
                "longitude": 11.58405,
                "entrances": [
                    {
                        "name": null,
                        "clearance": null,
                        "latitude": 48.13056,
                        "longitude": 11.58337
                    }
                ],
                "exits": [],
                "distance": 3.607,
                "travelTime": null,
                "businessHours": {
                    "hours": "Mon-Sun: 09:00 - 17:00",
                    "label": "Opening hours",
                    "status": "C",
                    "statusLabel": null,
                    "compressed": null,
                    "structured": [
                        {
                            "start": "T090000",
                            "duration": "PT08H00M",
                            "recurrence": "FREQ:DAILY;BYDAY:MO,TU,WE,TH,FR,SA,SU"
                        }
                    ]
                },
                "attributes": [],
                "categories": [
                    {
                        "id": null,
                        "vehicleCategoryId": 21070,
                        "hierarchy": [],
                        "realTimeCategoryId": null,
                        "providerCategories": []
                    }
                ],
                "details": {
                    "parking": null,
                    "refueling": null,
                    "charging": null,
                    "dealer": null,
                    "online": {
                        "isPOI": true,
                        "chainIds": [],
                        "vicinity": "Museumsinsel 1\n80538 Munich",
                        "contacts": [
                            {
                                "name": "Phone",
                                "label": "Phone",
                                "value": "+498921791"
                            },
                            {
                                "name": "Website",
                                "label": "Website",
                                "value": "http://www.deutsches-museum.de/en"
                            }
                        ],
                        "reviewSummary": {
                            "provider": "Yelp",
                            "ciVersion": "V1",
                            "averageRating": 4.0,
                            "reviewCount": 96,
                            "ratingIconId": "40",
                            "priceRange": ""
                        },
                        "reviews": []
                    },
                    "phonemes": null,
                    "common": {
                        "announcements": null,
                        "image": "https://s3-media0.fl.yelpcdn.com/bphoto/-pz39cRUDEbHFb41dTYbeQ/o.jpg",
                        "rawAdditions": null,
                        "boundingBox": null,
                        "priceLabel": null
                    },
                    "childrenPois": []
*/

                };
                bmwPois.Add(bmwPoi);
            }
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
