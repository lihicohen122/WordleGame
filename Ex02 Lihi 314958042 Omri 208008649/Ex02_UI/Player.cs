namespace Ex02_UI
{
    internal class Player
    {
        internal int CurrentGuessNumber { get; private set; }
        internal int[] CurrentGuess { get; set; }
        internal int TotalGuessesChosen { get; set; }
        internal bool IsWinner { get; set; }

        internal Player (int i_NumGuessesChosen)
        {
            TotalGuessesChosen = i_NumGuessesChosen;
            CurrentGuessNumber = 0;
            CurrentGuess = null;
            IsWinner = false;
        }

        internal void RaisePlayerGuessCounter()
        {
            CurrentGuessNumber++;
        }
    }
}
