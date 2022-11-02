//avigail and iti
using System;
namespace Stage0
{
    partial class program
    {
        static void Main(string[] args)
        {
            Welcome8388();
            Welcome2367();
            Console.ReadKey();
        }
        static partial void Welcome2367();

        private static void Welcome8388()
        {
            Console.WriteLine("Enter your name");
            string username = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", username);
        }
    }
   
}