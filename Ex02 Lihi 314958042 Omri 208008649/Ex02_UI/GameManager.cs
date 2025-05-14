using System;
using System.Runtime.CompilerServices;

namespace Ex02_UI
{
  
    public class GameManager
    {
        
        private Player m_PPlayer;

        private bool m_isGameOver;

        //private readonly string r_CurrentWordToGuess;
        public readonly int[] r_CurrentWordToGuess; //CHANGE TO PRIVATE
        private int[] m_Result;
        private const int k_SizeOfGeneratedWordToGuess = 4;
       

        public GameManager(int i_UserChosenGuessCount)
        {
            m_PPlayer = new Player(i_UserChosenGuessCount);
            r_CurrentWordToGuess = randomizeWord();
            m_isGameOver = false;
            m_Result = null;
        }

        public int[] GetResult()
        {
            return m_Result;
        }

        public int getUserChosenNumberOfGuesses()
        {
            return m_PPlayer.MTotalGuessesChosen;
        }

        public int GetUserCurrentGuessCount()
        {
            return m_PPlayer.MCurrentGuessNumber;
        }

        private int[] randomizeWord() //static
        {
            
            //Random rand = new Random();
            //string randomizedResult="";
            //int remainingLettersToRandomize = 8;
            Random rand = new Random();
            int[] randomizedResult = new int[k_SizeOfGeneratedWordToGuess];
            int remainingLettersToRandomize = 8; 
            int[] iPossibleNumbers = { 0,1,2,3,4,5,6,7 }; //Representing {A,B,C,D,E,F,G,H}

            for (int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                int randomizedIndex = rand.Next(remainingLettersToRandomize);
                //randomizedResult[i] = randomizedIndex;

                randomizedResult[i] = iPossibleNumbers[randomizedIndex];

                //swap possible numbers to prevent repetition
                iPossibleNumbers[randomizedIndex] = iPossibleNumbers[remainingLettersToRandomize - 1];


                remainingLettersToRandomize--;
            }

            return randomizedResult;
        }

        public void ProcessGuess(int[] i_Input)
        {
            //SHOULD ALREADY BE TRANSLATED
            m_PPlayer.MCurrentGuess = i_Input;
            m_PPlayer.raisePlayerGuessCounter();

            m_Result = checkPlayerGuess();

            m_isGameOver = IsGameOverChecker();
        }

        private int[] checkPlayerGuess()
        {
            int[] currentPlayerGuess = m_PPlayer.MCurrentGuess;
            int exactMatchesCounterV = 0;
            int misplacedMatchesCounterX = 0;


            bool[] randomizedWordMatched = new bool[k_SizeOfGeneratedWordToGuess]; 
            bool[] playerGuessMatched = new bool[k_SizeOfGeneratedWordToGuess]; 

            for(int i = 0; i < k_SizeOfGeneratedWordToGuess; i++)
            {
                if(currentPlayerGuess[i] == r_CurrentWordToGuess[i])
                {
                    exactMatchesCounterV++;
                    randomizedWordMatched[i] = true; //Mark the word position as matched to prevent recounting.
                    playerGuessMatched[i] = true; //Mark the guess position as matched.
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
            bool isGuessMatch = false;

            //if(m_Result!=null && m_Result[0] == 4)
            // {
            //  isGuessMatch=true;
            //}
            isGuessMatch = m_Result != null && m_Result[0] == 4;


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