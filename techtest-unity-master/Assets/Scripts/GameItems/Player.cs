using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Player
{
   

    private int _userId;
	private string _name;
	private int _coins;

	public Player(PlayerData playerData)
	{
		_userId = playerData.userId;
		_name = playerData.name; 
		_coins = playerData.coins;
	}
	
	public int GetUserId()
	{
		return _userId;
	}
	
	public string GetName()
	{
		return _name;
	}

	public int GetCoins()
	{
		return _coins;
	}

	public void ChangeCoinAmount(int amount)
	{
		_coins += amount;
        _coins = Mathf.Clamp(_coins, -Constants.MAX_MONEY, Constants.MAX_MONEY);
	}
}