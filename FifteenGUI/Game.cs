using System;

namespace FifteenGUI
{
    internal class Game
    {
        int[,] field;
        int size, x0, y0;
        Random rand = new Random();

        public Game(int size)
        {
            this.size = size;
            field = new int[size, size];
        }

        private int TurnCoordinatesToPosition(int x, int y)
        {
            return 4 * y + x;
        }

        private void TurnPositionToCoordinates(int position, out int x, out int y)
        {
            x = Math.Abs(position % 4);
            y = Math.Abs(position / 4);
        }

        public void Start()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    field[i, j] = TurnCoordinatesToPosition(i, j) + 1;

            field[3, 3] = 0;
            x0 = 3;
            y0 = 3;
        }

        public int GetNumber(int position)
        {
            int x, y;
            TurnPositionToCoordinates(position, out x, out y);

            if ((x > -1) && (x < 4) && (y > -1) && (y < 4)) return field[x, y];
            else return 0;
        }

        public bool Shift(int position)
        {
            int x, y;
            TurnPositionToCoordinates(position, out x, out y);
            if (((Math.Abs(x0 - x) < 2) && (y0 - y == 0)) || ((x0 - x == 0) && (Math.Abs(y0 - y) < 2)))
            {
                field[x0, y0] = field[x, y];
                field[x, y] = 0;
                x0 = x;
                y0 = y;
                return true;
            }
            return false;
        }

        public void ShiftRandom()
        {
            int move = rand.Next(4), x = x0, y = y0;

            if (move == 0) x = (x - 1) % 4;
            else if (move == 1) x = (x + 1) % 4;
            else if (move == 2) y = (y - 1) % 4;
            else y = (y + 1) % 4;

            Shift(TurnCoordinatesToPosition(x, y));
        }

        public bool CheckWin()
        {
            if (field[3, 3] != 0) return false;
            else return CheckEachPosition();
        }

        private bool CheckEachPosition()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (
                        (field[i, j] != TurnCoordinatesToPosition(i, j) + 1) &&
                        (((i != 3) || (j != 3)))
                       )
                        return false;
            return true;
        }
    }
}
