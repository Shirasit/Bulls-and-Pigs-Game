using System;
using System.Drawing;
using System.Windows.Forms;
using PigsAndBullsLogic;

namespace PigsAndBullsUI
{
    public partial class GameBoardForm : Form
    {
        private const int k_ButtonSizeOfComputerAndUserGuess = 40;
        private const int k_ButtonSizeOfScoreBoard = 15;
        private const int k_ButtonWidthOfSubmitGuess = 45;
        private const int k_ButtonHeightOfSubmitGuess = 20;
        private const int k_PositionOfStart = 12;
        private const char k_MediumOrchidColor = 'A';
        private const char k_RedColor = 'B';
        private const char k_LimeColor = 'C';
        private const char k_AquaColor = 'D';
        private const char k_BlueColor = 'E';
        private const char k_YellowColor = 'F';
        private const char k_MaroonColor = 'G';
        private const char k_WhiteColor = 'H';
        private const char k_EmptyCell = ' ';
        private readonly GameLogic r_GameLogic;
        private readonly StartForm r_StartForm;
        private readonly ColorSelsectionForm r_ColorSelectionForm;
        private readonly Button[] r_ButtonsOfComputerSelection;
        private readonly Button[,] r_ButtonsOfPlayerGuess;
        private readonly Button[] r_ButtonsOfSubmitGuess;
        private readonly Button[,] r_ButtonsOfScoreBoard;
        private readonly int r_NumberOfGuesses;

