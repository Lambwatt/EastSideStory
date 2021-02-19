using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;

    public PlayerLoadController playerLoadController;
    public EndScreenController endScreenController;
    public BetController betController;
    public OpponentManager opponentManager;

    public Button[] ProgressionButtons;

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
        LoadPlayer();
        opponentManager.intialize();
        //Debug.Log("Pause here");
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
        opponentManager.ChooseFirstOpponent();
        playerLoadController.OnLoaded -= OnPlayerInfoLoaded;
        StartCoroutine(CallUpdate());
	}

	public void UpdateHud()
	{
        
		_nameLabel.text = "Name: " + _session.Player.GetName();
		_moneyLabel.text = "Money: $" + _session.Player.GetCoins().ToString();
	}

    private void LoadPlayer()
    {
        playerLoadController.gameObject.SetActive(true);
        playerLoadController.OnLoaded += OnPlayerInfoLoaded;
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

        UpdateGame(playerChoice, opponentManager.GetHand(), betController.GetBet());
	}

	private void UpdateGame(UseableItem playerChoice, UseableItem opponentChoice, int bet)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice, opponentChoice, bet);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.load();
	}

	public void OnGameUpdated(GameUpdate gameUpdateData)
	{
		playerHand.text = DisplayResultAsText(gameUpdateData.resultPlayer);
		enemyHand.text = DisplayResultAsText(gameUpdateData.resultOpponent);

		_session.Player.ChangeCoinAmount(gameUpdateData.coinsAmountChange);
        _session.AddGameUpdate(gameUpdateData);

        DisableControl();
        opponentManager.HandleResult(gameUpdateData.drawResult, ReturnControl);

        betController.UpdateUI();
	}

    public void DisableControl()
    {
        SetProgressionButtons(false);
    }

    public void ReturnControl()
    {
        SetProgressionButtons(true);
    }

    public void SetProgressionButtons(bool status)
    {
        for(int i = 0; i<ProgressionButtons.Length; i++)
        {
            ProgressionButtons[i].interactable = status;
        }
    }

    public void HandleRestart()
    {
        endScreenController.OnReplay -= HandleRestart;

        LoadPlayer();
    }

    public void OnRetire()
    {
        _session.SavePlayerData();
        endScreenController.OnReplay += HandleRestart;
        endScreenController.gameObject.SetActive(true);
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