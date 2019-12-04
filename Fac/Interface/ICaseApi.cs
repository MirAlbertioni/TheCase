using Fac.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Fac.Interface
{
    public interface ICaseApi
    {
        [Post("/api/CaseController/AddCase/{testing}")]
        Task<CaseResponse> AddCase([Body] string testing);

        [Get("/api/CaseController/Get")]
        Task<HttpResponseMessage> Get();
    }

    public class CaseHttpClient
    {
        public static HttpClient New(string url, Dictionary<string, string> headers)
        {
            return new HttpClient(new CaseApiHandler(headers))
            {
                BaseAddress = new Uri(url)
            };
        }
    }

    public class CaseApiHandler : HttpClientHandler
    {
        private readonly Dictionary<string, string> headers;
        //private readonly ILogger log;

        public CaseApiHandler(Dictionary<string, string> headers)
        {
            this.headers = headers;
            //log = Log.ForContext<CaseApiHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            var msg = $"API CALL : {request.Method.Method}/{request.RequestUri.AbsolutePath}";
            //log.Information(msg);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}