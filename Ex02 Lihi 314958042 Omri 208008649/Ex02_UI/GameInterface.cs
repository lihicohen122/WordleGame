using System;
using Ex02_Logic;

namespace Ex02_UI
{
    internal class GameInterface
    {

        //private GameManager m_GameManager;
        private readonly InputValidation r_Input = new InputValidation();

        public void RunGame()
        {
            printWelcomeMessage();
            startGameSession();
            ShowExitMessage();
        }

        private static void printWelcomeMessage()
        {
            Console.WriteLine("Lets start Playing!");
        }

        private void startGameSession()
        {
            bool startGame = true;

            while (startGame)
            {
                playSingleGame();
                startGame = isPlayingAgain();
            }
        }

        private void playSingleGame()
        {
            startRound();
        }

        private void startRound()
        {
            InputValidation.InitPackage initPackage = r_Input.GetInitPackage();
            //m_GameManager = new GameManager(initPackage.UserChosenGuessCount); //TODO: Initialize GameManager with user-chosen guess count

            runGameLoop();
            printGameResult();
        }

        private bool isPlayingAgain()
        {
            return r_Input.StartNewGameQuestion();
        }

        private void runGameLoop()
        {
            // Game loop will continue until the game is over
           // while (!m_GameManager.IsGameOver) // Use m_GameManager here
            {
                //ClearScreen();
                //DisplayCurrentBoard(); // Display current board

                string userGuess = r_Input.GetUserGuess(); // Validated guess or quit
                //m_GameManager.ProcessGuess(userGuess); // Process the user's guess in GameManager
            }
        }

        private void printGameResult()
        {
            //ClearScreen(); //from consoleUtils 
            //PrintBoard(); // Final state of the board

           // if (//m_GameManager.isWinner)
            {
                Console.WriteLine("GREAT SUCCESS! You Guessed after X steps!" ); //we need X from game manager or somewhere else in the logic. 
            }
           // else
            {
                Console.WriteLine("you lost! no more guesses allowed");
                Console.WriteLine($"The correct code was: XXXX"); //get the correct code from GameManager 
            }
        }

        //we also need to write V and X per guess. we need to think about this together because it's highly related to the logic. 


    }



    
}
