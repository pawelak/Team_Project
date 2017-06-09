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
        private const string t1 = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBkNDI5Y2I1M2Q4YTFlZjA5ZjFhZTY1ODc1N2JlMjFlNzZhNjEzN2IifQ.eyJhenAiOiI3MjM0OTQ4NzM5ODEtcXNubnA1dnNhNzJmNGQ3NGJvOG04a3Fmc3JibzI1Y3EuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiI3MjM0OTQ4NzM5ODEtbnAzdjF1OWpzNmptYW4ycXJpNXIwZ2ZkN2ZsM2czYzIuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTYwNTkxNDU0MTAwODI2MzkxMzYiLCJlbWFpbCI6InBpb3RyZWthczFAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImlzcyI6Imh0dHBzOi8vYWNjb3VudHMuZ29vZ2xlLmNvbSIsImlhdCI6MTQ5Njc1Njg3NCwiZXhwIjoxNDk2NzYwNDc0LCJuYW1lIjoiUGlvdHIgQmFyc3pjenlrIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS8tQk9uUmc3WjhhRU0vQUFBQUFBQUFBQUkvQUFBQUFBQUFBQUEvQUF5WUJGNWFmQTFSSTRfSm9RY1lxQ3kxRkJ1dlVqR0lqQS9zOTYtYy9waG90by5qcGciLCJnaXZlbl9uYW1lIjoiUGlvdHIiLCJmYW1pbHlfbmFtZSI6IkJhcnN6Y3p5ayIsImxvY2FsZSI6InBsIn0.t4O6XrlakFCrLq3ODetJ0XPPkrweJoNx0k7FndBdmEHW_ZfdqrrennE6eQiJzhRFSlMSE8G-X5XxtDNh_Swn1O-AJhCi29G1nnDpPg5jn5jpCBlBNkMfYThTlBf-JyI0SrtI29xKy4UyD64r9ejSzGOec_fihmIjjVn7tAFH30hoJKbdkWUhwOVhNFMbj84f-LFpDk26YFAGvFzuWGsf-R-GzTEx-vvBtlnsenMQVyx5fxi41qfmO6adm6IUHQ2tvg0zqrtWHrYn8MFOZ84vdRAlsZF0CpBN1M65bLLTwDsFMhhTlJtONJ4v3MHdtvXvZkxRBRqQ-3qTnfN8xNqgQw";
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



