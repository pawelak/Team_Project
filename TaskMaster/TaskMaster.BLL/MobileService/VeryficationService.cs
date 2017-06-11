using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using TaskMaster.BLL.WebApiModels;
using TaskMaster.DAL.Repositories;

namespace TaskMaster.BLL.MobileServices
{
    public class VeryficationService
    {
        private readonly UserRepositories _userRepositories = new UserRepositories();

        private const string GoogleApiTokenInfoUrl = "www.googleapis.com/drive/v2/files?access_token={0}";

        public bool Verify(string token)
        {
            //dopisac
            return true;

        }

        public bool Authorization(string email, string token)   //przydało by się sprawdzać czy z tego urządzenia
        {
            return (_userRepositories.Get(email).Tokens.FirstOrDefault(t => t.Token.Equals(token)) != null) ? true : false;
        }

        public string GenereteToken()
        {
            var g = Guid.NewGuid();
            return g.ToString();
        }

        public bool VeryfyGoogle(string token)
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
                return false;
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }
    }
}



