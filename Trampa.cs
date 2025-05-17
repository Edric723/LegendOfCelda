using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParaElRepo
{
    internal class Trampa : Casilla
    {
        public Trampa(char simbolo, ConsoleColor color) : base(simbolo, color)
        {
            simboloBase = simbolo;
            ColorBase = color;
            Ocupante = null;
            Caminable = true;
        }

        public override bool EsTransitable(Entidad entidad)
        {
            return Caminable; // deja pasar aunque haya ocupante (que luego será validado en MoverEntidad)
        }



    }
}
