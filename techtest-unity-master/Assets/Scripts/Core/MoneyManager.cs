using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text _money;
    public Text _change;
    public Animator _animator;

    public void Initialze()
    {
        _money.text = "Money: $" + SessionData.Instance.GetMoney();
    }

    public void UpdateMoney(GameUpdate update, System.Action OnMoneyUpdated)
    {
        _money.text = "Money: $" + SessionData.Instance.GetMoney();
        _change.text = "$" + update.coinsAmountChange;
        if (update.drawResult == Result.Draw) {
            OnMoneyUpdated();
        } else { 
            _animator.SetTrigger(update.drawResult.ToString());
            StartCoroutine(Common.WaitThenCallAction(1.2f, OnMoneyUpdated));
        }
    }
}
