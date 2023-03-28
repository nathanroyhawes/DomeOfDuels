using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomeOfDuels
                    
{
    public class Item

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackMod { get; set; }
        public int Gold { get; set; }

        public Item(int id, string name, int attackMod, int gold)
        { 
            Id = id;
            Name = name;
            AttackMod = attackMod;
            Gold = gold;
        }   
    }


}
