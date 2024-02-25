using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : SingletonBehaviour<GameAssets>
{
    public int Width;
    public int Height;
    public GameObject SnakeBody;
    public GameObject MassGainer;
    public GameObject MassBurner;
    public GameObject ShieldPowerUp;
    public GameObject SpeedBoostPowerUp;
    public GameObject ScoreBoostPowerUp;
}
