using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using UserInfo = Models.UserInfo;

namespace HelloWorldConsumer
{
    public static class Listener
    {
        const string ServiceBusConnectionString = "Endpoint=sb://hasibul.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ABAdiTtn32vGiN4CBlFCCGnpirTwMn4qwnZv49mVy34=";
        const string QueueName = "helloworld";
        static IQueueClient queueClient;
        private static MessageHandlerOptions messageHandlerOptions;

        public static void Listen()
        {
            if (queueClient == null)
                queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            if (messageHandlerOptions == null   )
            {
                messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
                {
                    AutoComplete = true, MaxConcurrentCalls = 1
                };
            }

            queueClient.RegisterMessageHandler(Handler,  messageHandlerOptions);
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
