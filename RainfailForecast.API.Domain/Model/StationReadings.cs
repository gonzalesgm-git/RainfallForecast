namespace RainfailForecast.API.Domain.Model
{
    public class StationReadings
    {
        public Meta Meta { get; set; }
        public LatestReading[] Items { get; set; }
    }
}
