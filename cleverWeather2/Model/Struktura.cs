using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverWeather2
{
    public class Struktura
    {
        public Boolean[] Top;
        public Boolean[] Bottom;
        public Boolean[] Shoes;
        public Boolean[] Accessories;
        public int Temperature;
        public Boolean[] WeatherConditions;
        public Boolean Wind;
        public int ChanceOfRain;

        public Struktura()
        {
            Top = new Boolean[2]; // kratka, duga majica
            Bottom = new Boolean[2]; // duge, kratke hlace
            Shoes = new Boolean[2]; // cizme, tenisice
            Accessories = new Boolean[5]; // sunglasses, light jacket, winter jacket, winter hat, gloves
            WeatherConditions = new Boolean[3]; // rain, snow, sun

        }
    }
}
