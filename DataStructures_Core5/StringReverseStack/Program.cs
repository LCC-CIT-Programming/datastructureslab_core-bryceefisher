using System;
using System.Collections.Generic;

namespace StringReverseStack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a stack of type string
            Stack<string> stack = new Stack<string>();
            // Get user input
            string userInput = GetString();
            // Create a stack from the user input
            UpdateStack(userInput, stack);
            // Reverse the word
            ReverseWord(stack, userInput);
        }

        //Function to get user input
        private static string GetString()
        {
            //Print message to console
            Console.Write("Enter a string and the program with print it in reverse order: ");
            //Save user input to a variable
            string userInput = Console.ReadLine().ToUpper();
            //Clear the console
            Console.Clear();
            //Return the user input
            return userInput;
        }

        //Function to create a stack from the user input
        private static void UpdateStack(string userInput, Stack<string> stack)
        {
            //Loop through the user input and push each letter to the stack
            for (int i = 0; i < userInput.Length; i++) stack.Push(userInput[i].ToString());
        }

        //Function to reverse the word
        private static void ReverseWord(Stack<string> stack, string userInput)
        {
            //Create a variable to hold the reversed word
            string reversedWord = "";
            //Loop through the stack and add each letter to the reversed word variable
            foreach (string letter in stack) reversedWord += letter;
            //Print the original word and the reversed word to the console
            Console.WriteLine($"Original Phrase: {userInput.ToUpper()}");
            Console.WriteLine($"Reversed Phrase: {reversedWord}");
        }
    }
}