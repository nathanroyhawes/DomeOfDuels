using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeOfDuels
{
    public class Enemy 
    { 
        public int EnemyId { get; set; }
        public string EnemyName { get; set; }
        public int EnemyAttack { get; set; }
        public int EnemyCurrentHp { get; set; }
        public int EnemyMaxHp { get; set; }
        public int HealAmt { get; set; }

        public Enemy(int enemyId, string enemyName, int enemyAttack, int enemyCurrentHp, int enemyMaxHp, int healAmt)
        {
            EnemyId = enemyId;
            EnemyName = enemyName;
            EnemyAttack = enemyAttack;
            EnemyCurrentHp = enemyCurrentHp;
            EnemyMaxHp = enemyMaxHp;
            HealAmt = healAmt;
     
        }

        

    }
}
