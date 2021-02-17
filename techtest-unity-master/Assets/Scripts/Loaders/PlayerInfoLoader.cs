using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
	public delegate void OnLoadedAction(PlayerData playerData);
	public event OnLoadedAction OnLoaded;

	public void load()
	{
		Hashtable mockPlayerData = new Hashtable();
		mockPlayerData["userId"] = 1;
		mockPlayerData["name"] = "Player 1";
		mockPlayerData["coins"] = 50;

        PlayerDataList playersTestInput = new PlayerDataList();

        playersTestInput.AddPlayer("Player 1"); 
        //playersTestInput.AddPlayer("Player 2");

        //string testString = JsonUtility.ToJson(playersTestInput);
        //PlayerDataList playersTestOutput = JsonUtility.FromJson<PlayerDataList>(testString);

        OnLoaded(playersTestInput.players[0]);
	}
}