namespace Ex02
{
    internal class Result
    {
        public int ExactGuess { get; }
        public int MisplacedGuess { get; }

        public Result(int i_Exact, int i_Misplaced)
        {
            ExactGuess = i_Exact;
            MisplacedGuess = i_Misplaced;
        }
    }
}

