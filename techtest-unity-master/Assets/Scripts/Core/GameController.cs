using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;

    public IntroController playerLoadController;
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
		//PlayerInfoLoader playerInfoLoader = new PlayerInfoLoader();
       

        //Open intro window
        playerLoadController.gameObject.SetActive(true);
        playerLoadController.OnLoaded += OnPlayerInfoLoaded;

        Debug.Log("Pause here");
        //playerInfoLoader.load();
	}

	IEnumerator CallUpdate()
	{
        while (true)
        {
            //This almost never needs to happen. Maybe move it out of the update/coroutine
            yield return null;
            UpdateHud();
        }
	}

	public void OnPlayerInfoLoaded(PlayerData player)
	{
		_session = SessionData.Instance.intialize(new Player(player));
        playerLoadController.OnLoaded -= OnPlayerInfoLoaded;
        StartCoroutine(CallUpdate());
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

    public void OnRetire()
    {
        _session.SavePlayerData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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