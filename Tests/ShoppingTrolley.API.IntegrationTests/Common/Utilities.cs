using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingTrolley.API.IntegrationTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response) where T: new()
        {
            try
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            catch
            {

               return new T();
            }
        }
    }
}
