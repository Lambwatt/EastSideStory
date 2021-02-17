using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(GameUpdate gameUpdateData);
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


  //      Hashtable mockGameUpdate = new Hashtable();
		//mockGameUpdate["resultPlayer"] = _choice;
		//mockGameUpdate["resultOpponent"] = opponentHand;
		//mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(_choice, opponentHand);

        GameUpdate gameUpdate = HandleDraw(_choice, opponentHand);
		OnLoaded(gameUpdate);
	}

	private GameUpdate HandleDraw (UseableItem playerHand, UseableItem opponentHand)
	{
        GameUpdate gameUpdate = new GameUpdate();

        gameUpdate.resultPlayer = _choice;
        gameUpdate.resultOpponent = opponentHand;

        gameUpdate.drawResult = ResultAnalyzer.GetResultState(playerHand, opponentHand);

		if (gameUpdate.drawResult.Equals (Result.Won))
		{
            gameUpdate.coinsAmountChange = SessionData.Instance.GetMoney() + _bet < Constants.MAX_MONEY ? _bet : Constants.MAX_MONEY - SessionData.Instance.GetMoney();
		}
		else if (gameUpdate.drawResult.Equals (Result.Lost))
		{
            gameUpdate.coinsAmountChange = SessionData.Instance.GetMoney() - _bet > -Constants.MAX_MONEY ? -_bet : - Constants.MAX_MONEY - SessionData.Instance.GetMoney();
		}
		else
		{
            gameUpdate.coinsAmountChange = 0;
		}

		return gameUpdate;
	}
}