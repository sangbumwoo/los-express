using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosExpress.Models
{
    public class GroupSubList
    {
        public string SubSeq { get; set; }
        public string SubName { get; set; }
        public string SubNavX { get; set; }
        public string SubNavY { get; set; }
        public string SubRpFlag { get; set; }
        public string SubPoiId { get; set; }
        public string SubNavSeq { get; set; }
        public string SubParkYn { get; set; }
        public string SubNavWgsX { get; set; }
        public string SubNavWgsY { get; set; }
        public string SubCatId { get; set; }
        public string SubUpperBizName { get; set; }
        public string SubMiddleBizName { get; set; }
        public string SubLowerBizName { get; set; }
        public string SubDetailBizName { get; set; }
    }

    public class Poi
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TelNo { get; set; }
        public string FrontLat { get; set; }
        public string FrontLon { get; set; }
        public string NoorLat { get; set; }
        public string NoorLon { get; set; }
        public string UpperAddrName { get; set; }
        public string MiddleAddrName { get; set; }
        public string LowerAddrName { get; set; }
        public string DetailAddrName { get; set; }
        public string MlClass { get; set; }
        public string FirstNo { get; set; }
        public string SecondNo { get; set; }
        public string RoadName { get; set; }
        public string FirstBuildNo { get; set; }
        public string SecondBuildNo { get; set; }
        public string Radius { get; set; }
        public string CatId { get; set; }
        public string BizName { get; set; }
        public string UpperBizName { get; set; }
        public string MiddleBizName { get; set; }
        public string LowerBizName { get; set; }
        public string DetailBizName { get; set; }
        public string RpFlag { get; set; }
        public string ParkFlag { get; set; }
        public string DetailInfoFlag { get; set; }
        public string Desc { get; set; }
        public IList<GroupSubList> GroupSubLists { get; set; }
    }

    public class Pois
    {
        public IList<Poi> Poi { get; set; }
    }

    public class SearchPoiInfo
    {
        public string TotalCount { get; set; }
        public string Count { get; set; }
        public string Page { get; set; }
        public Pois Pois { get; set; }
    }

    public class SktLos
    {
        public SearchPoiInfo SearchPoiInfo { get; set; }
    }


}
