namespace RainfailForecast.API.Domain.Model
{
    public class StationRainfallInfo : RainfallInfo
    {
        public LatestReading LatestReading { get; set; }
        public string ValueType { get; set; }
    }
}
