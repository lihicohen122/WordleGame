using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02_UI
{
    internal class GameInterface
    {
        private GameManager m_GameManager;
        private UItoLogicMapper m_UiEncoder;
        private Board m_Board;
        private InputValidation m_Input;

        public void RunGame()
        {
            printWelcomeMessage();
            startGameSession();
        }

        private static void printWelcomeMessage()
        {
            Console.WriteLine("Let's start Playing!");
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
            m_Input = new InputValidation();
            InputValidation.InitPackage initPackage = m_Input.GetInitPackage();
            m_GameManager = new GameManager(initPackage.UserChosenGuessCount);
            m_UiEncoder = new UItoLogicMapper(m_GameManager);
            m_Board = new Board();

            runGameLoop();
            printGameResult();
        }

        private bool isPlayingAgain()
        {
            return m_Input.StartNewGameQuestion();
        }

        private void runGameLoop()
        {
            while (!m_GameManager.IsGameOverChecker())
            {
                clearScreen();
                displayBoard(m_Board, m_GameManager.GetUserChosenNumberOfGuesses() + 1);
                string userGuess = m_Input.GetUserGuess();
                List<int> guessAsList = m_UiEncoder.MapUserInputToLogicParameters(userGuess).ToList();

                m_GameManager.ProcessGuess(guessAsList.ToArray());
                m_Board.AddGuess(guessAsList);
                int[] result = m_GameManager.GetResult();

                m_Board.AddResult(result[0], result[1]);
            }
        }

        private void printGameResult()
        {
            clearScreen();
            displayBoard(m_Board, m_GameManager.GetUserChosenNumberOfGuesses() + 1);

            if (m_GameManager.IsWinner())
            {
                int stepsTaken = m_GameManager.GetUserCurrentGuessCount();
                Console.WriteLine("GREAT SUCCESS! You Guessed after {0} steps!", stepsTaken);
            }
            else
            {
                string correctCode = m_UiEncoder.MapLogicParametersToUi(m_GameManager.GetCurrentWordToGuess());
                Console.WriteLine("You lost! No more guesses allowed");
                Console.WriteLine("The correct code was: {0}", correctCode);
            }

        }

        private static void displayBoard(Board i_Board, int i_MaxRows)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Current board status:");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendLine("| Pins:      | Result:   |");
            sb.AppendLine("|============|===========|");
            sb.AppendLine(string.Format("| {0,-11}| {1,-10}|", "# # # #", string.Empty));
            sb.AppendLine("|============|===========|");

            for (int i = 0; i < i_MaxRows - 1; i++)
            {
                string guessStr = string.Empty;
                string resultStr = string.Empty;

                if (i < i_Board.Guesses.Count)
                {
                    guessStr = formatGuess(i_Board.Guesses[i]);
                }

                if (i < i_Board.Results.Count)
                {
                    resultStr = formatResult(i_Board.Results[i]);
                }

                sb.AppendLine(string.Format("| {0,-11}| {1,-10}|", guessStr, resultStr));
                sb.AppendLine("|============|===========|");
            }

            Console.WriteLine(sb.ToString());
        }

        private static string formatGuess(List<int> i_UserGuessAsList)
        {
            string result;

            if (i_UserGuessAsList == null || i_UserGuessAsList.Count == 0 || i_UserGuessAsList[0] == -1)
            {
                result = "# # # #";
            }
            else
            {
                List<string> letters = i_UserGuessAsList.ConvertAll(i_Num => ((char)('A' + i_Num)).ToString());
                result = string.Join(" ", letters);
            }

            return result;
        }

        private static string formatResult(Result i_Result)
        {
            string result = string.Empty;

            if (i_Result.ExactGuess != 0 || i_Result.MisplacedGuess != 0)
            {
                StringBuilder sb = new StringBuilder(i_Result.ExactGuess + i_Result.MisplacedGuess);

                sb.Append('V', i_Result.ExactGuess);
                sb.Append('X', i_Result.MisplacedGuess);

                result = sb.ToString();
            }

            return result;
        }

        private static void clearScreen()
        {
            Console.Clear();
        }
    }
}
