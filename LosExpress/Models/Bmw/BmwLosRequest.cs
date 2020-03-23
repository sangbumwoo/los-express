using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LosExpress.Models.Bmw
{
    public class BmwLosRequest
    {
        [DefaultValue("BMW")]
        public string Brand { get; set; }
        public string Market { get; set; }
        public string Locale { get; set; }
        public string QueryGeometry { get; set; }
        [DefaultValue("스타벅스")]
        public string Query { get; set; }
        [DefaultValue("37.4341335")]
        public string Latitudes { get; set; }
        [DefaultValue("126.9012528")]
        public string Longitudes { get; set; }
        public string MatchMode { get; set; }
        public string ResponseAttributes { get; set; }
        public string Distance { get; set; }
        public string DistanceUnit { get; set; }
        public string Drivetrain { get; set; }
        public string MaxResultSize { get; set; }
        public string CompressAkinAttributes { get; set; }
        public string OnlyOpened { get; set; }
        public string ExpandPrefix { get; set; }
        public string RawData { get; set; }
        public string Translations { get; set; }
    }
}
