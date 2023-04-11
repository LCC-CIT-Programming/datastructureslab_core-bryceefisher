using System;
using System.Collections.Generic;

namespace BinaryNumbersQueue
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Queue<string> binaryQueue = new Queue<string>();
            
            DisplayBinary(GetUserInput(), binaryQueue);
            
        }

        static int GetUserInput()
        {
            Console.Write("Enter a positive number and the program will print all binary numbers up to that number: ");

            bool isInt;
            int userInput;
            do
            {
                isInt = int.TryParse(Console.ReadLine(), out userInput);
            } while (!isInt || userInput < 0);

            return userInput;
        }

        static void DisplayBinary(int userInput, Queue<string> binaryQueue)
        {
            binaryQueue.Enqueue("1");
            
            for (int i = 0; i < userInput; i++)
            {
                Console.WriteLine($"{i+1}: {binaryQueue.Peek()}");
                string s1 = binaryQueue.Dequeue();
                binaryQueue.Enqueue(s1 + "0");
                binaryQueue.Enqueue(s1 + "1");
            }
        }
    }
}