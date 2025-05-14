using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02_UI
{
    internal class UItoLogicMapper
    {
        GameManager m_Manager;

        public UItoLogicMapper(GameManager i_CurrentGameManager)
        {
            m_Manager = i_CurrentGameManager;
        }

        public int[] MapUserInputToLogicParameters(string i_Input)
        {
            int[] translatedWordToLogic = new int[4];

            for (int i = 0; i < i_Input.Length; i++)
            {
                char currentLetterBeingTranslated = i_Input[i];
                translatedWordToLogic[i] = currentLetterBeingTranslated - 'A';
            }

            return translatedWordToLogic;
        }


        public string MapLogicParametersToUi(int[] i_Input)
        {
            StringBuilder TranslatedWord = new StringBuilder(4);

            foreach (int number in i_Input)
            {
                // Convert the integer back to a character
                TranslatedWord.Append((char)(number + 'A'));
            }

            // Return the resulting string
            return TranslatedWord.ToString();
        }

        public string TranslateResultToUi(int[] m_Result)
        {
            StringBuilder TranslatedResult = new StringBuilder(4);

            int exactMatches = m_Result[0];
            int misplacedMatches = m_Result[1];

            // Append 'V's for exact matches
            for (int i = 0; i < exactMatches; i++)
            {
                TranslatedResult.Append('V');
            }

            // Append 'X's for misplaced matches
            for (int i = 0; i < misplacedMatches; i++)
            {
                TranslatedResult.Append('X');
            }

            return TranslatedResult.ToString();
        }

    }
}
