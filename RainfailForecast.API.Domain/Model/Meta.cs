using System.Runtime.Serialization;

namespace RainfailForecast.API.Domain.Model
{
    [DataContract]
    public class Meta
    {
        [DataMember(Name = "publisher")]
        public string Publisher { get; set; }

        [DataMember(Name = "documentation")]
        public string Documentation { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "hasFormat")]
        public string[] HasFormat { get; set; }

        [DataMember(Name = "licence")]
        public string License { get; set; }
    }
}
