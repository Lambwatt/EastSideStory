using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFunctionWithOffset : IStrategy
{
    private Func<int> _baseFunction;
    private int _offset;
    private int _wins = 4;

    public SingleFunctionWithOffset(Func<int> baseFunction, int offset)
    {
        _baseFunction = baseFunction;
        _offset = offset;
    }

    public UseableItem Draw()
    {
        return (UseableItem)((_baseFunction()+_offset)%3);
    }

    public string GetSpecialTaunt()
    {
        return "";
    }

    public bool HasSpecialTaunt()
    {
        return false;
    }

    public bool IsDoneAfterResult(Result r)
    {
        _wins -= r == Result.Win ? 1 : 0;
        return _wins <= 0;
    }
}
