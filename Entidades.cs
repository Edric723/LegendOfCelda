using PracticaParaElRepo;
using System;


public abstract class Entidad// Clase
{
    public int PosicionX;
    public int PosicionY;
    public char Simbolo;
    public ConsoleColor Color;


    public Entidad(int posicionX, int posicionY, char simbolo, ConsoleColor color) // Constructor
    {
        PosicionX = posicionX;
        PosicionY = posicionY;
        Simbolo = simbolo;
        Color = color;

    }
    public void Mover(int nuevaFila, int nuevaColumna) // Simplemente actualiza la posición
    {
        //Mapa.instance.casillas[PosicionY, PosicionX].Ocupante = null;
        PosicionY = nuevaFila;
        PosicionX = nuevaColumna;
    }


    public static bool EsEntidadEnCoordenada(Entidad entidad, int x, int y)
    {
        return entidad.PosicionX == x && entidad.PosicionY == y;
    }





    public abstract void RecibirDanio( int cantidadDanio);
    

    

    // ATAQUE

    // RECIBIR DAÑO



}

