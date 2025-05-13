using System;
using System.Runtime.CompilerServices;

namespace Ex02_UI
{
  
    public class GameManager
    {
        //private static bool forNow = true;
        
        private Player m_PPlayer;

        private bool m_isGameOver;

        private readonly string r_CurrentWordToGuess;
        private string m_Result;
        private const int k_SizeOfGeneratedWordToGuess = 4;
       

        public GameManager(int i_UserChosenGuessCount)
        {
            m_PPlayer = new Player(i_UserChosenGuessCount);
            r_CurrentWordToGuess = randomizeWord();
            m_isGameOver = false;
            m_Result = null;
        }

        public string GetResult()
        {
            return m_Result;
        }

        public int getUserChosenNumberOfGuesses()
        {
            return m_PPlayer.MTotalGuessesChosen;
        }
        private string randomizeWord() //static
        {
            char[] iPossibleLetters = {'A', 'B', 'C', 'D', 'E','F','G','H'};
            Random rand = new Random();
            string randomizedResult="";
            int remainingLettersToRandomize = 8;

            for(int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                int randomizedIndex = rand.Next(remainingLettersToRandomize);
                randomizedResult += iPossibleLetters[randomizedIndex];

                (iPossibleLetters[randomizedIndex], iPossibleLetters[remainingLettersToRandomize - 1]) =
                    (iPossibleLetters[remainingLettersToRandomize - 1], iPossibleLetters[randomizedIndex]);

                remainingLettersToRandomize--;
            }

            return randomizedResult;
        }

        public void ProcessGuess(string input)
        {
            m_PPlayer.MCurrentGuess=input;

            m_Result = checkPlayerGuess();

            m_isGameOver = IsGameOverChecker();
        }

        private string checkPlayerGuess()
        {
            string currentPlayerGuess = m_PPlayer.MCurrentGuess;
            int V_counter = 0;
            int X_counter = 0;

            bool[] randomizedWordMatched = new bool[k_SizeOfGeneratedWordToGuess]; // Change to - letterMatch
            bool[] playerGuessMatched = new bool[k_SizeOfGeneratedWordToGuess]; //Change to - exactPositionMatch

            for(int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                if(currentPlayerGuess[i] == r_CurrentWordToGuess[i])
                {
                    V_counter++;
                    randomizedWordMatched[i] = true;
                    playerGuessMatched[i] = true;
                }
            }

            for(int j = 0; j < k_SizeOfGeneratedWordToGuess; j++)
            {
                if(playerGuessMatched[j])
                    continue;

                for(int k = 0; k < k_SizeOfGeneratedWordToGuess; k++)
                {
                    if (randomizedWordMatched[k])
                        continue;

                    if(currentPlayerGuess[j] == r_CurrentWordToGuess[k])
                    {
                        X_counter++;
                        randomizedWordMatched[k]=true;
                        break;
                    }
                }
            }

            string resultAfterCheck = buildResponseAfterCheck(V_counter, X_counter);

            return resultAfterCheck;
        }
        private static string buildResponseAfterCheck(int V_counter, int X_counter)
        {
            string response = new string('V', V_counter) + new string('X', X_counter);
            return response;
        }

        public bool IsGameOverChecker()
        {
            if(!isRemainingGuesses())
            {
                m_isGameOver=true;
                m_PPlayer.MisWinner = false;
            }

            if(isGuessMatch())
            {
                m_isGameOver = true;
                m_PPlayer.MisWinner = true;
            }

            return m_isGameOver;
        }

        private bool isGuessMatch()
        {
            //might want to change both returns to use a flag.
            //then we would have a single return.

            bool isGuessMatch = m_Result != null;


            for (int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                if(m_Result!=null && m_Result[i] != 'V')
                {
                    isGuessMatch = false;
                    break;
                }
            }

            return isGuessMatch;
        }


        private bool isRemainingGuesses()
        {
            return m_PPlayer.MCurrentGuessNumber < m_PPlayer.MTotalGuessesChosen;
        }

        public bool IsWinner()
        {
            return m_PPlayer.MisWinner;
        }




    }
}