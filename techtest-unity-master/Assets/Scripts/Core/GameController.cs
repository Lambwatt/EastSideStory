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
    public ResultPresentationManager resultPresentationManager;
    public MoneyManager moneyManager;

    public Button[] progressionButtons;

	private Text _nameLabel;
	private SessionData _session;

    #region -- Startup -- 
    void Awake()
	{
        _nameLabel = transform.Find ("Canvas/Name").GetComponent<Text>();
	}

	void Start()
	{
        LoadPlayer();
        opponentManager.intialize();
    }

    private void LoadPlayer()
    {
        DisableControl();
        playerLoadController.gameObject.SetActive(true);
        playerLoadController.OnLoaded += OnPlayerInfoLoaded;
    }

	private void OnPlayerInfoLoaded(PlayerData player)
	{
        playerLoadController.OnLoaded -= OnPlayerInfoLoaded;
		_session = SessionData.Instance.intialize(new Player(player));
        _nameLabel.text = "" + _session.Player.GetName();
        moneyManager.Initialze();
        opponentManager.ChooseFirstOpponent(ReturnControl);
	}
    #endregion

    #region -- Gameplay --
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

	private void OnGameUpdated(GameUpdate gameUpdateData)
	{
		playerHand.text = DisplayResultAsText(gameUpdateData.resultPlayer);
		enemyHand.text = DisplayResultAsText(gameUpdateData.resultOpponent);

		_session.Player.ChangeCoinAmount(gameUpdateData.coinsAmountChange);
        _session.AddGameUpdate(gameUpdateData);

        DisableControl();
        resultPresentationManager.HandleResult(gameUpdateData.drawResult, () =>
        {
            moneyManager.UpdateMoney(gameUpdateData , ()=> {
                opponentManager.HandleResult(gameUpdateData.drawResult , ReturnControl);
            });
        });

        betController.UpdateUI();
	}

    private string DisplayResultAsText(UseableItem result)
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
    #endregion

    #region -- Button Management --
    private void DisableControl()
    {
        SetProgressionButtons(false);
    }

    private void ReturnControl()
    {
        SetProgressionButtons(true);
    }

    private void SetProgressionButtons(bool status)
    {
        for(int i = 0; i<progressionButtons.Length; i++)
        {
            progressionButtons[i].interactable = status;
        }
    }
    #endregion

    #region -- Endgame Flow --
    public void OnRetire()
    {
        _session.SavePlayerData();
        endScreenController.OnReplay += HandleRestart;
        endScreenController.gameObject.SetActive(true);
    }

    private void HandleRestart()
    {
        endScreenController.OnReplay -= HandleRestart;
        LoadPlayer();
    }
    #endregion
}