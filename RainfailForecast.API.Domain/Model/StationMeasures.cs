namespace RainfailForecast.API.Domain.Model
{
    public class StationMeasures
    {
        public Meta Meta { get; set; }
        public StationRainfallInfo[] Items { get; set; }
    }
}
