using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using FlickrApiDemo.Core.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace FlickrApiDemo.Core.WebServices
{
    class FlickrApi
    {
        private const string BaseUrl = "https://api.flickr.com/services/rest/";
        private const string FlickrApiKey = "ca0441b9f295ae48152bf2dd9aa36a9f";

        public async Task<IEnumerable<Photo>> SearchPhotos(string searchText, int page, int pageSize)
        {
            var url = CalculateUrl(searchText, page, pageSize);

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);
                string jsonResponse = GetJsonResponse(response);
                var jObject = JObject.Parse(jsonResponse);
                var photos = jObject["photos"]["photo"];

                return photos.ToObject<IEnumerable<Photo>>();
            }
        }

        private string GetJsonResponse(string response)
        {
            int startIndex = "jsonFlickrApi(".Length;
            int endIndex = response.LastIndexOf(")", StringComparison.Ordinal);
            return response.Substring(startIndex, endIndex - startIndex);
        }

        private string CalculateUrl(string searchText, int page, int pageSize)
        {
            var parameters = GetSearchParameterOptions(searchText, page, pageSize);

            var url = new StringBuilder(BaseUrl);
            url.Append("?");
            foreach (var parameter in parameters)
            {
                var escapeValue = Uri.EscapeDataString(parameter.Value);
                url.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}&", parameter.Key, escapeValue);
            }

            return url.ToString();
        }

        private Dictionary<string, string> GetSearchParameterOptions(string searchText, int page, int pageSize)
        {
            var paramenterOptions = new Dictionary<string, string>()
            {
                {"method", "flickr.photos.search"},
                {"page", page.ToString(CultureInfo.InvariantCulture) },
                {"per_page", pageSize.ToString(CultureInfo.InvariantCulture) },
                {"tags", searchText },
                {"api_key", FlickrApiKey },
                {"format", "json" }
            };

            return paramenterOptions;
        }
    }
}