        public GameBoardForm()
        {
            InitializeComponent();
            r_StartForm = new StartForm();
            r_StartForm.ShowDialog();
            if (r_StartForm.DialogResult == DialogResult.OK)
            {
                r_GameLogic = new GameLogic();
                r_ColorSelectionForm = new ColorSelsectionForm();
                r_NumberOfGuesses = r_StartForm.MaxNumberOfGuesses;
                r_ButtonsOfComputerSelection = new Button[GameLogic.k_LengthOfGuess];
                r_ButtonsOfPlayerGuess = new Button[r_NumberOfGuesses, GameLogic.k_LengthOfGuess];
                r_ButtonsOfSubmitGuess = new Button[r_NumberOfGuesses];
                r_ButtonsOfScoreBoard = new Button[r_NumberOfGuesses, GameLogic.k_LengthOfGuess];
                Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void boardBuilding()
        {
            Point locationOfButton = new Point(k_PositionOfStart, k_PositionOfStart);

            createRowOfComputerSelectionButtons(ref locationOfButton);
            for (int i = 0; i < r_NumberOfGuesses; i++)
            {
                createRowOfUserGuessButtons(i, ref locationOfButton);
                createSubmitGuessButton(i, locationOfButton.X, locationOfButton.Y + (k_ButtonSizeOfComputerAndUserGuess / 4));
                locationOfButton.X += k_ButtonWidthOfSubmitGuess + 5;
                createRowOfScoreBoardButtons(i, ref locationOfButton);
                locationOfButton.X = k_PositionOfStart;
                locationOfButton.Y += k_ButtonSizeOfScoreBoard + 10;
            }
        }

        public void Run()
        {
            r_GameLogic.InitializeGame(r_NumberOfGuesses);
            boardBuilding();
            setSizeOfGameForm();
            enableAndDisableUserGuessButtons(r_GameLogic.NumberOfCurrentGuess, true);
        }

        private void setSpacesInCharArrayofGuesses(ref char[] io_CharArrayOfGuess)
        {
            for (int i = 1; i < io_CharArrayOfGuess.Length; i += 2)
            {
                io_CharArrayOfGuess[i] = k_EmptyCell;
            }
        }

        private void setLettersInCharArrayOfGuess(ref char[] io_CharArrayOfGuess, int i_Row)
        {
            int indexOfLetterUserGuess;

            for (int i = 0; i < GameLogic.k_LengthOfGuess; i++)
            {
                indexOfLetterUserGuess = i * 2;
                switch (r_ButtonsOfPlayerGuess[i_Row, i].BackColor.Name.ToString())
                {
                    case "MediumOrchid":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_MediumOrchidColor;
                        break;
                    case "Red":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_RedColor;
                        break;
                    case "Lime":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_LimeColor;
                        break;
                    case "Aqua":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_AquaColor;
                        break;
                    case "Blue":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_BlueColor;
                        break;
                    case "Yellow":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_YellowColor;
                        break;
                    case "Maroon":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_MaroonColor;
                        break;
                    case "White":
                        io_CharArrayOfGuess[indexOfLetterUserGuess] = k_WhiteColor;
                        break;
                }
            }
        }

        private void setSizeOfGameForm()
        {
            int newHeight = this.Size.Height + 10;
            int newWidth = this.Size.Width + 10;

            this.AutoSize = false;
            this.Size = new Size(newWidth, newHeight);
        }

        private void enableAndDisableUserGuessButtons(int i_Row, bool i_ToEnableButton)
        {
            for (int i = 0; i < GameLogic.k_LengthOfGuess; i++)
            {
                r_ButtonsOfPlayerGuess[i_Row, i].Enabled = i_ToEnableButton;
                r_ButtonsOfPlayerGuess[i_Row, i].TabStop = i_ToEnableButton;
            }

            r_ButtonsOfPlayerGuess[i_Row, 0].Focus();
        }

        private void setSubmitGuessButtonEnableOption(Button i_Button, int i_ButtonRow)
        {
            bool setSubmitGuessButtonEnable = false;

            if (isAllGuessButtonsInRowSelected(i_ButtonRow) && isAllGuessButtonsinRowWithDifferentColor(i_ButtonRow))
            {
                setSubmitGuessButtonEnable = true;
            }

            r_ButtonsOfSubmitGuess[i_ButtonRow].Enabled = setSubmitGuessButtonEnable;
            r_ButtonsOfSubmitGuess[i_ButtonRow].TabStop = setSubmitGuessButtonEnable;
        }

        private bool isAllGuessButtonsInRowSelected(int i_ButtonRow)
        {
            bool AllGuessButtonsInLineSelected = true;

            for (int j = 0; j < GameLogic.k_LengthOfGuess; j++)
            {
                if (r_ButtonsOfPlayerGuess[i_ButtonRow, j].BackColor == new Button().BackColor)
                {
                    AllGuessButtonsInLineSelected = false;
                    break;
                }
            }

            return AllGuessButtonsInLineSelected;
        }

        private bool isAllGuessButtonsinRowWithDifferentColor(int i_Row)
        {
            bool isAllGuessButtonsinRowWithDifferentColor = true;

            for (int j = 0; j < GameLogic.k_LengthOfGuess; j++)
            {
                for (int k = j + 1; k < GameLogic.k_LengthOfGuess; k++)
                {
                    if (r_ButtonsOfPlayerGuess[i_Row, j].BackColor == r_ButtonsOfPlayerGuess[i_Row, k].BackColor)
                    {
                        isAllGuessButtonsinRowWithDifferentColor = false;
                        break;
                    }
                }
            }

            return isAllGuessButtonsinRowWithDifferentColor;
        }

        private void showComputerSelection()
        {
            int row = 0;

            foreach (char charGuess in r_GameLogic.ComputerRandomGuessSelection)
            {
                switch (charGuess)
                {
                    case k_MediumOrchidColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.MediumOrchid;
                        break;
                    case k_RedColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Red;
                        break;
                    case k_LimeColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Lime;
                        break;
                    case k_AquaColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Aqua;
                        break;
                    case k_BlueColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Blue;
                        break;
                    case k_YellowColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Yellow;
                        break;
                    case k_MaroonColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.Maroon;
                        break;
                    case k_WhiteColor:
                        r_ButtonsOfComputerSelection[row].BackColor = Color.White;
                        break;
                }

                row++;
            }
        }

        private void updateButtonsBackgroundColorOfScoreBoard(int i_Row)
        {
            int column = 0;

            for (int i = 0; i < r_GameLogic.NumberOfBullsInGuess; i++, column++)
            {
                r_ButtonsOfScoreBoard[i_Row, column].BackColor = Color.Black;
            }

            for (int i = 0; i < r_GameLogic.NumberOfPigsInGuess; i++, column++)
            {
                r_ButtonsOfScoreBoard[i_Row, column].BackColor = Color.Yellow;
            }
        }

        private void buttonUserGuess_Click(object sender, EventArgs e)
        {
            Button guessButton = sender as Button;
            int buttonRow = r_GameLogic.NumberOfCurrentGuess;
            r_ColorSelectionForm.Location = new Point(this.Location.X - r_ColorSelectionForm.Size.Width, this.Location.Y + guessButton.Location.Y);
            r_ColorSelectionForm.ShowDialog();
            guessButton.BackColor = r_ColorSelectionForm.LastSelectedColor;

            setSubmitGuessButtonEnableOption(guessButton, buttonRow);
        }

        private void buttonSubmitGuess_Click(object sender, EventArgs e)
        {
            Button guessButton = sender as Button;
            int buttonRow = r_GameLogic.NumberOfCurrentGuess;
            char[] guessInput = createGuessCharArrayFromUserGuessButtonColor(buttonRow);

            enableAndDisableUserGuessButtons(buttonRow, false);
            r_GameLogic.GameManager(guessInput);
            guessButton.Enabled = false;
            updateButtonsBackgroundColorOfScoreBoard(buttonRow);
            if (!r_GameLogic.IsGameOver())
            {
                buttonRow++;
                enableAndDisableUserGuessButtons(buttonRow, true);
            }
            else
            {
                showComputerSelection();
            }
        }

        private void createUserGuessButton(int i_Row, int i_Column, int i_LocationX, int i_LocationY)
        {
            r_ButtonsOfPlayerGuess[i_Row, i_Column] = new Button();
            r_ButtonsOfPlayerGuess[i_Row, i_Column].Name = string.Format("buttonUserGuess{0},{1}", i_Row, i_Column);
            r_ButtonsOfPlayerGuess[i_Row, i_Column].Size = new Size(k_ButtonSizeOfComputerAndUserGuess, k_ButtonSizeOfComputerAndUserGuess);
            r_ButtonsOfPlayerGuess[i_Row, i_Column].Location = new Point(i_LocationX, i_LocationY);
            r_ButtonsOfPlayerGuess[i_Row, i_Column].FlatStyle = FlatStyle.Flat;
            r_ButtonsOfPlayerGuess[i_Row, i_Column].TabStop = false;
            r_ButtonsOfPlayerGuess[i_Row, i_Column].Enabled = false;
            r_ButtonsOfPlayerGuess[i_Row, i_Column].Click += buttonUserGuess_Click;
            this.Controls.Add(r_ButtonsOfPlayerGuess[i_Row, i_Column]);
        }

        private void createComputerGuessButton(int i_Index, int i_LocationX, int i_LocationY)
        {
            r_ButtonsOfComputerSelection[i_Index] = new Button();
            r_ButtonsOfComputerSelection[i_Index].Name = string.Format("buttonComputerGuess{0}", i_Index);
            r_ButtonsOfComputerSelection[i_Index].Size = new Size(k_ButtonSizeOfComputerAndUserGuess, k_ButtonSizeOfComputerAndUserGuess);
            r_ButtonsOfComputerSelection[i_Index].Location = new Point(i_LocationX, i_LocationY);
            r_ButtonsOfComputerSelection[i_Index].BackColor = Color.Black;
            r_ButtonsOfComputerSelection[i_Index].FlatStyle = FlatStyle.Flat;
            r_ButtonsOfComputerSelection[i_Index].TabStop = false;
            r_ButtonsOfComputerSelection[i_Index].Enabled = false;
            this.Controls.Add(r_ButtonsOfComputerSelection[i_Index]);
        }

        private void createSubmitGuessButton(int i_Index, int i_LocationX, int i_LocationY)
        {
            r_ButtonsOfSubmitGuess[i_Index] = new Button();
            r_ButtonsOfSubmitGuess[i_Index].Name = string.Format("buttonSubmitGuess{0}", i_Index);
            r_ButtonsOfSubmitGuess[i_Index].Text = "->>";
            r_ButtonsOfSubmitGuess[i_Index].Size = new Size(k_ButtonWidthOfSubmitGuess, k_ButtonHeightOfSubmitGuess);
            r_ButtonsOfSubmitGuess[i_Index].Location = new Point(i_LocationX, i_LocationY);
            r_ButtonsOfSubmitGuess[i_Index].FlatStyle = FlatStyle.Flat;
            r_ButtonsOfSubmitGuess[i_Index].TabStop = false;
            r_ButtonsOfSubmitGuess[i_Index].Enabled = false;
            r_ButtonsOfSubmitGuess[i_Index].Click += buttonSubmitGuess_Click;
            this.Controls.Add(r_ButtonsOfSubmitGuess[i_Index]);
        }

        private void createScoreBoardButtons(int i_Row, int i_Column, int i_LocationX, int i_LocationY)
        {
            r_ButtonsOfScoreBoard[i_Row, i_Column] = new Button();
            r_ButtonsOfScoreBoard[i_Row, i_Column].Name = string.Format("buttonSocreBoard{0},{1}", i_Row, i_Column);
            r_ButtonsOfScoreBoard[i_Row, i_Column].Size = new Size(k_ButtonSizeOfScoreBoard, k_ButtonSizeOfScoreBoard);
            r_ButtonsOfScoreBoard[i_Row, i_Column].Location = new Point(i_LocationX, i_LocationY);
            r_ButtonsOfScoreBoard[i_Row, i_Column].FlatStyle = FlatStyle.Flat;
            r_ButtonsOfScoreBoard[i_Row, i_Column].TabStop = false;
            r_ButtonsOfScoreBoard[i_Row, i_Column].Enabled = false;
            this.Controls.Add(r_ButtonsOfScoreBoard[i_Row, i_Column]);
        }

        private void createRowOfComputerSelectionButtons(ref Point io_LocationOfButton)
        {
            for (int i = 0; i < GameLogic.k_LengthOfGuess; i++)
            {
                createComputerGuessButton(i, io_LocationOfButton.X, io_LocationOfButton.Y);
                io_LocationOfButton.X += k_ButtonSizeOfComputerAndUserGuess + 5;
            }

            io_LocationOfButton.X = k_PositionOfStart;
            io_LocationOfButton.Y += k_ButtonSizeOfComputerAndUserGuess + 20;
        }

        private void createRowOfUserGuessButtons(int i_Row, ref Point io_LocationOfButton)
        {
            for (int i = 0; i < GameLogic.k_LengthOfGuess; i++)
            {
                createUserGuessButton(i_Row, i, io_LocationOfButton.X, io_LocationOfButton.Y);
                io_LocationOfButton.X += k_ButtonSizeOfComputerAndUserGuess + 5;
            }
        }

        private char[] createGuessCharArrayFromUserGuessButtonColor(int i_Row)
        {
            char[] userGuessInput = new char[GameLogic.k_LengthOfGuess * 2];

            setSpacesInCharArrayofGuesses(ref userGuessInput);
            setLettersInCharArrayOfGuess(ref userGuessInput, i_Row);

            return userGuessInput;
        }

        private void createRowOfScoreBoardButtons(int i_Row, ref Point io_LocationOfButton)
        {
            int ButtonOneLocationX = io_LocationOfButton.X;
            int column = 0;

            for (; column < GameLogic.k_LengthOfGuess / 2; column++)
            {
                createScoreBoardButtons(i_Row, column, io_LocationOfButton.X, io_LocationOfButton.Y);
                io_LocationOfButton.X += k_ButtonSizeOfScoreBoard + 2;
            }

            io_LocationOfButton.X = ButtonOneLocationX;
            io_LocationOfButton.Y += k_ButtonSizeOfScoreBoard + 5;
            for (; column < GameLogic.k_LengthOfGuess; column++)
            {
                createScoreBoardButtons(i_Row, column, io_LocationOfButton.X, io_LocationOfButton.Y);
                io_LocationOfButton.X += k_ButtonSizeOfScoreBoard + 2;
            }
        }

        private void GameBoardForm_Load(object sender, EventArgs e)
        {
        }
    }
}
