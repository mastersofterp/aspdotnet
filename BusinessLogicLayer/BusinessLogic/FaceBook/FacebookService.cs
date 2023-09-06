using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.BusinessLogic.FaceBook
{
    public interface IFacebookService
    {
        Task<AccessUser> GetAccountAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
    }
    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<AccessUser> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>(accessToken, "me", "fields=id,name,first_name,last_name,age_range,birthday,gender");
            //accessToken, "me", "fields=id,name,email,first_name,last_name,age_range,birthday,gender");

            if (result == null)
            {
                return new AccessUser();
            }

            var account = new AccessUser
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name
                //Locale = result.locale
            };

            return account;
        }

        //public async Task PostOnWallAsync(string accessToken, string message)=> await _facebookClient.PostAsync(accessToken, "me/feed", new {message});

        public async Task PostOnWallAsync(string accessToken, string message)
        {
            await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
        }
    }
}
