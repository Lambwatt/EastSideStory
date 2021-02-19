using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFromSubset : IStrategy
{
    UseableItem[] _outcomes;
    int _wins = 2;

    public RandomFromSubset(UseableItem[] subset, int wins = 2)
    {
        if(subset!=null && subset.Length > 0)
        {
            _outcomes = subset;
        }
        else
        {
            _outcomes = new UseableItem[1] { UseableItem.Rock };
        }

        _wins = wins;
    }

    public UseableItem draw()
    {
        return _outcomes[Random.Range(0, _outcomes.Length)];
    }

    public string getSpecialTaunt()
    {
        return "";
    }

    public bool hasSpecialTaunt()
    {
        return false;
    }

    public bool isDoneAfterResult(Result r)
    {
        _wins -= r == Result.Win ? 1 : 0;
        return _wins<=0;
    }
}
