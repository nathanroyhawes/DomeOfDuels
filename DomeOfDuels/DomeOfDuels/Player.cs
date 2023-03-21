﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeOfDuels
{
    public class Player
    {
        public int PlayerCurrentHP = 20;
        public int PlayerMaxHP = 20;
        public int PlayerAttack { get; set; }
        public int HealAmt = 6;

        public Player(int playerCurrentHP, int playerMaxHP, int playerAttack, int healAmt)
        {
            PlayerCurrentHP = playerCurrentHP;
            PlayerMaxHP = playerMaxHP;
            PlayerAttack = playerAttack;
            HealAmt = healAmt;
        }
    }

}   