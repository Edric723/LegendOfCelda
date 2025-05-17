using System;
using System.Runtime.InteropServices;
using PracticaParaElRepo;

internal class Jugador : Entidad // Herencia de la clase Entidad.
{
    public Jugador(int posicionX, int posicionY) : base(posicionX, posicionY, '§', ConsoleColor.Yellow) 
    {
     // Deberían ir los atributos necesarios. Por el momento lo nombramos como su clase, y está hardcodeado su daño. No presenta vidas.
    }


    // Las funciones que debieran ser polimorficas para todas las entidades.
    public void MoverJugador(int desplazamientoFila, int desplazamientoColumna)
    {
        int nuevaFila = PosicionY + desplazamientoFila;
        int nuevaColumna = PosicionX + desplazamientoColumna;

        Mapa.instance.MoverEntidad(this, nuevaFila, nuevaColumna);
    }


    public void Atacar(Mapa mapa)
    {
        var direcciones = new (int ejeY, int ejeX)[] // array de tuplas, cada tupla es una coordenada/celda.
        {
        (-1, 0),  // Arriba
        (1, 0),   // Abajo
        (0, -1),  // Izquierda
        (0, 1)    // Derecha
        };

        int danioPorGolpe = 1; // Hardcodeo del daño de link, podría meterlo en los atributos de jugador.

        foreach (var (ejeY, ejeX) in direcciones)
        {
            int atkY = PosicionY + ejeY; // En el eje Y , en base a la posición de Link fijarse si hay un enemigo al norte o al sur.
            int atkX = PosicionX + ejeX; // En el eje X , en base a la posición de Link fijarse si hay un enemigo al este u oeste.

            foreach (var enemigo in Enemigo.Enemigos)
            {
                if (EsEntidadEnCoordenada(enemigo, atkX, atkY))
                {
                    // FLAG para verificar antes de aplicar el daño
                    Console.WriteLine($"Intentando golpear a {enemigo.Nombre} en ({enemigo.PosicionX},{enemigo.PosicionY}). Vida del enemigo: {enemigo.Vida}");

                    // Aplica el daño
                    enemigo.RecibirDanio(danioPorGolpe);

                    // FLAG para verificar después de aplicar el daño
                    Console.WriteLine($"El {enemigo.Nombre} ha perdido un punto de vida, ahora tiene: {enemigo.Vida} de vida.");
                    Console.WriteLine();
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();

                    if (!Enemigo.Enemigos.Contains(enemigo))
                        return;  // Solo atacamos una vez

                }
            }
        }
    }

    public override void RecibirDanio( int cantidadDanio)
    {
       
    }
}




