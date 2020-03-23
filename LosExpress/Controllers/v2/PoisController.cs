using System.Threading.Tasks;
using Btc.Swagger;
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
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using Microsoft.AspNetCore.Http;
using LosExpress.Models;

namespace LosExpress.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("los/v{version:apiVersion}/[controller]")]
    [BtcSwaggerTag("Get POIs")]
    public class PoisController : Controller
    {
        private readonly IBtcLogger<PoisController> _logger;
        private readonly IErrorResponseHelper _errorResponseHelper;
        private readonly IHttpClientFactory _httpClientFactory;
        public IConfiguration Configuration { get; set; }

        public PoisController(
            IErrorResponseHelper errorResponseHelper, 
            IBtcLogger<PoisController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration
            )
        {
            _errorResponseHelper = errorResponseHelper;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            Configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoisAsync([FromQuery] BmwLosRequest request)
        {
            var logProperties = new Dictionary<string, string> { ["request"] = JsonSerializer.Serialize(request) };
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            try
            {

                if (request == null || request.Query == null || request.Latitudes == null || request.Longitudes == null)
                {
                    _logger.LogWarn("LOS POIs controller invalid request", logProperties);
                    return BadRequest(new ErrorMessage { ErrorCode = StatusCodes.Status400BadRequest, Message = "Invalid Request Parameter" });
                }

                var queryParams = new Dictionary<string, string>()
                {
                    //{ <sktLosRequest key>, <bmwLosRequet value> }
                    {"searchType", "all"},
                    {"searchKeyword", request.Query},
                    {"centerLat", request.Latitudes},
                    {"centerLon", request.Longitudes},
                    {"radius", request.Distance},
                    {"reqCoordType", "WGS84GEO"},
                    //{"count", "20"},
                    //{"version", "1"},
                };
                var noEmptyQueryParams = (from kv in queryParams where kv.Value != null select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
                var url = QueryHelpers.AddQueryString(Configuration.GetValue<string>("SKT:PoisPath"), noEmptyQueryParams);
                var httpClient = this._httpClientFactory.CreateClient("SktLos");
                var jsonString = await httpClient.GetStringAsync(url);
                SktLos sktLos = JsonSerializer.Deserialize<SktLos>(jsonString, options);
            
                var bmwPois = new List<Models.Bmw.Pois>();
                foreach (var sktPoi in sktLos.SearchPoiInfo.Pois.Poi)
                {
                    var bmwPoi = new Models.Bmw.Pois()
                    {
                        Provider = Configuration.GetValue<string>("SKT:Provider"),
                        ProviderId = Configuration.GetValue<string>("SKT:ProviderId"),
                        ProviderPOIid = sktPoi.Id,
                        Id = -1,
                        Name = sktPoi.Name,
                        Address = new Models.Bmw.Address()
                        {
                            Street = sktPoi.RoadName,
                            IntersectingStreets = new List<object> { },
                            HouseNumber = sktPoi.FirstBuildNo,
                            //"postalCode": "80538",

                            //                postalCode = sktPostalCodeResponse != null
                            //                            && sktPostalCodeResponse.CoordinateInfo != null
                            //                            && sktPostalCodeResponse.CoordinateInfo.Coordinate != null ? sktPostalCodeResponse.CoordinateInfo.Coordinate.First().Zipcode : string.Empty
                            City = sktPoi.MiddleAddrName,
                            Country = Configuration.GetValue<string>("SKT:Country"),
                            CountryCode = Configuration.GetValue<string>("SKT:CountryCode"),
                            Region = sktPoi.UpperAddrName,
                            RegionCode = null,
                            Settlement = null

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
                        Distance = Convert.ToDouble(sktPoi.Radius), // sktPoi.Radius unit : Km
                        TravelTime = null,
                        BusinessHours = new BusinessHours()
                        {
                            Hours = " Mon-Sun: 09:00 - 17:00",
                            Label = "Opening hours",
                            Status = "C",
                            StatusLabel = null,
                            Compressed = null,
                            Structured = new List<Structured>()
                            {
                                new Structured()
                                {
                                    Start = "T090000",
                                    Duration = "PT08H00M",
                                    Recurrence = "FREQ:DAILY; BYDAY: MO,TU,WE,TH,FR,SA,SU"
                                }
                            }
                        },
                        Attributes = new List<object>() { },
                        Categories = new List<Category>()
                        {
                            new Category()
                            {
                                Id = null,
                                VehicleCategoryId = 21070,
                                Hierarchy = new List<object>() { },
                                RealTimeCategoryId = null,
                                ProviderCategories = new List<object>() { }
                            }
                        },
                        Details = new Details()
                        {
                            Parking = null,
                            Refueling = null,
                            Charging = null,
                            Dealer = null,
                            Online = new Online()
                            {
                                IsPOI = true,
                                ChainIds = new List<int>() { },
                                Vicinity = $"{sktPoi.UpperAddrName} {sktPoi.MiddleAddrName} {sktPoi.LowerAddrName}",
                                Contacts = new List<Contact>
                                {
                                    new Contact()
                                    {
                                        Name = "Phone",
                                        Label = "Phone",
                                        Value = sktPoi.TelNo
                                    },
                                    new Contact()
                                    {
                                        Name = "Website",
                                        Label = "Website",
                                        Value = "http://www.deutsches-museum.de/en"
                                    },
                                },
                                ReviewSummary = new ReviewSummary
                                {
                                    Provider = "Yelp",
                                    CiVersion = "V1",
                                    AverageRating = 4.0,
                                    ReviewCount = 96,
                                    RatingIconId = "40",
                                    PriceRange = ""
                                },
                                Reviews = new List<object>() { }
                            },
                            Phonemes = null,
                            Common = new Common()
                            {
                                Announcements = null,
                                Image = "https://s3-media0.fl.yelpcdn.com/bphoto/-pz39cRUDEbHFb41dTYbeQ/o.jpg",
                                RawAdditions = null,
                                BoundingBox = null,
                                PriceLabel = null
                            },
                            ChildrenPois = new List<object>() { }
                        }
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

                return Ok(bmwLos);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "get-los-express-pois-error", logProperties);
                return _errorResponseHelper.InternalServerError("get-los-express-pois-error", "Error while retrieving pois");
            }

        }
    }
}
