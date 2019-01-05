using System;
using System.Threading.Tasks;
using Models;

namespace HelloToQueue
{
    class Program
    {
        static async Task Main(string[] args)
        {



            Console.WriteLine("Enter to send ");
            Console.ReadLine();
            await EventPublisher.Publish(new UserInfo
            {
                Age = 25, Id = Guid.NewGuid(), UserName = "masud rana"
            });

            Console.Write("Done");
            Console.ReadLine();

        }
    }
}
