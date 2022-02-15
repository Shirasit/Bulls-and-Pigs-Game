namespace PigsAndBullsLogic
{
    public class Board
    { 
        private readonly int r_LengthOfGameBoard;
        private readonly int r_WidthOfGameBoard;
        private readonly char[,] r_UserGuessBoard;
        private readonly char[,] r_ScoreBoardOfUserGuess;
        
        public Board(int i_Length, int i_Width)
        {
            r_LengthOfGameBoard = i_Length + 1;
            r_WidthOfGameBoard = i_Width;
            r_UserGuessBoard = new char[r_LengthOfGameBoard, i_Width / 2];
            r_ScoreBoardOfUserGuess = new char[r_LengthOfGameBoard, i_Width / 2];
        }

        public int LengthOfGameBoard
        {
            get { return r_LengthOfGameBoard; }
        }

        public int WidthOfGameBoard
        {
            get { return r_WidthOfGameBoard; }
        }

        public char GetUserGuessBoard(int i_Row, int i_Column)
        {
            return r_UserGuessBoard[i_Row, i_Column];
        }

        public void SetUserGuessBoard(int i_Row, int i_Column, char value)
        {
            r_UserGuessBoard[i_Row, i_Column] = value;
        }

        public char GetScoreBoardOfUserGuess(int i_Row, int i_Column)
        {
            return r_ScoreBoardOfUserGuess[i_Row, i_Column];
        }

        public void SetScoreBoardOfUserGuess(int i_Row, int i_Column, char value)
        {
            r_ScoreBoardOfUserGuess[i_Row, i_Column] = value;
        }     
    }
}