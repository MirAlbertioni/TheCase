using System.Net;

namespace Fac.Entities
{
    public class ResponseBase
    {
        public string Status { get; set; }
        public string Description { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}