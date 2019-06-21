using RestSharp.Deserializers;

namespace Orc.Interlink
{
    public class LoginResponse
    {
        [DeserializeAs(Name = "geoSession")]
        public string SessionId { get; set; }
    }
}