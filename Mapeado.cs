using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticaParaElRepo;

namespace PracticaParaElRepo
{

    public class Mapa
    {
        public int alto;
        public int ancho;
        public Casilla[,] casillas;
        public static Mapa? instance;

        public Mapa(int alto, int ancho)
        {
            if (instance == null) instance = this;
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("TENES MAS DE UN MAPA!!!");
            }
            this.alto = alto;
            this.ancho = ancho;
            casillas = new Casilla[alto, ancho];

            Inicializar();
        }

        // Función que inicializa el mapeado interno.
        private void Inicializar()
        {
            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    if (EsBorde(i, j)) // Si es borde, dibujame el límite.
                    {
                        casillas[i, j] = new Casilla('#', ConsoleColor.DarkGray)
                        {
                            Caminable = false
                        };

                    }
                    else // Si no es borde, dibujame el relleno.
                    {
                        casillas[i, j] = new Casilla('.', ConsoleColor.DarkGreen);

                    }
                }
            }
        }


        // FILA = Y FILA = X SE SUPONE QUE TODO AHORA ESTÄ EN FILA,COLUMNA

        // Indica si una posición está en el borde del mapa.
        public bool EsBorde(int fila, int columna)
        {
            return fila == 0 || fila == alto - 1 || columna == 0 || columna == ancho - 1;
        }
        // Indica si una coordenada está dentro de los límites del mapa.

        public bool EsPosicionValida(int fila, int columna)
        {
            return fila >= 0 && fila < alto && columna >= 0 && columna < ancho;
        }
        // Indica si la casilla no tiene ocupantes y es caminable.

        public bool EsPosicionLibre(int fila, int columna) 
        {
            return casillas[fila, columna].Ocupante == null && casillas[fila, columna].Caminable;
        }
        // Genera coordenadas aleatorias que no estén ocupadas y sean caminables.

        public (int, int) ObtenerPosicionLibreAleatoria()
        {
            Random random = new();
            int fila, columna;

            do // Uso do porque preciso si o si una posición para poder verificarla con la condición del while.
            {
                fila = random.Next(1, alto - 1);     // evita los bordes
                columna = random.Next(1, ancho - 1); // evita los bordes
            }
            while (!EsPosicionLibre(fila, columna)); // Verifico que sea libre.

            return (fila, columna);
        }
        // Coloca una entidad en el mapa ( en una posición válida dentro de los límites del mapa)

        public void ColocarEntidad(Entidad entidad)
        {
            if (EsPosicionValida(entidad.PosicionY, entidad.PosicionX) &&
                EsPosicionLibre(entidad.PosicionY, entidad.PosicionX))
            {
                casillas[entidad.PosicionY, entidad.PosicionX].Ocupante = entidad;
            }
        }

        // Indica si hay enemeigos en el mapa.
        public bool HayEnemigosEnMapa()
        {
            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    var ocupante = casillas[i, j].Ocupante;
                    if (ocupante is Enemigo)
                        return true;
                }
            }
            return false;
        }

        public bool MoverEntidad(Entidad entidad, int nuevaFila, int nuevaColumna)
        {
            if (!EsPosicionValida(nuevaFila, nuevaColumna))
                return false;

            var origen = casillas[entidad.PosicionY, entidad.PosicionX];
            var destino = casillas[nuevaFila, nuevaColumna];

            if (!destino.EsTransitable(entidad)) // acá se llama a Casilla o Trampa
                return false;

            origen.Ocupante = null;

            // Actualizamos la posición.
            entidad.Mover(nuevaFila, nuevaColumna);

            // Asignr ocupante al destino.
            destino.Ocupante = entidad;

            // Si es trampa, dañamos al ocupante.
            if (destino is Trampa)
                entidad.RecibirDanio(2);

            return true;
        }

        // Indica si una casilla puede ser atravesada por una entidad.
        //public bool EsPasable(int fila, int columna, Entidad entidad)
        //{
        //    if (!EsPosicionValida(fila,columna)) // Si la fila o la columna NO están dentro del mapa
        //        return false;

        //    return casillas[fila, columna].EsTransitable(entidad);// Solo se puede caminar sobre celdas de piso
        //}





        public void Mostrar()
        {
            Console.SetCursorPosition(0, 0); // Sobre-escribe el mapa sin hacer Clear

            for (int i = 0; i < alto; i++)
            {
                for (int j = 0; j < ancho; j++)
                {
                    var casilla = casillas[i, j];



                    Console.ForegroundColor = casilla.Colorear();
                        Console.Write(casilla.Simbolo());
                    
                

                    Console.Write(' '); // Espacio entre celdas
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }




        // Imprime pantalla Inicial.
        public static void MostrarPantallaInicio()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("███████╗██╗  ██╗███████╗  ██╗     ███████╗ ██████╗ ███████╗███╗   ██╗██████╗ ");
            Console.WriteLine("╚═██║══╝██║  ██║██╔════╝  ██║     ██╔════╝██╔════╝ ██╔════╝████╗  ██║██╔══██╗");
            Console.WriteLine("  ██║   ███████║█████╗    ██║     █████╗  ██║  ███╗█████╗  ██╔██╗ ██║██║  ██║");
            Console.WriteLine("  ██║   ██╔══██║██╔══╝    ██║     ██╔══╝  ██║   ██║██╔══╝  ██║╚██╗██║██║  ██║");
            Console.WriteLine("  ██║   ██║  ██║███████╗  ███████╗███████╗╚██████╔╝███████╗██║ ╚████║██████╔╝");
            Console.WriteLine("  ╚═╝   ╚═╝  ╚═╝╚══════╝  ╚══════╝╚══════╝ ╚═════╝ ╚══════╝╚═╝  ╚═══╝╚═════╝ ");
            Console.WriteLine();
            Console.WriteLine("          ██████╗ ███████╗  ██████╗███████╗██╗     ██████╗  █████╗ ");
            Console.WriteLine("         ██╔═══██╗██╔════╝  ██╔═══╝██╔════╝██║     ██╔══██╗██╔══██╗");
            Console.WriteLine("         ██║   ██║█████╗    ██║    █████╗  ██║     ██║  ██║███████║");
            Console.WriteLine("         ██║   ██║██╔══╝    ██║    ██╔══╝  ██║     ██║  ██║██╔══██║");
            Console.WriteLine("         ╚██████╔╝██║       ██████╗███████╗███████╗██████╔╝██║  ██║");
            Console.WriteLine("          ╚═════╝ ╚═╝       ╚═════╝╚══════╝╚══════╝╚═════╝ ╚═╝  ╚═╝");

            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Controles:");
            Console.WriteLine("  W - Mover arriba");
            Console.WriteLine("  S - Mover abajo");
            Console.WriteLine("  A - Mover izquierda");
            Console.WriteLine("  D - Mover derecha");
            Console.WriteLine("  Espacio - Atacar");

            Console.WriteLine();
            Console.WriteLine("Presiona cualquier tecla para comenzar!");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }
        // Imprime pantalla Final.
        public static void MostrarPantallaFinal()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║          ¡Has derrotado a todos los            ║");
            Console.WriteLine("║               enemigos del mapa!               ║");
            Console.WriteLine("║    Pero la princesa no está en este reino...   ║");
            Console.WriteLine("║       intenta en nuestro proximo juego.        ║");
            Console.WriteLine("║       COMPRALO EN TU TIENDA MAS CERCANA        ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\nPresiona Esc para salir...");
            Console.ReadKey();
        }




        // Funciones Auxiliares que me permiten agregar obstaculos al mapa.
        public void AgregarObstaculo(Obstaculo obstaculo)
        {
            obstaculo.DibujarObstaculo(casillas);
        }

        public static void AgregarGrupoObstaculos(Mapa mapa, List<(int w, int h, int x, int y)> datos, char simbolo, ConsoleColor color)
        {
            foreach (var (w, h, x, y) in datos)
                mapa.AgregarObstaculo(new Obstaculo(w, h, x, y, simbolo, color));
        }
    }




    public class Obstaculo(int width, int height, int xPosition, int yPosition, char symbol, ConsoleColor color)
    {
        public int height = height;
        public int width = width;
        public int xPosition = xPosition;
        public int yPosition = yPosition;
        public char symbol = symbol;
        public ConsoleColor color = color;

        // Función que dibuja el obstáculo en el mapa    FILA = EJE Y / COLUMNA = EJE X
        public void DibujarObstaculo(Casilla[,] casillas)
        {
            // Iteramos sobre las filas y columnas que dibujaremos
            for (int fila = yPosition; fila < yPosition + height; fila++)
            {
                for (int columna = xPosition; columna < xPosition + width; columna++)
                {
                    // Asegurarse de que no se salga del mapa
                    if (fila < casillas.GetLength(0) && columna < casillas.GetLength(1))
                    {
                        casillas[fila, columna].simboloBase = symbol; // Coloca el obstáculo en el mapa.
                        casillas[fila, columna].ColorBase = color; // Colorea el obstáculo.
                        casillas[fila, columna].Caminable = false;
                    }
                }
            }
        }
    }
}


