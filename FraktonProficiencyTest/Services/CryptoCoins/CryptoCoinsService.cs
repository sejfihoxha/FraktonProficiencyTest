using FraktonProficiencyTest.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace FraktonProficiencyTest.Services.CryptoCoins
{
    public class CryptoCoinsService : ICryptoCoinsService
    {
        public CryptoCoinsModel GetAll()
        {
            var result = new CryptoCoinsModel(); ;

            using (var client = new HttpClient())
            {
                string url = string.Format("https://api.coincap.io/v2/assets");
                client.BaseAddress = new Uri(url);

                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if(responseMessage.IsSuccessStatusCode)
                    result = JsonConvert.DeserializeObject<CryptoCoinsModel>(responseMessage.Content.ReadAsStringAsync().Result);
            }

            return result;
        }
    }
}
