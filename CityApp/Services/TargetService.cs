using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityApp
{
    public class TargetService
    {
        public async static Task<Model<List<Target>>> ListCheque()
        {
            var result = await CommonService<List<Target>>.HttpPostOperation("");
            if (result.data!=null)
            {
                try
                {
                    result.data = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<Target>>(result.data.ToString()));
                }
                catch (Exception e)
                {
                    result.message = e.ToString();
                }
            }
            return result;
        }
    }
}