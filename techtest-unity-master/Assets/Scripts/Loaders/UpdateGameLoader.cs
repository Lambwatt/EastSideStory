using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(GameUpdate gameUpdateData);
	public event OnLoadedAction OnLoaded;

	private UseableItem _playerChoice;
    private UseableItem _opponentChoice;
    private int _bet;

	public UpdateGameLoader(UseableItem playerChoice, UseableItem opponentChoice, int playerBet)
	{
		_playerChoice = playerChoice;
        _opponentChoice = opponentChoice;
        _bet = playerBet;
    }

	public void load()
	{
        //UseableItem opponentHand = UseableItem.Rock;//(UseableItem)UnityEngine.Random.Range(0, Enum.GetValues(typeof(UseableItem)).Length);

        GameUpdate gameUpdate = HandleDraw(_playerChoice, _opponentChoice);
		OnLoaded(gameUpdate);
	}

	private GameUpdate HandleDraw (UseableItem playerHand, UseableItem _opponentChoice)
	{
        GameUpdate gameUpdate = new GameUpdate();

        gameUpdate.resultPlayer = _playerChoice;
        gameUpdate.resultOpponent = _opponentChoice;

        gameUpdate.drawResult = ResultAnalyzer.GetResultState(playerHand, _opponentChoice);

		if (gameUpdate.drawResult.Equals (Result.Win))
		{
            gameUpdate.coinsAmountChange = SessionData.Instance.GetMoney() + _bet < Constants.MAX_MONEY ? _bet : Constants.MAX_MONEY - SessionData.Instance.GetMoney();
		}
		else if (gameUpdate.drawResult.Equals (Result.Lose))
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