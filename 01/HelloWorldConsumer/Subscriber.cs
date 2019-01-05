using Microsoft.Azure.ServiceBus;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloWorldConsumer
{
    public static class Subscriber
    {
        const string ServiceBusConnectionString = "Endpoint=sb://hasibul.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ABAdiTtn32vGiN4CBlFCCGnpirTwMn4qwnZv49mVy34=";
        const string topicName = "eventbus";
        private const string subscriberName = "consumer1";
        static ISubscriptionClient subscriptionClient;
        private static MessageHandlerOptions messageHandlerOptions;
        public static void Subscribe()
        {
            if (subscriptionClient == null)
                subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, topicName,subscriberName);

            if (messageHandlerOptions == null)
            {
                messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    AutoComplete = true,
                    MaxConcurrentCalls = 1
                };
            }

            subscriptionClient.RegisterMessageHandler(Handler, messageHandlerOptions);
        }

        private static Task Handler(Message message, CancellationToken cancellationToken)
        {
            var str = Encoding.UTF8.GetString(message.Body);
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(str);
            Console.WriteLine($"Done for {userInfo.UserName}");
            return Task.CompletedTask;
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
