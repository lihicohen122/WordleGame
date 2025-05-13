using System.Collections.Generic;
using System.Text;

namespace Ex02_UI
{
    internal class Board
    {
        internal List<string> s_guesses = new List<string>();
        internal List<string> s_results = new List<string>();

        internal Board()
        {
            s_guesses.Add("# # # #");
            s_results.Add("");
        }

        internal string BuildBoardSnapshot(int i_UserChosenGuesses)
        {
            StringBuilder board = new StringBuilder();
            board.AppendLine("Current board status:\n");

            // Header
            board.AppendLine("| Pins:      | Result:   |");
            board.AppendLine("|============|===========|");

            for(int i = 0; i < i_UserChosenGuesses; i++) //HARD CODED
            {
                string pinLine = i < s_guesses.Count ? s_guesses[i] : "";
                string resultLine = i < s_results.Count ? s_results[i] : "";

                board.AppendLine(string.Format("| {0,-11}| {1,-10}|", pinLine, resultLine));
                board.AppendLine("|============|===========|");
            }
            
            return board.ToString();
        }

        internal void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }
    }
}