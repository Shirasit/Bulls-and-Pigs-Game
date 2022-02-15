using System;
using System.Linq;

namespace PigsAndBullsLogic
{
    public class GameLogic
    {
        public const int k_LengthOfGuess = 4;
        private const int k_NumberOfSpacesBetweenLetters = 1;
        private const int k_ComputerSelectionRow = 0;
        private const int k_NumberOfOptionalGuesses = 8;
        private const char k_StartRangeOfLetters = 'A';
        private readonly Random r_Random;
        private Board m_BoardOfGame;
        private char[] m_ComputerRandomGuess = new char[k_LengthOfGuess];
        private bool m_IsPlayerWin = false;
        private bool m_IsPlayerLose = false;
        private int m_MaximunNumberOfGuesses;
        private int m_NumberOfCurrentGuess = 0;
        private int m_NumberOfBullsInGuess = 0;
        private int m_NumberOfPigsInGuess = 0;

        public GameLogic()
        {
            r_Random = new Random();
        }

        public Board GameBoard
        {
            get { return m_BoardOfGame; }
        }

        public int MaximumNumberOfGuesses
        {
            get { return m_MaximunNumberOfGuesses; }
            set { m_MaximunNumberOfGuesses = value; }
        }

        public int NumberOfCurrentGuess
        {
            get { return m_NumberOfCurrentGuess; }
        }

        public bool IsPlayerWon
        {
            get { return m_IsPlayerWin; }
        }

        public bool IsPlayerLose
        {
            get { return m_IsPlayerLose; }
        }

        public char[] ComputerRandomGuessSelection
        {
            get { return m_ComputerRandomGuess; }
        }

        public int NumberOfBullsInGuess
        {
            get { return m_NumberOfBullsInGuess; }
        }

        public int NumberOfPigsInGuess
        {
            get { return m_NumberOfPigsInGuess; }
        }

        public void GameManager(char[] i_ListOfUserGuess)
        {
            countPigsAndBullsInUserGuess(i_ListOfUserGuess);
            if (m_IsPlayerLose)
            {
                showComputerSelection();
            }
        }

        public void InitializeGame(int i_MaximumNumberOfGuesses)
        {
            m_BoardOfGame = new Board(i_MaximumNumberOfGuesses, k_LengthOfGuess * 2);
            m_MaximunNumberOfGuesses = i_MaximumNumberOfGuesses;
            m_NumberOfCurrentGuess = 0;
            m_IsPlayerWin = false;
            m_IsPlayerLose = false;
          
            ComputerRandomGuess();
        }

        private void countPigsAndBullsInUserGuess(char[] i_UserGuess)
        {
            int indexOfUserGuess;

            m_NumberOfCurrentGuess++;
            m_NumberOfBullsInGuess = 0;
            m_NumberOfPigsInGuess = 0;
            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                indexOfUserGuess = (k_NumberOfSpacesBetweenLetters + 1) * i;
                if (m_ComputerRandomGuess.Contains(i_UserGuess[indexOfUserGuess]))
                {
                    if (m_ComputerRandomGuess[i].Equals(i_UserGuess[indexOfUserGuess]))
                    {
                        m_NumberOfBullsInGuess++;
                    }
                    else
                    {
                        m_NumberOfPigsInGuess++;
                    }
                }
            }

            checkIfPlayerWin();
            checkIfPlayerLose();
        }

        private void checkIfPlayerLose()
        {
            if (m_NumberOfCurrentGuess == m_MaximunNumberOfGuesses && !m_IsPlayerWin)
            {
                m_IsPlayerLose = true;
            }
        }

        private void checkIfPlayerWin()
        {
            if (m_NumberOfBullsInGuess.Equals(k_LengthOfGuess))
            {
                m_IsPlayerWin = true;
            }
        }

        public bool IsGameOver()
        {
            bool isGameOver = false;

            if (m_IsPlayerLose || m_IsPlayerWin)
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        private void ComputerRandomGuess()
        {
            int[] OptionalLettersGuess = new int[k_NumberOfOptionalGuesses];
            int randomNum;

            for (int i = 0; i < k_LengthOfGuess; i++)
            {
                randomNum = r_Random.Next(0, k_NumberOfOptionalGuesses);
                while (OptionalLettersGuess[randomNum] > 0)
                {
                    randomNum = r_Random.Next(0, k_NumberOfOptionalGuesses);
                }

                m_ComputerRandomGuess[i] = Convert.ToChar(randomNum + k_StartRangeOfLetters);
                OptionalLettersGuess[randomNum]++;
            }
        }

        private void showComputerSelection()
        {
            for (int column = 0; column < k_LengthOfGuess; column++)
            {
                m_BoardOfGame.SetUserGuessBoard(k_ComputerSelectionRow, column, m_ComputerRandomGuess[column]);
            }
        }
    }
}
