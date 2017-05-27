using System;
using System.Net;
using System.Net.Http;
using System.Security;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.BLL.MobileServices
{
    public class VeryficationService
    {

        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

        public GoogleUser GetUserDetails(string token)
        {

            var httpClient = new HttpClient();
            var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, token));

            HttpResponseMessage httpResponseMessage;
            try
            {
                httpResponseMessage = httpClient.GetAsync(requestUri).Result;
            }
            catch (Exception ex)
            {
                return null;
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var googleApiTokenInfo = JsonConvert.DeserializeObject<GoogleUser>(response);

            if (response == null)
            {
                Console.WriteLine("Google API Token Info aud field ({0}) not containing the required client id");
                return null;
            }

            return googleApiTokenInfo;
        }
    }
}



