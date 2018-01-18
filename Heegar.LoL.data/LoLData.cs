using Heegar.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Heegar.LoL.data
{
    public static class Requests
    {
        public static string SomeRequest = "https://na1.api.riotgames.com/lol/summoner/v3/summoners/by-name/RiotSchmick?api_key={0}";

        static long KingdomManAccountID = 200569521;
        static long AdamusHeegariusAccountID = 40069102;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSummonerProfile(string name, string key)
        {
            return string.Format("https://na1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{0}?api_key={1}", name, key);
        }

        public static string GetSummonerMasteryBySummonerID(long summonerID, string key)
        {
            return string.Format("https://na1.api.riotgames.com/lol/champion-mastery/v3/champion-masteries/by-summoner/{0}?api_key={1}", summonerID, key);
        }

        public static string GetSummonerBySummonerName(string name, string key)
        {
            return string.Format("https://na1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{0}?api_key={1}", name, key);
        }
    }
    public class LoLData
    {
        private string _apiKey;

        public LoLData(string APIKey)
        {
            this._apiKey = APIKey;
        }

        public string GetSummonerMastery(long SummonerID)
        {
            return SendRequest(Requests.GetSummonerMasteryBySummonerID(SummonerID, _apiKey));
        }

        public string SendRequest(string url)
        {
            return Utilities.SendURLAndGetResponse(url);
        }
        public string GetSummonerProfile(string summonerName)
        {
            return SendRequest(Requests.GetSummonerBySummonerName(summonerName, _apiKey));
            return SubmitReqeustAndGetResponse(Requests.GetSummonerBySummonerName(summonerName, _apiKey));
        }

        public string SubmitReqeustAndGetResponse(string request)
        {
            return SubmitReqeustAndGetResponseAsync(request).Result;
        }

        public async Task<string> SubmitReqeustAndGetResponseAsync(string request)
        {
            HttpClient client = new HttpClient();

            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                HttpResponseMessage response = await client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
            finally
            {
                // Need to call dispose on the HttpClient object
                // when done using it, so the app doesn't leak resources
                client.Dispose();
            }
        }
    }
}

