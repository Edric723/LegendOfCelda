using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaParaElRepo
{
    public class Casilla
    {
        public char simboloBase; //
        public Entidad? Ocupante { get; set; }
        public ConsoleColor ColorBase { get; set; }
        public bool Caminable;


        public Casilla(char simbolo, ConsoleColor color)
        {
            simboloBase = simbolo;
            ColorBase = color;
            Ocupante = null;
            Caminable = true;
        }


        public char Simbolo()
        {
            if (Ocupante != null)
            {
                return Ocupante.Simbolo;
            }
            return simboloBase;
        }

        public ConsoleColor Colorear()
        {
            if (Ocupante != null)
            {
                return Ocupante.Color;
            }
            return ColorBase;
        }

        public virtual bool EsTransitable(Entidad entidad) // Separar la lógica de movimiento con el dibujado.
        {
            return Ocupante == null && Caminable;
        }
    }
}

