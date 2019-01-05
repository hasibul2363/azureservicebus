using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloToQueue
{
    public static class EventPublisher
    {
        const string ServiceBusConnectionString = "Endpoint=sb://hasibul.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=ABAdiTtn32vGiN4CBlFCCGnpirTwMn4qwnZv49mVy34=";
        const string topicName = "eventbus";
        static ITopicClient queueClient;


        public static Task Publish<T>(T @event)
        {
            if (queueClient == null)
                queueClient = new TopicClient(ServiceBusConnectionString, topicName);


            var strMessage = JsonConvert.SerializeObject(@event);
            return queueClient.SendAsync(new Message(Encoding.UTF8.GetBytes(strMessage)));

        }
    }
}
