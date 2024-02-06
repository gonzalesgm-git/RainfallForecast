namespace RainfailForecast.API.Domain.Model
{
    public class LatestReading
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string DateTime { get; set; }
        public string Measure { get; set; }
        public double Value { get; set; }

    }
}
