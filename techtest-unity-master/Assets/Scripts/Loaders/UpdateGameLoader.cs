using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(Hashtable gameUpdateData);
	public event OnLoadedAction OnLoaded;

	private UseableItem _choice;
    private int _bet;

	public UpdateGameLoader(UseableItem playerChoice, int playerBet)
	{
		_choice = playerChoice;
        _bet = playerBet;
    }

	public void load()
	{
        UseableItem opponentHand = UseableItem.Rock;//(UseableItem)UnityEngine.Random.Range(0, Enum.GetValues(typeof(UseableItem)).Length);


        Hashtable mockGameUpdate = new Hashtable();
		mockGameUpdate["resultPlayer"] = _choice;
		mockGameUpdate["resultOpponent"] = opponentHand;
		mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(_choice, opponentHand);
		
		OnLoaded(mockGameUpdate);
	}

	private int GetCoinsAmount (UseableItem playerHand, UseableItem opponentHand)
	{
		Result drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

		if (drawResult.Equals (Result.Won))
		{
			return _bet;
		}
		else if (drawResult.Equals (Result.Lost))
		{
			return -_bet;
		}
		else
		{
			return 0;
		}

		return 0;
	}
}