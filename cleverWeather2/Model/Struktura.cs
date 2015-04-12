using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverWeather2
{
    /// <summary>
    /// Contains Boolean fields that indicate whether the icon should be displayed or not
    /// </summary>
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
            Top = new Boolean[3]; // kratka, duga majica, kratka majica
            Bottom = new Boolean[2]; // duge, kratke hlace
            Shoes = new Boolean[2]; // cizme, tenisice
            Accessories = new Boolean[6]; // sunglasses, light jacket, winter jacket, winter hat, gloves, scarf
            WeatherConditions = new Boolean[3]; // rain, snow, sun

        }
    }
}
