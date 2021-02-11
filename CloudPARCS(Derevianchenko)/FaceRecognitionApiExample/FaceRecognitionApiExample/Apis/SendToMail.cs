using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FaceRecognitionApiExample.Apis
{
    public static class SendToMail
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        private static readonly string[] Scopes = {GmailService.Scope.GmailSend};
        private const string ApplicationName = "FaceRecognition";
        private const string _weatherFile = "weather.json";
        private const string _faceFile = "face.json";

        public static void QuickStart()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }

            Console.Read();
        }
        
        public static void SendHtmlMessage()
        {
            //Create Message
            MailMessage mail = new MailMessage();
            mail.Subject = "Result of APIs!";
            var weather = File.ReadAllText(_weatherFile);
            mail.Body = $"Sending {_weatherFile} and  {_faceFile}";
            mail.From = new MailAddress("<REQUIRED>");
            mail.IsBodyHtml = true;
            
            string attImg = "temp.jpg";
            mail.Attachments.Add(new Attachment(attImg));
            /*mail.Attachments.Add(new Attachment(_faceFile));
            mail.Attachments.Add(new Attachment(_weatherFile));*/
            

            mail.To.Add(new MailAddress("<REQUIRED>"));
            MimeKit.MimeMessage mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);

            Message message = new Message();
            message.Raw = Base64UrlEncode(mimeMessage.ToString());
            //Gmail API credentials
            UserCredential credential;
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart2.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            
            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            //Send Email
            var result = service.Users.Messages.Send(message, "me").Execute();
        }
        
        private static string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
    }
}