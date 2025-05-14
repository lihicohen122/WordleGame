using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_UI
{
    internal class Player
    {
        public int MCurrentGuessNumber { get; internal set;}
        public int[] MCurrentGuess { get; internal set; }
        public int MTotalGuessesChosen { get; set; }

        public bool MisWinner { get; set; }

        public Player(int i_NumGuessesChosen)
        {
            MTotalGuessesChosen = i_NumGuessesChosen;
            MCurrentGuessNumber = 0;
            MCurrentGuess = null;
            MisWinner = false;
        }


        public void raisePlayerGuessCounter()
        {
            MCurrentGuessNumber++;
        }
    }
}
