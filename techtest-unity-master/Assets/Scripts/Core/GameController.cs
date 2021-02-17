using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;

    public BetController betController;

	private Text _nameLabel;
	private Text _moneyLabel;

	private SessionData _session;

	void Awake()
	{
		_nameLabel = transform.Find ("Canvas/Name").GetComponent<Text>();
		_moneyLabel = transform.Find ("Canvas/Money").GetComponent<Text>();
	}

	void Start()
	{
		PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
		playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
		playerInfoLoader.load();
	}

	void Update()
	{
		UpdateHud();
	}

	public void OnPlayerInfoLoaded(PlayerData player)
	{
		_session = SessionData.Instance.intialize(new Player(player));
	}

	public void UpdateHud()
	{
		_nameLabel.text = "Name: " + _session.Player.GetName();
		_moneyLabel.text = "Money: $" + _session.Player.GetCoins().ToString();
	}

	public void HandlePlayerInput(int item)
	{
		UseableItem playerChoice = UseableItem.Rock;

		switch (item)
		{
			case 0:
				playerChoice = UseableItem.Rock;
				break;
			case 1:
				playerChoice = UseableItem.Paper;
				break;
			case 2:
				playerChoice = UseableItem.Scissors;
				break;
		}

        UpdateGame(playerChoice, betController.GetBet());
	}

	private void UpdateGame(UseableItem playerChoice, int bet)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice, bet);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.load();
	}

	public void OnGameUpdated(GameUpdate gameUpdateData)
	{
		playerHand.text = DisplayResultAsText(gameUpdateData.resultPlayer);
		enemyHand.text = DisplayResultAsText(gameUpdateData.resultOpponent);

		_session.Player.ChangeCoinAmount(gameUpdateData.coinsAmountChange);
        betController.UpdateUI();
	}

	private string DisplayResultAsText (UseableItem result)
	{
		switch (result)
		{
			case UseableItem.Rock:
				return "Rock";
			case UseableItem.Paper:
				return "Paper";
			case UseableItem.Scissors:
				return "Scissors";
		}

        //Unreachable
		return "Nothing";
	}
}