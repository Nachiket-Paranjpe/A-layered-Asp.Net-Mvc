using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Mvc_App.Helper
{
   public class ApiServiceClient : IDisposable
    {
        //The URL of the WEB API Service
        // private readonly string _iisExpressUrl = "http://localhost:49605/";

        private readonly string _iisUrl = "http://localhost/ServiceLayer/";
        private HttpClient _client { get; set; }    
        protected bool Disposed { get; set; } = false;       
        internal ApiServiceClient()
        { }
        public HttpClient ServiceClient
        {
            get {
                _client = new HttpClient { BaseAddress = new Uri(_iisUrl) };
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
              //  _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/bson"));

                return _client;
            }
        }
        internal virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                    _client.Dispose();
            }
            this.Disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public sealed class Servicesingleton
    {
        private static readonly ApiServiceClient _instance = new ApiServiceClient();
        private Servicesingleton() { } 

        public static HttpClient Instance => _instance.ServiceClient;
    }
}