using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LosExpress.Models.Bmw
{
    public class Address
    {
        public string Street { get; set; }
        public IList<object> IntersectingStreets { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public object RegionCode { get; set; }
        public object Settlement { get; set; }
    }

    public class Entrance
    {
        public object Name { get; set; }
        public object Clearance { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Structured
    {
        public string Start { get; set; }
        public string Duration { get; set; }
        public string Recurrence { get; set; }
    }

    public class BusinessHours
    {
        public string Hours { get; set; }
        public string Label { get; set; }
        public string Status { get; set; }
        public object StatusLabel { get; set; }
        public object Compressed { get; set; }
        public IList<Structured> Structured { get; set; }
    }

    public class Category
    {
        public object Id { get; set; }
        public int VehicleCategoryId { get; set; }
        public IList<object> Hierarchy { get; set; }
        public object RealTimeCategoryId { get; set; }
        public IList<object> ProviderCategories { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public class ReviewSummary
    {
        public string Provider { get; set; }
        public string CiVersion { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public string RatingIconId { get; set; }
        public string PriceRange { get; set; }
    }

    public class Online
    {
        public bool IsPOI { get; set; }
        public IList<int> ChainIds { get; set; }
        public string Vicinity { get; set; }
        public IList<Contact> Contacts { get; set; }
        public ReviewSummary ReviewSummary { get; set; }
        public IList<object> Reviews { get; set; }
    }

    public class Common
    {
        public object Announcements { get; set; }
        public string Image { get; set; }
        public object RawAdditions { get; set; }
        public object BoundingBox { get; set; }
        public object PriceLabel { get; set; }
    }

    public class Details
    {
        public object Parking { get; set; }
        public object Refueling { get; set; }
        public object Charging { get; set; }
        public object Dealer { get; set; }
        public Online Online { get; set; }
        public object Phonemes { get; set; }
        public Common Common { get; set; }
        public IList<object> ChildrenPois { get; set; }
    }

    public class Pois
    {
        public string Provider { get; set; }
        public string ProviderId { get; set; }
        public string ProviderPOIid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IList<Entrance> Entrances { get; set; }
        public IList<object> Exits { get; set; }
        public double Distance { get; set; }
        public object TravelTime { get; set; }
        public BusinessHours BusinessHours { get; set; }
        public IList<object> Attributes { get; set; }
        public IList<Category> Categories { get; set; }
        public Details Details { get; set; }
    }

    public class Translations
    {
        [JsonPropertyName("pois.businessHours.hours")]
        public string PoisBusinessHoursHours { get; set; }
        [JsonPropertyName("pois.details.charging.authentications")]
        public string PoisDetailsChargingAuthentications { get; set; }
        [JsonPropertyName("pois.details.charging.freeCharging")]
        public string PoisDetailsChargingFreeCharging { get; set; }
        [JsonPropertyName("pois.details.charging.greenElectricity")]
        public string PoisDetailsChargingGreenElectricity { get; set; }
        [JsonPropertyName("pois.details.charging.highPowerCharging")]
        public string PoisDetailsChargingHighPowerCharging { get; set; }
        [JsonPropertyName("pois.details.charging.location")]
        public string PoisDetailsChargingLocation { get; set; }
        [JsonPropertyName("pois.details.charging.power.connectors.plugType")]
        public string PoisDetailsChargingPowerConnectorsPlugType { get; set; }
        [JsonPropertyName("pois.details.charging.reservable")]
        public string PoisDetailsChargingReservable { get; set; }
        [JsonPropertyName("pois.details.charging.stationOperator")]
        public string PoisDetailsChargingStationOperator { get; set; }
        [JsonPropertyName("pois.details.charging.typeOfService")]
        public string PoisDetailsChargingTypeOfService { get; set; }
        [JsonPropertyName("pois.details.parking.availabilityStatus")]
        public string PoisDetailsParkingAvailabilityStatus { get; set; }
        [JsonPropertyName("pois.details.parking.freeSpotsNumber")]
        public string PoisDetailsParkingFreeSpotsNumber { get; set; }
        [JsonPropertyName("pois.details.parking.paymentMethods")]
        public string PoisDetailsParkingPaymentMethods { get; set; }
        [JsonPropertyName("pois.details.parking.prices")]
        public string PoisDetailsParkingPrices { get; set; }
        [JsonPropertyName("pois.details.parking.restrictions")]
        public string PoisDetailsParkingRestrictions { get; set; }
        [JsonPropertyName("pois.details.refueling.prices")]
        public string PoisDetailsRefuelingPrices { get; set; }
        [JsonPropertyName("pois.details.refueling.services")]
        public string PoisDetailsRefuelingServices { get; set; }
        [JsonPropertyName("pois.details.refueling.types")]
        public string PoisDetailsRefuelingTypes { get; set; }
        [JsonPropertyName("pois.provider")]
        public string PoisProvider { get; set; }
    }

    public class Sorting
    {
        public string Name { get; set; }
        public int Identifier { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }

    public class Response
    {
        public IList<Pois> Pois { get; set; }
        public Translations Translations { get; set; }
        public IList<Sorting> Sorting { get; set; }
        public IList<object> Filters { get; set; }
        public int Size { get; set; }
    }

    public class BmwLos
    {
        public int Status { get; set; }
        public int RequestId { get; set; }
        public object AsyncRequestId { get; set; }
        public Response Response { get; set; }
    }


}
