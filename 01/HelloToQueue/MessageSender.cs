using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HelloToQueue
{
  

    public static class MessageSender
    {
        const string ServiceBusConnectionString = "Endpoint=sb://hasibul.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ABAdiTtn32vGiN4CBlFCCGnpirTwMn4qwnZv49mVy34=";
        const string QueueName = "helloworld";
        static IQueueClient queueClient;


        public static Task Send<T>(T message)
        {
            if (queueClient == null)
                queueClient = new QueueClient(ServiceBusConnectionString, QueueName);


            var strMessage = JsonConvert.SerializeObject(message);
            return queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(strMessage)));

        }
    }
}
