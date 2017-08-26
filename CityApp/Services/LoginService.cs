using System.Net.Http;
using System.Threading.Tasks;

namespace CityApp
{
    public class LoginService
    {
        public async static Task<Model<UserData>> Login(string body)
        {
            var result = await CommonService<UserData>.HttpPutOperation("/v1/User", body);
            return result;
        }
    }
}