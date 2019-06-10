using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace api.appstore.Models
{
    public class CommonModels
    {
        public enum FileType
        {
            AppIcon, AppAndroidSmart, AppAndroidTablet, AppIphoneSmart, AppIphoneTablet, AppScreen
        }

        public string GetAPILink()
        {
            return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SFApiLink"]);
            //return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WebApiLink"]);
        }

        public Task<HttpResponseMessage> Get<T>(string requestUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetAPILink());
                var responseTask = client.GetAsync(requestUrl);
                responseTask.Wait();
                return responseTask;
            }
        }

        [HttpPost]
        public Task<HttpResponseMessage> Post<T>(string requestUrl, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetAPILink());
                var postTask = client.PostAsJsonAsync<T>(requestUrl, data);
                postTask.Wait();
                return postTask;
            }
        }

        [HttpPost]
        public Task<HttpResponseMessage> Put<T>(string requestUrl, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetAPILink());
                var putTask = client.PutAsJsonAsync<T>(requestUrl, data);
                putTask.Wait();
                return putTask;
            }
        }
    }
}