using System;
using System.Windows.Forms;

namespace FifteenGUI
{
    public partial class FifteenPuzzle : Form
    {
        Game game;
        public FifteenPuzzle()
        {
            InitializeComponent();
            game = new Game(4);
        }

        private Button GetButton(int index)
        {
            return (Button)Controls.Find("button" + index, true)[0];
        }

        private void RefreshButtonField()
        {
            for (int i = 0; i < 16; i++)
            {
                Button tempButton = GetButton(i);
                tempButton.Text = game.GetNumber(i).ToString();
                if (tempButton.Text == "0") tempButton.Visible = false;
                else tempButton.Visible = true;
            }
        }

        private void startMenu_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void FifteenPuzzle_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            timer.Stop();
            game.Start();
            for (int i = 0; i < 100; i++) game.ShiftRandom();
            RefreshButtonField();
            labelTime.Text = "0";
            labelScore.Text = "0";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            timer.Start();
            int position = Convert.ToInt32(((Button)sender).Tag);

            if (game.Shift(position)) labelScore.Text = $"{Convert.ToInt32(labelScore.Text) + 1}";

            RefreshButtonField();
            if (game.CheckWin())
            {
                MessageBox.Show("You win the game!", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                timer.Stop();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = $"{Convert.ToInt32(labelTime.Text) + 1}";
        }
    }
}
