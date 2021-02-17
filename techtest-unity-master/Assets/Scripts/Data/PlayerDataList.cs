﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> players;

    public PlayerDataList()
    {
        players = new List<PlayerData>();
    }

    //public PlayerData AddPlayer(string name)
    //{
    //    PlayerData player = new PlayerData(players.Count, name);
    //    players.Add(player);
    //    return player;
    //}

    public void UpdatePlayer(PlayerData player)
    {
        if (player.userId > players.Count)
        {
            player.userId = players.Count;
            players.Add(player);
        }
    }
}
