using System.Text;

namespace Ex02
{
    internal class UItoLogicMapper
    {
        private GameManager m_Manager;

        public UItoLogicMapper (GameManager i_CurrentGameManager)
        {
            m_Manager = i_CurrentGameManager;
        }

        public int[] MapUserInputToLogicParameters(string i_UserInputStr)
        {
            int[] translatedWordToLogic = new int[4];

            for (int i = 0; i < i_UserInputStr.Length; i++)
            {
                char currentLetterBeingTranslated = i_UserInputStr[i];
                translatedWordToLogic[i] = currentLetterBeingTranslated - 'A';
            }

            return translatedWordToLogic;
        }

        public string MapLogicParametersToUi(int[] i_UserInputArrInt)
        {
            StringBuilder translatedWord = new StringBuilder(4);

            foreach (int number in i_UserInputArrInt)
            {
                translatedWord.Append((char)(number + 'A'));
            }

            return translatedWord.ToString();
        }

    }
}
