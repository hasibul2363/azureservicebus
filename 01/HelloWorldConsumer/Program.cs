﻿using System;
using System.Threading.Tasks;

namespace HelloWorldConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            Listener.Listen();
            Subscriber.Subscribe();
            Console.WriteLine("Now listening...");
            Console.ReadLine();
        }
    }
}
