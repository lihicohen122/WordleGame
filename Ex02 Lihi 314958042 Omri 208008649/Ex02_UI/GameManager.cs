using System;

namespace Ex02_UI
{
    public class GameManager
    {
        private readonly Player r_PPlayer;
        private bool m_IsGameOver;
        private readonly int[] r_CurrentWordToGuess; 
        private int[] m_Result;
        private const int k_SizeOfGeneratedWordToGuess = 4;
        
        public GameManager (int i_UserChosenGuessCount)
        {
            r_PPlayer = new Player(i_UserChosenGuessCount);
            r_CurrentWordToGuess = randomizeWord();
            m_IsGameOver = false;
            m_Result = null;
        }

        public int[] GetCurrentWordToGuess()
        {
            return r_CurrentWordToGuess;
        }

        public int[] GetResult()
        {
            return m_Result;
        }

        public int GetUserChosenNumberOfGuesses()
        {
            return r_PPlayer.TotalGuessesChosen;
        }

        public int GetUserCurrentGuessCount()
        {
            return r_PPlayer.CurrentGuessNumber;
        }

        private int[] randomizeWord() 
        {
            Random rand = new Random();
            int[] randomizedResult = new int[k_SizeOfGeneratedWordToGuess];
            int remainingLettersToRandomize = 8; 
            int[] iPossibleNumbers = { 0,1,2,3,4,5,6,7 }; //Representing {A,B,C,D,E,F,G,H}

            for (int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                int randomizedIndex = rand.Next(remainingLettersToRandomize);

                randomizedResult[i] = iPossibleNumbers[randomizedIndex];
                iPossibleNumbers[randomizedIndex] = iPossibleNumbers[remainingLettersToRandomize - 1];
                remainingLettersToRandomize--;
            }

            return randomizedResult;
        }

        public void ProcessGuess(int[] i_Input)
        {
            r_PPlayer.CurrentGuess = i_Input;
            r_PPlayer.RaisePlayerGuessCounter();
            m_Result = checkPlayerGuess();
            m_IsGameOver = IsGameOverChecker();
        }

        private int[] checkPlayerGuess()
        {
            int[] currentPlayerGuess = r_PPlayer.CurrentGuess;
            int exactMatchesCounterV = 0;
            int misplacedMatchesCounterX = 0;
            bool[] randomizedWordMatched = new bool[k_SizeOfGeneratedWordToGuess]; 
            bool[] playerGuessMatched = new bool[k_SizeOfGeneratedWordToGuess]; 

            for (int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                if(currentPlayerGuess[i] == r_CurrentWordToGuess[i])
                {
                    exactMatchesCounterV++;
                    randomizedWordMatched[i] = true; 
                    playerGuessMatched[i] = true; 
                }
            }

            for (int j = 0; j < k_SizeOfGeneratedWordToGuess; j++)
            {
                if (playerGuessMatched[j])
                    continue;

                for (int k = 0; k < k_SizeOfGeneratedWordToGuess; k++)
                {
                    if (randomizedWordMatched[k])
                        continue;

                    if (currentPlayerGuess[j] == r_CurrentWordToGuess[k])
                    {
                        misplacedMatchesCounterX++;
                        randomizedWordMatched[k]=true;
                        break;
                    }
                }
            }

            int[] resultAfterCheck = new int[2];
            resultAfterCheck[0] = exactMatchesCounterV;
            resultAfterCheck[1] = misplacedMatchesCounterX;
            
            return resultAfterCheck;
        }

        public bool IsGameOverChecker()
        {
            if (!isRemainingGuesses())
            {
                m_IsGameOver = true;
                r_PPlayer.IsWinner = false;
            }

            if (isGuessMatch())
            {
                m_IsGameOver = true;
                r_PPlayer.IsWinner = true;
            }

            return m_IsGameOver;
        }

        private bool isGuessMatch()
        {
            bool isGuessMatch = m_Result != null && m_Result[0] == 4;

            return isGuessMatch;
        }


        private bool isRemainingGuesses()
        {
            return r_PPlayer.CurrentGuessNumber < r_PPlayer.TotalGuessesChosen;
        }

        public bool IsWinner()
        {
            return r_PPlayer.IsWinner;
        }

    }
}
