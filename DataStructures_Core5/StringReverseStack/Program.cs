using System;
using System.Collections.Generic;

namespace StringReverseStack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a stack of type string
            var stack = new Stack<string>();
            // Get user input
            var userInput = GetString();
            // Create a stack from the user input
            CreateStack(userInput, stack);
            // Reverse the word
            ReverseWord(stack, userInput);
        }
        //Function to get user input
        private static string GetString()
        {
            //Print message to console
            Console.Write("Enter a string and the program with print it in reverse order: ");
            //Save user input to a variable
            var userInput = Console.ReadLine().ToUpper();
            //Clear the console
            Console.Clear();
            //Return the user input
            return userInput;
        }
        //Function to create a stack from the user input
        private static Stack<string> CreateStack(string userInput, Stack<string> stack)
        {
            //Loop through the user input and push each letter to the stack
            for (var i = 0; i < userInput.Length; i++) stack.Push(userInput[i].ToString());
            //Return the stack
            return stack;
        }
        //Function to reverse the word
        private static void ReverseWord(Stack<string> stack, string userInput)
        {
            //Create a variable to hold the reversed word
            var reversedWord = "";
            //Loop through the stack and add each letter to the reversed word variable
            foreach (var letter in stack) reversedWord += letter;
            //Print the original word and the reversed word to the console
            Console.WriteLine($"Original Word: {userInput.ToUpper()}");
            Console.WriteLine($"Reversed Word: {reversedWord}");
        }
    }
}