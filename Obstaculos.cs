using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParaElRepo
{
    public class Obstaculo : Casilla
    {
        public Obstaculo(char simbolo = '#', ConsoleColor color = ConsoleColor.DarkGray)
            : base(simbolo, color)
        {
            Caminable = false;
        }
    }
}