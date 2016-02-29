using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DmvInfoApp.Controllers
{
    public class JsonApiObject<T>
    {
        public IEnumerable<T> data { get; set; }
        public IEnumerable<JsonApiError> errors { get; set; }

        public JsonApiObject()
        {
            data = new List<T>();
            errors = new List<JsonApiError>();
        }
    }

    public class JsonApiError
    {
        public string title { get; set; }
    }
}