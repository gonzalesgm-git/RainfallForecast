namespace RainfailForecast.API.Domain.Model
{

    public class Measure
    {

        public int Id { get; set; }

        public string Parameter { get; set; }

        public string ParameterName { get; set; }

        public int Period { get; set; }

        public string Qualifier { get; set; }

        public string UnitName { get; set; }
    }
}
