using System;
using System.Collections.Generic;
using System.Drawing;
using PracticaParaElRepo;
using static System.Runtime.InteropServices.JavaScript.JSType;




// Crear el mapa de 30x50
Mapa mapa = new Mapa (30, 50);
Mapa.MostrarPantallaInicio();

// Lista de obstaculos de tipo lago.
var lagos = new List<(int w, int h, int columna, int fila)>
        {
            (4, 4, 9, 1),
            (10, 5, 5, 5),
            (6, 5, 5, 10),
            (3, 1, 6, 15),
            (8, 2, 16, 21),
            (5, 1, 17, 23),
            (7, 2, 16, 24),
            (3, 3, 19, 26)
        };

// Lista de obstaculos de tipo montaña.
var montañas = new List<(int w, int h, int columna, int fila)>
        {
            (2, 4, 22, 6),
            (4, 2, 21, 8),
            (6, 3, 20, 10),
            (3, 2, 33, 16),
            (5, 2, 32, 18),
            (9, 3, 30, 20)
        };

// Lista de obstaculos de tipo árbol.
var arboles = new List<(int w, int h, int columna, int fila)>
        {
            (8, 4, 1, 1),
            (4, 2, 1, 5),
            (3, 5, 17, 6),
            (2, 3, 15, 7),
            (1, 3, 20, 7),
            (3, 8, 29, 1),
            (2, 6, 32, 1),
            (1, 12, 34, 1),
            (2, 15, 35, 1),
            (1, 13, 37, 1),
            (2, 11, 38, 1),
            (4, 4, 45, 15),
            (5, 6, 44, 19),
            (1, 1, 36, 2),
            (1, 1, 8, 3),
            (9, 3, 40, 5),
            (9, 6, 40, 1),
            (4, 3, 29, 26),
            (2, 1, 30, 25),
            (5, 3, 3, 22),
            (10, 4, 1, 25)
        };

// Agregar obstáculos al mapa

mapa.AgregarGrupoObstaculos(lagos, '«', ConsoleColor.Blue);
mapa.AgregarGrupoObstaculos(montañas, '█', ConsoleColor.DarkMagenta);
mapa.AgregarGrupoObstaculos(arboles, '╦', ConsoleColor.Green);




// JUGADOR------------------------------
Jugador jugador = new (15,20 );
mapa.ColocarEntidad(jugador);



// ENEMIGO-------------------------------
var goblin1 = new Enemigo(3, 10, 'G', ConsoleColor.Red, "Goblin", 2);
mapa.ColocarEntidad(goblin1);
// Generar aleatoriamente enemigos y spawn points.
Random random = new(); // Inicializar un generador de números aleatorios.
for (int i = 0; i < 6; i++)
{
    var (fila, columna) = mapa.ObtenerPosicionLibreAleatoria(); // Conseguir una posicion libre en el mapa.

    if (random.Next(2) == 0)
    {
        var goblin = new Enemigo(fila, columna, 'G', ConsoleColor.Red, "Goblin", 2);
        mapa.ColocarEntidad(goblin);

    }
    else
    {
        var minotauro = new Enemigo(fila, columna, 'M', ConsoleColor.DarkRed, "Minotauro", 3);
        mapa.ColocarEntidad(minotauro);

    }
}



// TRAMPAS
Casilla actual = mapa.casillas[10, 2];
Trampa trampa = new('X', ConsoleColor.Red);
trampa.Ocupante = actual.Ocupante; // mantener ocupante si hay
mapa.casillas[10, 2] = trampa;


// Imprimir el mapa por primera vez
mapa.Mostrar();


// BUCLE CENTRAL, es el que realiza los movimientos y las nuevas impresiones sobre el mapa.
while (true)
{
    ConsoleKeyInfo tecla = Console.ReadKey(true);

    int direccionY = 0, direccionX = 0;
    switch (tecla.Key)
    {
        case ConsoleKey.W: direccionY = -1; break;
        case ConsoleKey.S: direccionY = 1; break;
        case ConsoleKey.A: direccionX = -1; break;
        case ConsoleKey.D: direccionX = 1; break;
        case ConsoleKey.Spacebar:
            jugador.Atacar(mapa);
            Thread.Sleep(500); // Usá `using System.Threading;` si no lo tenés
            break;
        case ConsoleKey.Escape: return;
    }

    //  Limpiamos la pantalla (asi no imprimimos infinitas pantallas en cascada)
    //Console.Clear();
    
    //  Movemos a Link (Mover a link ya limpia e imprime la posición de Link en cada iteración)
    jugador.MoverJugador(direccionY, direccionX);
    
    // Patrullaje de enemigos (Patrullar ya limpia e imprime las posiciones de los enemigos en cada iteración).
    foreach (Enemigo enemigo in Enemigo.Enemigos)
        enemigo.Patrullar();

    // Mostramos el mapa final actualizado
    mapa.Mostrar();

    // Si no quedan enemigos en el mapa, pantalla final.

    if (!mapa.HayEnemigosEnMapa())
    {
        Mapa.MostrarPantallaFinal();
    }


}
