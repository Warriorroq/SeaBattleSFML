using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public struct PlayerData
    {
        public int mma;
        public int wins;
        public int loses;

        public PlayerData(int mma, int wins, int loses)
        {
            this.mma = mma;
            this.wins = wins;
            this.loses = loses;
        }
    }
}
