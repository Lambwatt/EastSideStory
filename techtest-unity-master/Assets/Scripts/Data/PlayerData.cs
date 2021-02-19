using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public int userId;
    public string name;
    public int coins;

    public PlayerData(string name)
    {
        this.userId = -1;
        this.name = name;
        coins = 50;
    }

    public PlayerData(int userId, string name, int coins)
    {
        this.userId = userId;
        this.name = name;
        this.coins = coins;
    }
}