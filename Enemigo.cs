using System;
using System.ComponentModel.Design;
using PracticaParaElRepo;

public class Enemigo : Entidad // Herencia de la clase entidad.
{
 
    public string Nombre;
    public int Vida;
    public bool EstaActivo; // Enemigo patrullando
    public int direccionX;
    public int direccionY;
    public static List<Enemigo> Enemigos = new List<Enemigo>();

    public Enemigo(int posicionX, int posicionY, char simbolo, ConsoleColor color, string nombre, int vida)
        : base(posicionX, posicionY, simbolo, color)
    {
        Enemigos.Add(this);
        Nombre = nombre;
        Vida = vida; // Sin esto link los mata de un golpe, o mejor dicho tienen vida 0.
        EstaActivo = true; // Està patrullando por default

        if (nombre == "Goblin")
        {
            direccionY = 0;
            direccionX = 1; // Le indicamos que patrulle en el eje horizontal.
        }

        else if (nombre == "Minotauro")
        {
            direccionY = 1; // Le indicamos que patrulle en el eje vertical.
            direccionX = 0;
        }

    }

    // Acá deberían ser las funciones polimorficas de la entidad enemigo.

    // PATRULLAJE (el mover del enemigo).

    public void Patrullar()
    {
        if (!EstaActivo)
            return;

        int nuevaFila = PosicionY + direccionY;
        int nuevaColumna = PosicionX + direccionX;

        if (!Mapa.instance.MoverEntidad(this, nuevaFila, nuevaColumna))
        {
            direccionY *= -1;
            direccionX *= -1;
        }
    }


    // RECIBIR DAÑO
    public override void RecibirDanio( int cantidadDanio)
    {
        Vida -= cantidadDanio;

        Console.WriteLine($"Enemigo {Nombre} en ({PosicionX},{PosicionY}) recibió {cantidadDanio} de daño. Vida restante: {Vida}"); // FLAG de testeo

        if (Vida <= 0) // Si està muerto lo limpiamos del mapa y lo eliimanos de la lista de enemigos
        {
            Mapa.instance.casillas[PosicionY, PosicionX].Ocupante = null;
            // Enemigo.Enemigos.Remove(this);
            EstaActivo = false;
            
        }
        else
        {
            EstaActivo = false;  // Si sigue vivo deja de patrullar.
            
        }
    }
}
