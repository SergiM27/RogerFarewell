using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGhost;
    public static bool fireworksDone;
    public static bool paintingDone;
    public static bool DJDone;
    public static bool zooDone;
    public static bool ringsDone;
    public static bool secondCinematicDone;
    public static int gamesCompleted;

    public void Start()
    {
        fireworksDone = false;
        paintingDone = false;
        DJDone = false;
        zooDone = false;
        ringsDone = false;
        gamesCompleted = 0;
        isGhost = false;
    }
}
