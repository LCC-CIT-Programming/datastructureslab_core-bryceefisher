using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Yahtzee
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //declare variables for the user's scorecard and the computer's scorecard
            var userScorecard = new int[16];
            var computerScorecard = new int[16];
            //declare a variable for the number of turns the user has taken and another for the number of moves the computer has taken
            var userMoves = 0;
            var computerMoves = 0;
            //declare a boolean that knows if it is the user's turn and set it to false
            var userTurn = false;
            // call ResetScorecard for the user
            ResetScorecard(userScorecard, ref userMoves);
            // call ResetScorecard for the computer
            ResetScorecard(computerScorecard, ref computerMoves);


            do
            {
                //set the userTurn variable to the opposite value
                userTurn = !userTurn;
                //call UpdateScorecard for the user
                UpdateScorecard(userScorecard);
                //call UpdateScorecard for the computer
                UpdateScorecard(computerScorecard);
                //call DisplayScorecards
                DisplayScoreCards(userScorecard, computerScorecard);
                //if it's the user's turn
                if (userTurn)
                {
                    Console.WriteLine("It's your turn.");
                    Pause();
                    //call UserPlay
                    UserPlay(userScorecard, ref userMoves);
                }
                else
                {
                    Console.WriteLine("It's the computer's turn.");
                    Pause();
                    //call ComputerPlay
                    ComputerPlay(computerScorecard, ref computerMoves);
                }

               


                //end if
                //while both the user's count and the computer's count are <= yahtzee
            } while (userMoves <= 12 || computerMoves <= 12);

            //call UpdateScorecard for the user
            UpdateScorecard(userScorecard);
            //call UpdateScorecard for the computer
            UpdateScorecard(computerScorecard);
            //call DisplayScorecards
            DisplayScoreCards(userScorecard, computerScorecard);
            
            //display a message about who won
            
            if (userScorecard[15] > computerScorecard[15])
                Console.WriteLine("You won!");
            else if (userScorecard[15] < computerScorecard[15])
                Console.WriteLine("The computer won!");
            else
                Console.WriteLine("It's a tie!");

            //display a message about who won
        }

        private static void Pause()
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        #region Scorecard Methods

        // sets all of the items in a scorecard to -1 to start the game
        // takes a data structure for a scorecard and the corresponding score card count as parameters.  Both are altered by the method.
        private static void ResetScorecard(int[] scorecard, ref int count)
        {
            for (var i = 0; i < 16; i++) scorecard[i] = -1;

            count = 0;
        }

        // calculates the subtotal, bonus and the total for the scorecard
        // takes a data structure for a scorecard as it's parameter
        private static void UpdateScorecard(int[] scorecard)
        {
            //you can uncomment this code once you declare the parameter
            scorecard[13] = 0;
            scorecard[14] = 0;
            for (var i = 0; i <= 5; i++)
                if (scorecard[i] != -1)
                    scorecard[13] += scorecard[i];

            if (scorecard[13] >= 63)
                scorecard[14] = 35;

            scorecard[15] = scorecard[13] + scorecard[14];
            for (var i = 6; i <= 12; i++)
                if (scorecard[i] != -1)
                    scorecard[15] += scorecard[i];
        }

        private static string FormatCell(int value)
        {
            return value < 0 ? "" : value.ToString();
        }

        // takes the data structure for the user's scorecard and the data structure for the computer's scorecard as parameters
        private static void DisplayScoreCards(int[] uScorecard, int[] cScorecard)
        {
            //you can uncomment this code when you have declared the parameters
            string[] labels =
            {
                "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
                "3 of a Kind", "4 of a Kind", "Full House", "Small Straight", "Large Straight",
                "Chance", "Yahtzee", "Sub Total", "Bonus", "Total Score"
            };
            var lineFormat = "| {3,2} {0,-15}|{1,8}|{2,8}|";
            var border = new string('-', 39);

            Console.Clear();
            Console.WriteLine(border);
            Console.WriteLine(lineFormat, "", "  You   ", "Computer", "");
            Console.WriteLine(border);
            for (var i = 0; i <= 5; i++)
                Console.WriteLine(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i);

            Console.WriteLine(border);
            Console.WriteLine(lineFormat, labels[13], FormatCell(uScorecard[13]), FormatCell(cScorecard[13]), "");
            Console.WriteLine(border);
            Console.WriteLine(lineFormat, labels[14], FormatCell(uScorecard[14]), FormatCell(cScorecard[14]), "");
            Console.WriteLine(border);
            for (var i = 6; i <= 12; i++)
                Console.WriteLine(lineFormat, labels[i], FormatCell(uScorecard[i]), FormatCell(cScorecard[i]), i);

            Console.WriteLine(border);
            Console.WriteLine(lineFormat, labels[15], FormatCell(uScorecard[15]), FormatCell(cScorecard[15]), "");
            Console.WriteLine(border);
        }

        #endregion

        #region Rolling Methods

        // rolls the specified number of dice and adds them to the data structure for the dice
        // takes an integer that represents the number of dice to roll and a data structure to hold the dice as it's parameters
        private static void Roll(int numDice, List<int> dice)
        {
            var gen = new Random();
            for (var i = 0; i < numDice; i++)
                dice.Add(gen.Next(1, 7));
        }

        // takes a data structure that is a set of dice as it's parameter.  Call it dice.
        private static void DisplayDice(List<int> dice)
        {
            //you can uncomment this code when you have declared the parameter
            var lineFormat = "|   {0}  |";
            var border = "*------*";
            var second = "|      |";

            foreach (var d in dice)
                Console.Write(border);
            Console.WriteLine();
            // foreach (var d in dice)
            //     Console.Write(second);
            // Console.WriteLine("");
            foreach (var d in dice)
                Console.Write(lineFormat, d);
            Console.WriteLine();
            // foreach (var d in dice)
            //     Console.Write(second);
            // Console.WriteLine("");
            foreach (var d in dice)
                Console.Write(border);
            Console.WriteLine();
        }

        #endregion

        #region Computer Play Methods

        // figures out the highest possible score for the set of dice for the computer
        // takes the scorecard datastructure and the set of dice that the computer is keeping as it's parameters
        private static int GetComputerScorecardItem(int[] scorecard, List<int> keeping)
        {
            //create an array of strings that contains the labels for the scorecard
            string[] labels =
            {
                "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
                "3 of a Kind", "4 of a Kind", "Full House", "Small Straight", "Large Straight",
                "Chance", "Yahtzee"
            };
            //create an integer variable to hold the index of the highest score
            var indexOfMax = 0;
            //create an integer variable to hold the highest score
            var max = 0;

            //you can uncomment this code once you've identified the parameters for this method
            for (var i = 0; i <= 12; i++)
                if (scorecard[i] == -1)
                {
                    //call the Score method to get the score for the current item
                    var score = Score(keeping, i);
                    //if the score is greater than the current max, 
                    if (score >= max)
                    {
                        //set the max to the score and set the index of the max to the current index
                        max = score;
                        indexOfMax = i;
                    }
                }
            //display the computer's choice
            Console.WriteLine("Computer chose {0} for {1} points", labels[indexOfMax], max);
            Pause();
            //return the index of the highest score
            return indexOfMax;
        }
        //method to check for small straights, takes a data structure that holds the dice as it's parameter
        private static bool CheckForSmallStraights(List<int> rolling)
        {
            //sort the data structure
            rolling.Sort();
            //create an integer variable to hold the number of consecutive dice
            var count = 0;
            //loop through the data structure
            for (var i = 0; i < rolling.Count - 1; i++)
            {
                //if the current die is one less than the next die increase the count
                if (rolling[i] == rolling[i + 1] - 1) count++;
                //if the count is 3 or more, return true
                if (count >= 3) return true;
            }
            //if the count is less than 3, return false
            return false;
        }
        //method to check for large straights, takes a data structure that holds the dice as it's parameter
        private static bool CheckForLargeStraights(List<int> rolling)
        {
            //sort the data structure
            rolling.Sort();
            //loop through the data structure
            for (var i = 0; i < rolling.Count - 1; i++)
                //if the current die is not one less than the next die, return false
                if (rolling[i] != rolling[i + 1] - 1)
                {return false;}
            //if the loop completes, return true
            return true;
        }

        // method to determine what dice to keep for the computer
        private static void GetCompKeeping(List<int> rolling, List<int> keeping, int[] scorecard)
        {
            // if the computer has a large straight, keep all the dice
            if (CheckForLargeStraights(rolling) && scorecard[10] == -1)
            {
                keeping.AddRange(rolling);
                return;
            }
            // if the computer has a small straight, keep all the dice with the exception of the duplicate
            if (CheckForSmallStraights(rolling) && scorecard[9] == -1)
            {
                //sort the data structure
                keeping.Sort();
                //add rolling to keeping
                keeping.AddRange(rolling);
                //loop through keeping
                for (var i = 0; i < keeping.Count - 1; i++)
                    //if the current die is the same as the next die, remove the current die
                    if (keeping[i] == keeping[i + 1])
                        keeping.Remove(keeping[i]);
                //clear rolling
                rolling.Clear();
            }
            //otherwise focus on keeping the dice that will give the most pairs, 3 of a kind, or 4 of a kind, or yahtzee
            else
            {
                //loop through rolling
                for (var i = 0; i < rolling.Count; i++)
                {
                    //count the number of times the current die is in rolling and keeping
                    var rollingCount = rolling.Count(x => x == rolling[i]);
                    var keepingCount = keeping.Count(x => x == rolling[i]);
                    //if the current die is in rolling 2 or more times or in keeping 1 or more times, add it to keeping
                    if (rollingCount >= 2 || keepingCount >= 1) keeping.Add(rolling[i]);
                }
            }
            //clear rolling 
            rolling.Clear();
        }


        // implements the computer's turn.  The computer only rolls once.
        // You can earn extra credit for making the computer play smarter
        // takes the computer's scorecard data structure and scorecard count as parameters.  Both are altered by the method.
        private static void ComputerPlay(int[] cScorecard, ref int cScorecardCount)
        {
            //declare a data structure for the dice that the computer is rolling. 
            var rolling = new List<int>();
            //declare a data structure for the dice that the computer is keeping.  Call it keeping.
            var keeping = new List<int>();
            //declare an integer to hold the index of the scorecard item that the computer is scoring.  
            var itemIndex = -1;
            //delcare an integer to hold the number of rolls.  
            var numRolls = 0;
            // Do while the number of rolls < 3 and the number of dice the computer is keeping is < 5
            do
            {
                //declare an integer to hold the number of dice to roll.  Call it diceToRoll
                var diceToRoll = 5 - keeping.Count;
                //Call Roll passing the number of dice to roll and the data structure for the dice that the computer is rolling
                Roll(diceToRoll, rolling);
                //increment the number of rolls
                numRolls++;
                //display a message

                Console.WriteLine("The dice the computer rolled: ");
                //Call DisplayDice
                DisplayDice(rolling);
                Pause();
                //if the number of rolls is < 3
                if (numRolls < 3)
                    //Call GetKeeping passing the data structure for the dice that the computer is rolling and the
                    //data structure for the dice that the computer is keeping
                    GetCompKeeping(rolling, keeping, cScorecard);
                //else
                else
                    //Call MoveRollToKeeping
                    MoveRollToKeep(rolling, keeping);

                //display a message
                Console.WriteLine("The dice the computer is keeping: ");
                //Call DisplayDice
                DisplayDice(keeping);
                Pause();

                //while the number of rolls < 3 and the number of dice the user is keeping is < 5
            } while (numRolls < 3 && keeping.Count < 5);


            itemIndex = GetComputerScorecardItem(cScorecard, keeping);
            cScorecard[itemIndex] = Score(keeping, itemIndex);
            cScorecardCount++;
        }

        #endregion

        #region User Play Methods

