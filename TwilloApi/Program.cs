using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace TwilioApi
{
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ////1
            //var client = new RestClient("https://api.twilio.com/2010-04-01");
            ////2
            //var request = new RestRequest("Accounts/AC707b7340f05a878433a345cef9384646/Messages", Method.POST);
            ////3
            //request.AddParameter("To", "+12029090989");
            //request.AddParameter("From", "+12013714641");
            //request.AddParameter("Body", "Hello world!");
            ////4
            //client.Authenticator = new HttpBasicAuthenticator("AC707b7340f05a878433a345cef9384646", "fd17f2c186f59552189befb72e979284");
            ////5
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response);
            //});
            //Console.ReadLine();
        

            var client = new RestClient("https://api.twilio.com/2010-04-01");
            //1
            var request = new RestRequest("Accounts/AC707b7340f05a878433a345cef9384646/Messages.json", Method.GET);
            client.Authenticator = new HttpBasicAuthenticator("AC707b7340f05a878433a345cef9384646", "fd17f2c186f59552189befb72e979284");
            //2
            var response = new RestResponse();
            //3a
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            //4
            Console.WriteLine(response.Content);
            Console.ReadLine();
            //JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

            //var messageList = JsonConvert.DeserializeObject<List<Message>>(jsonResponse["messages"].ToString());
            //foreach (var message in messageList)
            //{
            //    Console.WriteLine("To: {0}", message.To);
            //    Console.WriteLine("From: {0}", message.From);
            //    Console.WriteLine("Body: {0}", message.Body);
            //    Console.WriteLine("Status: {0}", message.Status);
            //    Console.WriteLine("======================================================");
            //}
            //Console.ReadLine();

        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}