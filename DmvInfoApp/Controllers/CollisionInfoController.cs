using DmvInfoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DmvInfoApp.Controllers
{
    [RoutePrefix("collision")]
    public class CollisionInfoController : ApiController
    {
        [Route("filter[borough]={boro}")]
        public JsonApiObject<CollisionInfo> GetCollisionInfo(string boro)
        {
            var jsonAPIObject = new JsonApiObject<CollisionInfo>();
            try
            {
                jsonAPIObject.data = CollisionInfo.GetCollisionInfoByBorough(boro);
            }
            catch
            {
                var errorList = new List<JsonApiError>();
                var error = new JsonApiError();
                error.title = "We are experiencing problems with our database.  Please try again later.";
                errorList.Add(error);
                jsonAPIObject.errors = errorList;
            }
            return jsonAPIObject;
        }

        [Route("filter[zip]={zipcode}")]
        public JsonApiObject<CollisionInfo> GetCollisionInfoByZipcode(string zipcode)
        {
            var jsonAPIObject = new JsonApiObject<CollisionInfo>();
            try
            {
                jsonAPIObject.data = CollisionInfo.GetCollisionInfoByZip(zipcode);
            }
            catch
            {
                var errorList = new List<JsonApiError>();
                var error = new JsonApiError();
                error.title = "We are experiencing problems with our database.  Please try again later.";
                errorList.Add(error);
                jsonAPIObject.errors = errorList;
            }
            return jsonAPIObject;
        }
    }
}
