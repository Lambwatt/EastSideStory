using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    const int MAX_MONEY = 999999999;

    int _bet = 10;

    public InputField BetField;

    public Button UpButton;
    public Button DownButton;
    public Button MaxButton;
    public Button MinButton;

    // Start is called before the first frame update
    void Start()
    {
        BetField.SetTextWithoutNotify("10");
    }

    //based on money and bet, enable or disable up or down buttons. 
    void UpdateUI()
    {
        //clampBet();

        int money = SessionData.Instance.GetMoney();

        bool canIncrease;
        if (money > 500)
        {
            _bet = Mathf.Max(Mathf.Min(_bet, money), 1);
            canIncrease = _bet < money;
        }
        else
        {
            _bet = Mathf.Max(Mathf.Min(_bet, 500), 1);
            canIncrease = _bet < 500;
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
}
