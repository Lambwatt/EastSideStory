using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataList
{
    [SerializeField] List<PlayerData> _players;
    public List<PlayerData> players => _players;

    public PlayerDataList()
    {
        _players = new List<PlayerData>();
    }

    public void UpdatePlayer(PlayerData player)
    {
        if (player.userId > _players.Count || player.userId<0)
        {
            player.userId = _players.Count;
            _players.Add(player);
        }
        else
        {
            _players[player.userId] = player;
        }
    }
}
