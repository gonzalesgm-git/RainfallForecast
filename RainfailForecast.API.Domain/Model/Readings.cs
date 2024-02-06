namespace RainfailForecast.API.Domain.Model
{
    public class Readings
    {
        public Meta Meta { get; set; }
        public RainfallInfo[] Items { get; set; }
    }
}
