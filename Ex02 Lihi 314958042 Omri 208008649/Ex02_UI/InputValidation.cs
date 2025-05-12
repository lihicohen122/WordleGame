using System;

namespace Ex02_UI
{
    internal class InputValidation //this class is for UI prompts + validation only
    {
        internal InitPackage GetInitPackage()
        {
            int userChosenGuessNumber = getUserNumOfGuesses();
            return new InitPackage(userChosenGuessNumber);
        }

        internal class InitPackage
        {
            private readonly int r_UserChosenGuessCount;

            internal InitPackage(int i_UserChosenGuessCount)
            {
                r_UserChosenGuessCount = i_UserChosenGuessCount;
            }

            internal int UserChosenGuessCount => r_UserChosenGuessCount;
        }

        private static int getUserNumOfGuesses()
        {
            Console.WriteLine("please enter desired number of guesses (a number between 4-10): ");
            string numOfGuessesStr = Console.ReadLine();
            checkToQuit(numOfGuessesStr);
            int.TryParse(numOfGuessesStr, out int numOfGuessesInt);

            while (!isValidNumOfGuesses(numOfGuessesInt))
            {
                Console.Write("Invalid input. Please enter a desired number of guesses between 4 and 10: ");
                numOfGuessesStr = Console.ReadLine();
                checkToQuit(numOfGuessesStr);
                int.TryParse(numOfGuessesStr, out numOfGuessesInt);
            }

            return numOfGuessesInt;
        }

        private static bool isValidNumOfGuesses(int i_NumOfGuesses)
        {
            return i_NumOfGuesses <= 10 && i_NumOfGuesses >= 4;
        }

        internal string GetUserGuess() //we will use this for each round
        {
            Console.Write("Please type your guess or 'Q' to quit: ");
            string userGuess = Console.ReadLine();
            checkToQuit(userGuess); 

            while (!isValidGuess(userGuess))
            {
                Console.Write("Invalid guess. Please enter 4 letter string using A–H: ");
                userGuess = Console.ReadLine();
            }

            return userGuess;
        }

        private static void checkToQuit(string i_UserGuess)
        {
            if (i_UserGuess == "Q")
            {
                quitGame();
            }
        }

        private static void quitGame()
        {
            Console.WriteLine("\nGoodbye!");
            Environment.Exit(0);
        }

        private static bool isValidGuess(string i_UserGuess)
        {
            return isValidGuessLength(i_UserGuess) && isValidGuessCharacters(i_UserGuess);
        }

        private static bool isValidGuessLength(string i_UserGuess)
        {
            return i_UserGuess.Length == 4;
        }

        private static bool isValidGuessCharacters(string i_UserGuess)
        {
            bool isValidCharacters = true;

            foreach (char letter in i_UserGuess)
            {
                if (letter < 'A' || letter > 'H')
                {
                    isValidCharacters = false;
                    break;
                }
            }

            return isValidCharacters;
        }

        internal bool StartNewGameQuestion()
        {
            Console.Write("Do you want to start a new game? (Y/N, or Q to quit): ");
            string yesOrNoInput = Console.ReadLine();
            checkToQuit(yesOrNoInput);

            while (!isValidYesOrNoInput(yesOrNoInput))
            {
                Console.WriteLine("Invalid input. Please enter Y, N, or Q.");
                yesOrNoInput = Console.ReadLine();
            }

            return yesOrNoInput == "Y"; //if we are here we have "Y" or "N"
        }

        private static bool isValidYesOrNoInput(string i_Input)
        {
            return i_Input == "Y" || i_Input == "N";
        }

    }
}
