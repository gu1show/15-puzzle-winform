﻿using System;
using System.Windows.Forms;

namespace FifteenGUI
{
    public partial class FifteenPuzzle : Form
    {
        Game game;
        GameHistory gameHistory;
        public FifteenPuzzle()
        {
            InitializeComponent();
            game = new Game(4);
            gameHistory = new GameHistory();
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

        private void StartMenu_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void FifteenPuzzle_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            cancelMyTurnToolStripMenuItem.Visible = false;
            gameTimer.Stop();
            game.Start();
            gameTimer.ResetLabel();
            for (int i = 0; i < 100; i++) game.ShiftRandom();
            RefreshButtonField();
            labelScore.Text = "0";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            gameTimer.Start();
            int position = Convert.ToInt32(((Button)sender).Tag);

            int x, y;
            game.TurnPositionToCoordinates(position, out x, out y);
            if (game.CanShift(x, y))
            {
                gameHistory.History.Push(game.SaveState());
                game.Shift(x, y);
                labelScore.Text = $"{Convert.ToInt32(labelScore.Text) + 1}";
                RefreshButtonField();
                cancelMyTurnToolStripMenuItem.Visible = true;
            }

            if (game.CheckWin())
            {
                gameTimer.Stop();
                MessageBox.Show($"You win the game!\nTime: {gameTimer.ReturnTime()}", 
                                "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cancelMyTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelTurn();
        }

        private void CancelTurn()
        {
            if (gameHistory.History.Count > 0)
            {
                game.RestoreState(gameHistory.History.Pop());
                if (gameHistory.History.Count == 0) cancelMyTurnToolStripMenuItem.Visible = false;
                RefreshButtonField();
            }
        }

        private void FifteenPuzzle_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Z) && (e.Modifiers == Keys.Control)) CancelTurn();
        }
    }
}
