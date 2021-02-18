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

    public bool isDoneAfterResult(Result r)
    {
        wins -= r == Result.Won ? 1 : 0;
        return wins <= 0;
    }
}
