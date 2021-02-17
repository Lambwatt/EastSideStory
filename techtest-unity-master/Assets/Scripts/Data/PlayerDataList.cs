using System.Collections;
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

    public void UpdatePlayer(PlayerData player)
    {
        if (player.userId > players.Count || player.userId<0)
        {
            player.userId = players.Count;
            players.Add(player);
        }
        else
        {
            players[player.userId] = player;
        }
    }
}
