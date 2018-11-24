using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsObject
{
    public bool GameCompleted;
    public string[] KeysPressed;
    public string Id;
    public string Level;
    public Vector2 LocationOnQuit;
    public float TimePlayedSeconds;
    public int DeathCount;
    public string[] LevelsCleared;
    public int Score;
    public string Platform;
}