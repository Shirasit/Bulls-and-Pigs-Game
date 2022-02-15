using System;
using System.Drawing;
using System.Windows.Forms;

namespace PigsAndBullsUI
{
    public partial class ColorSelsectionForm : Form
    {
        private Color m_LastSelectedColor;

        public Color LastSelectedColor
        {
            get { return m_LastSelectedColor; }
        }

        public ColorSelsectionForm()
        {
            InitializeComponent();
            foreach (Button colorButton in this.Controls)
            {
                colorButton.Click += colorButton_Click;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            Button colorButton = sender as Button;

            if (colorButton != null)
            {
                m_LastSelectedColor = colorButton.BackColor;
            }

            Close();
        }

        private void ColorSelectionForm_Load(object sender, EventArgs e)
        {
        }
    }
}