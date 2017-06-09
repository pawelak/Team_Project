using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using TaskMaster.BLL.MobileServices;
using TaskMaster.BLL.WebApiModels;

namespace TaskMaster.WebApi.Controllers
{
    public class UserController : ApiController
    {
        // TODO wszystkie serwisy powinny stać za interfejsami
        private readonly UserWebApiService _userWebApiService = new UserWebApiService();
        private readonly PrintAllTestService _printAllTestService = new PrintAllTestService();

        // GET: api/User               
        public List<UserMobileDto> Get()
        {
            return _printAllTestService.PrintAllUserWebApi();
        }

        // GET: api/User/email              pobiera dane urzytkownika o zadanym mailu
        public HttpResponseMessage Get(HttpRequestMessage request ,string email)
        {
            // TODO niepotrzebna jawna serializacja, brac przykład z pierwszej akcji GET
            return request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(_userWebApiService.GetUserByEmail(email)));
        }

        // TODO akcja PUT jest do dodawania
        // POST: api/User                                       edycja użytkownika
        public HttpResponseMessage Post([FromBody]string usr)
        {
            // TODO niepotrzebna jawna deserializacja, wystarczy jako parametr podać model, webApi zserializuje to w locie
            var tmp = JsonConvert.DeserializeObject<UserMobileDto>(usr);
            return _userWebApiService.EditUser(tmp)
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // TODO akcja PUT jest do edytowanie
        // PUT: api/User                                    dodanie nowego użtkownika
        public HttpResponseMessage Put([FromBody]string user)
        {
            // TODO niepotrzebna jawna deserializacja, wystarczy jako parametr podać model, webApi zserializuje to w locie
            var tmp = JsonConvert.DeserializeObject<UserMobileDto>(user);
          
            return _userWebApiService.AddNewUser(tmp) 
                ? new HttpResponseMessage(HttpStatusCode.OK) 
                : new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // DELETE: api/User                                     usuwanie
        public HttpResponseMessage Delete([FromBody]UserMobileDto userMobileDto)
        {
            // TODO po co negacja? jak delete zwróci true to powinniscie wyslac OK
            return !_userWebApiService.DeleteUserByEmail(userMobileDto)
                ? new HttpResponseMessage(HttpStatusCode.OK)
                : new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
