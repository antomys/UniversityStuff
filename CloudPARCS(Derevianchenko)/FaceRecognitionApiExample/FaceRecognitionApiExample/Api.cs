using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FaceRecognitionApiExample
{
    
    public class Api
    {
        private string Body { get; set; }
        public async Task GetDataFromApi(string city)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://community-open-weather-map.p.rapidapi.com/weather?q={city}&lang=Russian&units=metric"),
                Headers =
                {
                    { "x-rapidapi-key", "" }, //todo:add key 
                    { "x-rapidapi-host", "" }, //todo: add host
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(body);
                Body = body;
            }
            ToJson();
            LoadFromJsonString();
        }

        private void ToJson()
        {
            var deserialize = JsonConvert.DeserializeObject(Body);
            File.WriteAllText("test.json",deserialize?.ToString());
        }

        private void LoadFromJsonString()
        {
            var deserializeResult = JsonConvert.DeserializeObject<Result>(File.ReadAllText("test.json"));
            /*var deserializeMain = JsonConvert.DeserializeObject<Main>(File.ReadAllText("test.json"));
            var deserializeWind = JsonConvert.DeserializeObject<Wind>(File.ReadAllText("test.json"));*/
            
            Print(deserializeResult);
        }

        private void Print(Result result)
        {
            Console.WriteLine($"" +
                              $"Country code: {result.Sys.Country};\nCity: {result.Name}\nTemperature: {result.Main.Temp}°C\n" +
                              $"Feels like: {result.Main.FeelsLike}°C\nPressure: {result.Main.Pressure}\n" +
                              $"Humidity: {result.Main.Humidity}\nWind Speed : {result.Wind.Speed}\n");
        }
    }
}