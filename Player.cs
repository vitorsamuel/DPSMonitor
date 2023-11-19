using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPSMonitor
{
    internal class Player
    {
        public Player() { }


        String name;

        float damage;

        float dps;

        float physical;

        float divine;

        float magical;

        public string Name { get => name; set => name = value; }
        public float Damage { get => damage; set => damage = value; }
        public float Dps { get => dps; set => dps = value; }
        public float Physical { get => physical; set => physical = value; }
        public float Divine { get => divine; set => divine = value; }
        public float Magical { get => magical; set => magical = value; }
    }
}
