using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleverWeather2
{
    public class Day
    {
        public Struktura[] Today;
        public Struktura[] Tomorrow;

        public Day()
        {
            Today = new Struktura[3];
            Tomorrow = new Struktura[3];

        }
    }
}