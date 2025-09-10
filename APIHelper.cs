using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PepsArts.Models
{

    //API helper class
    public static class APIHelper
    {
        
            public static async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data)
            {
                var client = new HttpClient();
                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PostAsync(url, content);
            }
       

    }
}