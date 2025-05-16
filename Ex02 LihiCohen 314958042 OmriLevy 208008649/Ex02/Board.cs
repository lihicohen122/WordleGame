using System.Collections.Generic;

namespace Ex02
{
    internal class Board
    {
        private readonly List<List<int>> r_UserGuesses = new List<List<int>>();
        private readonly List<Result> r_Results = new List<Result>();
        internal IReadOnlyList<List<int>> Guesses => r_UserGuesses;
        internal IReadOnlyList<Result> Results => r_Results;

        internal void AddGuess(List<int> i_UserGuess)
        {
            r_UserGuesses.Add(i_UserGuess);
        }

        internal void AddResult(int i_ExactGuess, int i_MisplacedGuess)
        {
            r_Results.Add(new Result(i_ExactGuess, i_MisplacedGuess));
        }
    }
}