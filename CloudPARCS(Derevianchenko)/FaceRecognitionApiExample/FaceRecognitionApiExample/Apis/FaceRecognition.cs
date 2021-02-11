using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FaceRecognitionApiExample.Apis
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
   
    internal class FaceRecognition
    {
        public FaceRecognition(string url)
        {
            Url = url;
        }
        private string Url { get; init; }

        public async Task Recognise()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://facial-emotion-recognition.p.rapidapi.com/cloudVision/facialEmotionRecognition?source=https%3A%2F%2Fimages.unsplash.com%2Fphoto-1527631120902-378417754324%3Fixlib%3Drb-1.2.1%26ixid%3DeyJhcHBfaWQiOjEyMDd9%26auto%3Dformat%26fit%3Dcrop%26w%3D2250%26q%3D80&sourceType=url"),
                Headers =
                {
                    { "x-rapidapi-key", "<REQUIRED>" },
                    { "x-rapidapi-host", "facial-emotion-recognition.p.rapidapi.com" },
                },
                Content = new StringContent("{\r\n    \"source\": \"" +
                                            $"{Url}" +
                                            "\",\r\n    \"sourceType\": \"url\"\r\n}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            
            using (var cl = new WebClient()) 
            {
                cl.DownloadFileAsync(new Uri(Url), @"temp.jpg");
            }
            
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(body);
            
            ToJson(body);
            
            await File.WriteAllTextAsync("emergency.json", body);
        }
        
        private void ToJson(string body)
        {
            var deserialize = JsonConvert.DeserializeObject(body);
            File.WriteAllText("face.json",deserialize?.ToString());
        }
        
    }
}