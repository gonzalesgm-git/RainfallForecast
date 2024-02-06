namespace RainfailForecast.API.Domain.Model
{
    public class RainfallInfo
    {
        public int Id { get; set; }

        public int Easting { get; set; }

        public string GridReference { get; set; }

        public string Label { get; set; }

        public double Lat { get; set; }
        public double Long { get; set; }

        public int Northing { get; set; }
        public string Notation { get; set; }
        public string StationReference { get; set; }

        public Measure[] Measures { get; set; }
    }
}
