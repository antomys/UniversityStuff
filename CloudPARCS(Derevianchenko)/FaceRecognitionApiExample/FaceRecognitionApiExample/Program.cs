using System;
using System.Threading.Tasks;

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
        }
    }
}