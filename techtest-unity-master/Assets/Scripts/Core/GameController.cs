using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] Text _playerHand;
    public Text playerHand => _playerHand;

    [SerializeField] Text _enemyHand;
    public Text enemyHand => _enemyHand;

    [SerializeField] PlayerLoadController _playerLoadController;
    public PlayerLoadController playerLoadController => _playerLoadController;

    [SerializeField] EndScreenController _endScreenController;
    public EndScreenController endScreenController => _endScreenController;

    [SerializeField] BetController _betController;
    public BetController betController => _betController;

    [SerializeField] OpponentManager _opponentManager;
    public OpponentManager opponentManager => _opponentManager;

    [SerializeField] ResultPresentationManager _resultPresentationManager;
    public ResultPresentationManager resultPresentationManager => _resultPresentationManager;

    [SerializeField] MoneyManager _moneyManager;
    public MoneyManager moneyManager => _moneyManager;

    [SerializeField] Button[] _progressionButtons;
    public Button[] progressionButtons => _progressionButtons;

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

	private void OnPlayerInfoLoaded(PlayerData player)
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

	private void OnGameUpdated(GameUpdate gameUpdateData)
	{
		_playerHand.text = DisplayResultAsText(gameUpdateData.resultPlayer);
		_enemyHand.text = DisplayResultAsText(gameUpdateData.resultOpponent);

		_session.Player.ChangeCoinAmount(gameUpdateData.coinsAmountChange);
        _session.AddGameUpdate(gameUpdateData);

        DisableControl();
        _resultPresentationManager.HandleResult(gameUpdateData.drawResult, (Action)(() =>
        {
            _moneyManager.UpdateMoney(gameUpdateData, (Action)(()=> {
                this._opponentManager.HandleResult(gameUpdateData.drawResult , (Action)this.ReturnControl);
            }));
        }));

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
        for(int i = 0; i<_progressionButtons.Length; i++)
        {
            _progressionButtons[i].interactable = status;
        }
    }
    #endregion

    #region -- Endgame Flow --
    public void OnRetire()
    {
        _session.SavePlayerData();
        _endScreenController.OnReplay += HandleRestart;
        _endScreenController.gameObject.SetActive(true);
    }

    private void HandleRestart()
    {
        _endScreenController.OnReplay -= HandleRestart;
        LoadPlayer();
    }
    #endregion
}