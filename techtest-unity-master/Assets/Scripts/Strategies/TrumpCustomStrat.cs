﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpCustomStrat : IStrategy
{
    int _stage = 0;
    UseableItem _favourite;

    public UseableItem draw()
    {
        switch (_stage)
        {
            case 0:
                return (UseableItem)(AIFunctions.FullyRandom());
            case 1:  
            case 2:
                return _favourite;
            case 3:
                return (UseableItem)((AIFunctions.GetLastPlayerMove()+ Constants.WINNING_OFFSET)%3);
            case 4:
            case 5:
                return _favourite;
            case 6:
                return (UseableItem)((AIFunctions.GetWeightedGuess() + Constants.WINNING_OFFSET)%3);
            case 7:               
            case 8:
            case 9:
                return _favourite;
            default:
                return UseableItem.Rock;
        }
    }

    public bool isDoneAfterResult(Result r)
    {
        if (_stage == 0)
        {
            if(r == Result.Lost)
            {
                _favourite = (UseableItem)((AIFunctions.GetLastPlayerMove() + Constants.WINNING_OFFSET)%3);
                _stage = 1;
            }
        }
        else
        {
            _stage += r == Result.Won ? 1 : 0;
        }
        return _stage > 9;
    }
}