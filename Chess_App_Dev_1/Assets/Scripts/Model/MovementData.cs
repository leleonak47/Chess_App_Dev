using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : MonoBehaviour
{
    public string ChessmanName;
    public int LastX, LastY;
    public int NewX, NewY;

    public MovementData(string chessmanName, int lastX, int lastY, int newX, int newY)
    {
        ChessmanName = chessmanName;
        LastX = lastX;
        LastY = lastY;
        NewX = newX;
        NewY = newY;

        print($"Novo movimento registrado : {chessmanName} - LX:{lastX} LY:{lastY} NX:{newX} NY:{newY}");
    }
}
