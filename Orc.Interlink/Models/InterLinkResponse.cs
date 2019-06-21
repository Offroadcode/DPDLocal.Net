using System.Net;

namespace Orc.Interlink
{
    public class InterLinkResponse<Model>
    {
        public Model Data { get; set; }
        public HttpStatusCode ServerResponseCode { get; set; }
        public string RawResponse { get; set; }
    }
}