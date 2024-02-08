using System.Runtime.Serialization;

namespace RainfailForecast.API.Domain.Model
{
    [DataContract]
    public class RainfallInfo
    {
        [DataMember(Name = "@id")]
        public string Id { get; set; }

        [DataMember(Name = "easting")]
        public int Easting { get; set; }

        [DataMember(Name = "gridReference")]
        public string GridReference { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "lat")]
        public double Lat { get; set; }
        [DataMember(Name = "long")]
        public double Long { get; set; }

        [DataMember(Name = "northing")]
        public int Northing { get; set; }
        [DataMember(Name = "notation")]
        public string Notation { get; set; }
        [DataMember(Name = "stationReference")]
        public string StationReference { get; set; }
        [DataMember(Name = "measures")]
        public Measure[] Measures { get; set; }
    }
}
