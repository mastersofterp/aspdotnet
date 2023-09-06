using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace BusinessLogicLayer.BusinessLogic.FaceBook
{
    public interface IFacebookClient
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
        Task PostAsync(string accessToken, string endpoint, object data, string args = null);
    }
    public class FacebookClient : IFacebookClient
    {
        private readonly HttpClient _httpClient;

        public FacebookClient()
        {
            _httpClient = new HttpClient
            {
                //BaseAddress = new Uri("https://graph.facebook.com/v2.8/")
                BaseAddress = new Uri("https://graph.facebook.com/v8.0/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
            //var response = await _httpClient.GetAsync("{endpoint}?access_token={accessToken}&{args}");
            //var response = await _httpClient.GetAsync("{endpoint}?{args}&access_token={accessToken}");
            //HttpResponseMessage response = await _httpClient.GetAsync("https://graph.facebook.com/v8.0/me?fields=id%2Cname%2Cfirst_name%2Clast_name%2Cage_range%2Cbirthday%2Cgender&access_token=EAAFdbfvrskoBAFiGSvy4KpDySv0Y9eaOVxP2TMot1Af5FcsvCoJZAvfhna2tz0o9KdYuiJwZC5Tr3zqbOlJy2SUWMKo8xPQrF4drKYEEIOSzocOjwW9mmZClb3RMgLiWunx5HGWoPM7nncdlPFrCbp9Kjlq7XJOVjRpxnwxZAoWvwU5WzE50MNCjV9NRnX7lkqIRB58VJwZDZD");
            var response = _httpClient.GetAsync("https://graph.facebook.com/v8.0/me?fields=id%2Cname%2Cfirst_name%2Clast_name%2Cage_range%2Cbirthday%2Cgender&access_token=" + accessToken).Result;  // Blocking call!
            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
        {
            var payload = GetPayload(data);
            //await _httpClient.PostAsync("{endpoint}?access_token={accessToken}&{args}",payload);
            await _httpClient.PostAsync("https://graph.facebook.com/v8.0/me?fields=id%2Cname%2Cfirst_name%2Clast_name%2Cage_range%2Cbirthday%2Cgender&access_token=" + accessToken, payload);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
