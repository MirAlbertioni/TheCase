using Fac.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Fac
{
    public class ServiceResponse<T>
    {
        public bool Faulted { get; set; }
        public string Message { get; set; }
        public ServiceResponseStatus Status { get; set; }
        public T Response { get; set; }

        public ServiceResponse(HttpStatusCode code, string message = null)
        {
            Status = HttpToStatus(code);
            Faulted = !string.IsNullOrEmpty(message);
            Message = message;
        }


        private ServiceResponseStatus HttpToStatus(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    return ServiceResponseStatus.Ok;
                case HttpStatusCode.ServiceUnavailable:
                    return ServiceResponseStatus.ServiceUnavailable;
                case HttpStatusCode.Gone:
                    return ServiceResponseStatus.NoNetwork;
                case HttpStatusCode.BadRequest:
                    return ServiceResponseStatus.InvalidLicense;
                case HttpStatusCode.RequestTimeout:
                    return ServiceResponseStatus.Timeout;
                case HttpStatusCode.InternalServerError:
                    return ServiceResponseStatus.InternalServerError;
                case HttpStatusCode.NotFound:
                    return ServiceResponseStatus.InvalidUrl;
                default:
                    //Log.Warning("An unknown http status ({0}) is mapped to 'NotSet'", code);
                    return ServiceResponseStatus.NotSet;
            }
        }
    }

    public enum ServiceResponseStatus
    {
        NotSet = 0,
        ServiceUnavailable = 1,
        NoNetwork = 2,
        InvalidLicense = 3,
        Timeout = 4,
        InternalServerError = 5,
        InvalidUrl = 6,
        Ok = 10,
    }
}
