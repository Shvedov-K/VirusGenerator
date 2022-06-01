using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viruses
{
    class Symptom
    {
        public Boolean isActive { get; set; }
        public string name { get; set; }
        public int stealth { get; set; }
        public int resistance { get; set; }
        public int speed { get; set; }
        public int transmittable { get; set; }
        public int level { get; set; }
        public string chemical { get; set; }
        public string effect { get; set; }

        public int selectedIndex { get; set; }

        public Symptom(string name, int stealth, int resistance, int speed, int transmittable, int level, string chemical, string effect)
        {
            isActive = false;
            this.name = name;
            this.stealth = stealth;
            this.resistance = resistance;
            this.speed = speed;
            this.transmittable = transmittable;
            this.level = level;
            this.chemical = chemical;
            this.effect = effect;
        }

        public Symptom(string[] arr)
        {
            isActive = false;
            this.name = arr[0];            
            this.stealth = int.Parse(arr[1]);
            this.resistance = Int32.Parse(arr[2]);
            this.speed = Int32.Parse(arr[3]);
            this.transmittable = Int32.Parse(arr[4]);
            this.level = Int32.Parse(arr[5]);
            this.chemical = arr[6];
            this.effect = arr[7];
        }

        public void changeState()
        {
            if (isActive)
            {
                this.isActive = false;
            }
            else
            {
                this.isActive = true;
            }
        }

        public string getParam(int param)
        {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            switch (param)
            {
                case 0: return this.name;
                case 1: return this.stealth.ToString();
                case 2: return this.resistance.ToString();
                case 3: return this.speed.ToString();
                case 4: return this.transmittable.ToString();
                case 6: return this.level.ToString();
                case 5: return this.chemical.ToString();
                case 7: return this.effect.ToString();
                default: return null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            }
        }
    }
}