// moves the dice that the user want to keep from the rolling data structure to the keeping data structure
// takes the rolling data structure and the keeping data structure as parameters
        private static void GetKeeping(List<int> rolling, List<int> keeping)
        {
            int userChoice;
            bool isint;
            var udice = 0;
            do
            {
                Console.Write($"Do you want to keep dice: #{udice + 1}? y/n ");
                var userinput = Console.ReadLine();
                if (userinput == "y") keeping.Add(rolling[udice]);

                udice++;
            } while (udice < rolling.Count);

            rolling.Clear();
        }

// on the last roll moves the dice that the user just rolled into the data structure for the dice that the user is keeping
        private static void MoveRollToKeep(List<int> rolling, List<int> keeping)
        {
            // iterate through the rolling data structure and copy each item into the keeping data structure
            for (var i = 0; i < rolling.Count; i++) keeping.Add(rolling[i]);

            rolling.Clear();
        }

        // asks the user which item on the scorecard they want to score 
        // must make sure that the scorecard doesn't already have a value for that item
        // remember that the scorecard is initialized with -1 in each item
        // takes a scorecard data structure as it's parameter 
        private static int GetScorecardItem(int[] scorecard)
        {
            var itemIndex = -1;
            do
            {
                Console.Write("Which item do you want to score? ");
                itemIndex = int.Parse(Console.ReadLine());
            } while (scorecard[itemIndex] != -1);

            return itemIndex;
        }

        // implements the user's turn
        // takes the user's scorecard data structure and the user's move count as parameters.  Both will be altered by the method.
        private static void UserPlay(int[] uScorecard, ref int userMoves)
        {
            //declare a data structure for the dice that the user is rolling
            var userRoll = new List<int>();
            //declare a data structure for the dice that the user is keeping
            var userKeep = new List<int>();

            //declare a variable for the number of rolls
            var numRolls = 0;
            //declare a variable for the scorecard item that the user wants to score on this turn
            int scorecardItem;

            //do
            do
            {
                var diceToRoll = 5 - userKeep.Count;
                //Call Roll
                Roll(diceToRoll, userRoll);
                //increment the number of rolls
                numRolls++;
                //display a message
                //Call DisplayDice
                Console.WriteLine("The dice you rolled: ");
                DisplayDice(userRoll);
                //if the number of rolls is < 3
                if (numRolls < 3)
                    //Call GetKeeping
                    GetKeeping(userRoll, userKeep);
                //else
                else
                    //Call MoveRollToKeeping
                    MoveRollToKeep(userRoll, userKeep);

                //end if
                //Call DisplayDice
                Console.WriteLine("The dice you are keeping: ");
                DisplayDice(userKeep);
                //while the number of rolls < 3 and the number of dice the user is keeping is < 5
            } while (numRolls < 3 && userKeep.Count < 5);

            //Call GetScorecardItem
            scorecardItem = GetScorecardItem(uScorecard);
            //*Call Score
            uScorecard[scorecardItem] = Score(userKeep, scorecardItem);
            //Increment the scorecard count
            userMoves++;
        }

        #endregion

        #region Scoring Methods

        // counts how many of a specified value are in the set of dice
        // takes the value that you're counting and the data structure containing the set of dice as it's parameter
        // returns the count
        private static int Count(int value, List<int> dice)
        {
            var count = 0;
            foreach (var num in dice)
                if (num == value)
                    count++;

            return count;
        }

        // counts the number of ones, twos, ... sixes in the set of dice
        // takes a data structure for a set of dice as it's parameter
        // returns a data structure that contains the count for each dice value
        private static List<int> GetCounts(List<int> dice)
        {
            var numCounts = new List<int>();
            for (var i = 1; i < 7; i++)
            {
                var num = Count(i, dice);
                numCounts.Add(num);
            }

            return numCounts;
        }

        // adds the value of all of the dice based on the counts
        // takes a data structure that represents all of the counts as a parameter
        private static int Sum(List<int> count)
        {
            var sum = 0;
            // you can uncomment this code once you have declared the parameter
            for (var i = 1; i < 7; i++)
            {
                sum += i * count[i - 1];
            }
            
            return sum;
        }

        // determines if you have a specified count based on the counts
        // takes a data structure that represents all of the counts as a parameter
        private static bool HasCount(List<int> counts, int howMany)
        {
            //you can uncomment this when you declare the parameter
            foreach (var count in counts)
                if (howMany == count)
                    return true;

            return false;
        }

        // chance is the sum of the dice
        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreChance(List<int> counts)
        {
            return Sum(counts);
        }

        // calculates the score for ONES given the set of counts (from GetCounts)
        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreOnes(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[0] * 1;
        }

        // WRITE ALL OF THESE: ScoreTwos, ScoreThrees, ScoreFours, ScoreFives, ScoreSixes
        private static int ScoreTwos(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[1] * 2;
        }

        private static int ScoreThrees(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[2] * 3;
        }

        private static int ScoreFours(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[3] * 4;
        }

        private static int ScoreFives(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[4] * 5;
        }

        private static int ScoreSixes(List<int> counts)
        {
            // you can comment out this line when you have declared the parameters
            return counts[5] * 6;
        }

        // scores 3 of a kind.  4 of a kind or 5 of a kind also can be used for 3 of a kind
        // the sum of the dice are used for the score
        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreThreeOfAKind(List<int> counts)
        {
            if (HasCount(counts, 3) || HasCount(counts, 4) || HasCount(counts, 5))
                return Sum(counts);
            return 0;
        }

        // WRITE ALL OF THESE: ScoreFourOfAKind, ScoreYahtzee - a yahtzee is worth 50 points

        // scores 4 of a kind.  5 of a kind also can be used for 4 of a kind
        // the sum of the dice are used for the score
        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreFourOfAKind(List<int> counts)
        {
            if (HasCount(counts, 4) || HasCount(counts, 5))
                return Sum(counts);
            return 0;
        }

        // scores 5 of a kind "Yahtzee"
        // the sum of the dice are used for the score
        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreYahtzee(List<int> counts)
        {
            if (HasCount(counts, 5))
                return 50;
            return 0;
        }


        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreFullHouse(List<int> counts)
        {
            //you can uncomment this code once you declare the parameter
            if (HasCount(counts, 2) && HasCount(counts, 3))
                return 25;
            return 0;
        }

        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreSmallStraight(List<int> counts)
        {
            //you can uncomment this code once you declare the parameter
            for (var i = 2; i <= 3; i++)
                if (counts[i] == 0)
                    return 0;

            if ((counts[0] >= 1 && counts[1] >= 1) ||
                (counts[1] >= 1 && counts[4] >= 1) ||
                (counts[4] >= 1 && counts[5] >= 1))
                return 30;
            return 0;
        }

        // takes a data structure that represents all of the counts as a parameter
        private static int ScoreLargeStraight(List<int> counts)
        {
            //you can uncomment this code once you declare the parameter
            for (var i = 1; i <= 4; i++)
                if (counts[i] == 0)
                    return 0;

            if (counts[0] == 1 || counts[5] == 1)
                return 40;
            return 0;
        }

        // scores a score card item based on the set of dice
        // takes an integer which represent the scorecard item as well as a data structure representing a set of dice as parameters
        private static int Score(List<int> dice, int whichElement)
        {
            //you can uncomment this code once you declare the parameter
            var counts = GetCounts(dice);
            switch (whichElement)
            {
                case 0:
                    return ScoreOnes(counts);
                case 1:
                    return ScoreTwos(counts);
                case 2:
                    return ScoreThrees(counts);
                case 3:
                    return ScoreFours(counts);
                case 4:
                    return ScoreFives(counts);
                case 5:
                    return ScoreSixes(counts);
                case 6:
                    return ScoreThreeOfAKind(counts);
                case 7:
                    return ScoreFourOfAKind(counts);
                case 8:
                    return ScoreFullHouse(counts);
                case 9:
                    return ScoreSmallStraight(counts);
                case 10:
                    return ScoreLargeStraight(counts);
                case 11:
                    return ScoreChance(counts);
                case 12:
                    return ScoreYahtzee(counts);
                default:
                    return 0;
            }
        }

        #endregion
    }
}