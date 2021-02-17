using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager 
{
    const string PLAYER_KEY = "players";

    public static bool HasData()
    {
        return PlayerPrefs.HasKey(PLAYER_KEY);
    }

    public static PlayerDataList GetData()
    {
        return JsonUtility.FromJson<PlayerDataList>(PlayerPrefs.GetString(PLAYER_KEY));
    }

    public static void UpdateData(PlayerData playerData)
    {

        PlayerDataList playerDataList = HasData() ? JsonUtility.FromJson<PlayerDataList>(PlayerPrefs.GetString(PLAYER_KEY)) : new PlayerDataList();
        playerDataList.UpdatePlayer(playerData);
        PlayerPrefs.SetString(PLAYER_KEY, JsonUtility.ToJson(playerDataList));
    }
}
