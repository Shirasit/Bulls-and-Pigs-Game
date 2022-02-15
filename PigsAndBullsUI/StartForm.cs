using System;
using System.Windows.Forms;

namespace PigsAndBullsUI
{
    internal partial class StartForm : Form
    {
        private const int k_MinimumNumberOfGuesses = 4;
        private const int k_MaximumNumberOfGuesses = 10;
        private const string k_TextOnButtonNumberOfChances = "Number of chances: {0}";
        private int m_MaxNumberOfGuesses;

        public StartForm()
        {
            m_MaxNumberOfGuesses = k_MinimumNumberOfGuesses;
            InitializeComponent();
        }

        public int MaxNumberOfGuesses
        {
            get { return m_MaxNumberOfGuesses; }
        }

        private void buttonNumberOfChances_Click(object sender, EventArgs e)
        {
            m_MaxNumberOfGuesses++;
            if (m_MaxNumberOfGuesses > k_MaximumNumberOfGuesses)
            {
                m_MaxNumberOfGuesses = k_MinimumNumberOfGuesses;
            }

            buttonNumOfChances.Text = string.Format(k_TextOnButtonNumberOfChances, m_MaxNumberOfGuesses);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
