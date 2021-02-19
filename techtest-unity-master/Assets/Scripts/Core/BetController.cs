using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    [SerializeField] InputField BetField;

    [SerializeField] Button UpButton;
    [SerializeField] Button DownButton;
    [SerializeField] Button MaxButton;
    [SerializeField] Button MinButton;

    int _bet = 10;

    // Start is called before the first frame update
    void Start()
    {
        BetField.SetTextWithoutNotify("10");
    }

    //based on money and bet, clamp bet to limits and enable or disable buttons. 
    public void UpdateUI()
    {
        int money = SessionData.Instance.GetMoney();

        bool canIncrease;
        if (money > Constants.MAX_FREE_BET)
        {
            _bet = Mathf.Clamp(_bet, 1, money);
            canIncrease = _bet < money;
        }
        else
        {
            _bet = Mathf.Clamp(_bet, 1, Constants.MAX_FREE_BET);
            canIncrease = _bet < Constants.MAX_FREE_BET;
        }
        MaxButton.interactable = canIncrease;
        UpButton.interactable = canIncrease;

        DownButton.interactable = _bet > 1 ? true : false;
        MinButton.interactable = _bet > 1 ? true : false;

        BetField.SetTextWithoutNotify(""+_bet);
    }

    public void OnChangeEvent(string value)
    {
        _bet = int.Parse(value);
        UpdateUI();
    }

    public void RaiseBet()
    {
        //increment bet by 10
        _bet += 10;
        UpdateUI();
    }

    public void LowerBet()
    {
        //decrement bet by 10
        _bet -= 10;
        UpdateUI();
    }

    public void MaximizeBet()
    {
        //Bet the most you can
        int money = SessionData.Instance.GetMoney();
        if (money > 500)
            _bet = money;
        else
            _bet = 500;
        UpdateUI();
    }

    public void MinimizeBet()
    {
        //Bet 1
        _bet = 1;
        UpdateUI();
    }

    public int GetBet()
    {
        return _bet;
    }
}
