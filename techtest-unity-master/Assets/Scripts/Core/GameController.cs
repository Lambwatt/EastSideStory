using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	[SerializeField] Text _playerHand;
    [SerializeField] Text _enemyHand;

    [SerializeField] PlayerLoadController _playerLoadController;
    [SerializeField] EndScreenController _endScreenController;
    [SerializeField] BetController _betController;

    [SerializeField] OpponentManager _opponentManager;
    [SerializeField] ResultPresentationManager _resultPresentationManager;
    [SerializeField] MoneyManager _moneyManager;

    [SerializeField] Button[] _progressionButtons;

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
        _opponentManager.intialize();
    }

    private void LoadPlayer()
    {
        DisableControl();
        _playerLoadController.gameObject.SetActive(true);
        _playerLoadController.OnLoaded += OnPlayerInfoLoaded;
    }

	public void OnPlayerInfoLoaded(PlayerData player)
	{
        _playerLoadController.OnLoaded -= OnPlayerInfoLoaded;

		_session = SessionData.Instance.intialize(new Player(player));
        _nameLabel.text = "" + _session.Player.GetName();
        _moneyManager.Initialze();
        _opponentManager.ChooseFirstOpponent(ReturnControl);
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

        UpdateGame(playerChoice, _opponentManager.GetHand(), _betController.GetBet());
	}

	private void UpdateGame(UseableItem playerChoice, UseableItem opponentChoice, int bet)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(playerChoice, opponentChoice, bet);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.load();
	}

	public void OnGameUpdated(GameUpdate gameUpdateData)
	{
		_playerHand.text = DisplayResultAsText(gameUpdateData.resultPlayer);
		_enemyHand.text = DisplayResultAsText(gameUpdateData.resultOpponent);

		_session.Player.ChangeCoinAmount(gameUpdateData.coinsAmountChange);
        _session.AddGameUpdate(gameUpdateData);

        DisableControl();
        _resultPresentationManager.HandleResult(gameUpdateData.drawResult, () =>
        {
            _moneyManager.UpdateMoney(gameUpdateData , ()=> {
                _opponentManager.HandleResult(gameUpdateData.drawResult , ReturnControl);
            });
        });

        _betController.UpdateUI();
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
        for(int i = 0; i<_progressionButtons.Length; i++)
        {
            _progressionButtons[i].interactable = status;
        }
    }
    #endregion

    #region -- Endgame Flow --
    public void HandleRestart()
    {
        _endScreenController.OnReplay -= HandleRestart;

        LoadPlayer();
    }

    public void OnRetire()
    {
        _session.SavePlayerData();
        _endScreenController.OnReplay += HandleRestart;
        _endScreenController.gameObject.SetActive(true);
    }
    #endregion
}