using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SkatBowlingOpgave.DataModels;

namespace SkatBowlingOpgave.Consume
{
    public class RestApiConsume
    {
        readonly string baseUri = "http://95.85.62.55/api/";
        private string token;
        public async Task<Points> GetPointsAsync()
        {
            string uri = baseUri + "points";
            HttpClient client = new HttpClient();
            var content = await client.GetAsync(uri);
            if (content.IsSuccessStatusCode)
            {
                Points points = JsonConvert.DeserializeObject<Points>(await content.Content.ReadAsStringAsync());
                ServicePoint(uri);
                return points;
            }
            return null;
        }

        public async Task<string> PostPointsAsync( PointResultater pointResultater, string token )
        {
            string uri = baseUri + "points";
            int[] points = pointResultater.pointResultater;
            string jsonString = JsonConvert.SerializeObject(new { token, points });
            StringContent stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.PostAsync(uri, stringContent);
            string success = await response.Content.ReadAsStringAsync();
            if (success.Contains("true"))
            {
                ServicePoint(uri);
                return "Resultat uploadet succesfuldt";
            }
            return null;
        }

        private void ServicePoint(string uri)
        {
            var servicePoint = ServicePointManager.FindServicePoint(new Uri(uri));
            servicePoint.ConnectionLeaseTimeout = 60 * 1000;
        }
    }
}