using System;
using System.Collections.Generic;

namespace BinaryNumbersQueue
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //create a queue of strings
            Queue<string> binaryQueue = new Queue<string>();
            //call the method to display the binary numbers passing in the user input and the queue
            DisplayBinary(GetUserInput(), binaryQueue);
        }
        //method to get user input
        private static int GetUserInput()
        {
            //prompt user for input
            Console.Write("Enter a positive number and the program will print all binary numbers up to that number: ");
            //validate user input
            //create a boolean variable to check if the user input is an integer
            bool isInt;
            //create an integer variable to store the user input
            int userInput;
            //if the user input is not an integer or is less than 0, prompt the user to enter a valid number
            do
            {
                isInt = int.TryParse(Console.ReadLine(), out userInput);
                
            } while (!isInt || userInput < 0);

            return userInput;
        }
        //method to display the binary numbers
        private static void DisplayBinary(int userInput, Queue<string> binaryQueue)
        {
            //add 1 to the queue
            binaryQueue.Enqueue("1");
            //loop through the queue and display the binary numbers as well as add 0 and 1 to the end of the string
            for (int i = 0; i < userInput; i++)
            {
                //display the binary numbers
                Console.WriteLine($"{i + 1}: {binaryQueue.Peek()}");
                //add 0 and 1 to the end of the string
                //create
                string s1 = binaryQueue.Dequeue();
                binaryQueue.Enqueue(s1 + "0");
                binaryQueue.Enqueue(s1 + "1");
            }
        }
    }
}