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

        float cold;

        float fire;

        float acid;

        float positive;

        float negative;

        float eletrical;

        float sonic;

        int sings;

        public string Name { get => name; set => name = value; }
        public float Damage { get => damage; set => damage = value; }
        public float Dps { get => dps; set => dps = value; }
        public float Physical { get => physical; set => physical = value; }
        public float Divine { get => divine; set => divine = value; }
        public float Magical { get => magical; set => magical = value; }
        public int Sings { get => sings; set => sings = value; }
        public float Cold { get => cold; set => cold = value; }
        public float Fire { get => fire; set => fire = value; }
        public float Acid { get => acid; set => acid = value; }
        public float Positive { get => positive; set => positive = value; }
        public float Negative { get => negative; set => negative = value; }
        public float Eletrical { get => eletrical; set => eletrical = value; }
        public float Sonic { get => sonic; set => sonic = value; }
    }
}
