using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFunctionWithOffset : IStrategy
{
    Func<int> _baseFunction;
    int _offset;
    int wins = 4;

    public SingleFunctionWithOffset(Func<int> baseFunction, int offset)
    {
        _baseFunction = baseFunction;
        _offset = offset;
    }

    public UseableItem draw()
    {
        return (UseableItem)((_baseFunction()+_offset)%3);
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
        wins -= r == Result.Win ? 1 : 0;
        return wins <= 0;
    }
}
