using System;
using System.Threading.Tasks;
using FaceRecognitionApiExample.Apis;

namespace FaceRecognitionApiExample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Write("Please input city to see temperature: ");
            var input = Console.ReadLine();
            var result = new Api();
            await result.GetDataFromApi(input);

            Console.Write("Please insert link to face image: ");
            input = Console.ReadLine();
            var face = new FaceRecognition(input);
            await face.Recognise();
            
            SendToMail.SendHtmlMessage();
        }
    }
}