using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VizziniCustomStrat : IStrategy
{
    int _stage = 0;
    bool _specialTauntReady = false;
    string _specialTaunt;

    public UseableItem Draw()
    {
        switch (_stage)
        {
            case 0:
                return (UseableItem)((AIFunctions.GetLastPlayerMove()+Constants.WINNING_OFFSET)%3);
            case 1:
                return (UseableItem)((AIFunctions.GetLastPlayerMove() + Constants.LOSING_OFFSET)%3);
            case 2:
                return (UseableItem)(AIFunctions.GetLastPlayerMove());
            case 3:
                return (UseableItem)((AIFunctions.GetWeightedGuess() + Constants.WINNING_OFFSET)%3);
            case 4:
                return (UseableItem)((AIFunctions.GetWeightedGuess() + Constants.LOSING_OFFSET)%3);
            case 5:
                return (UseableItem)(AIFunctions.GetWeightedGuess());
            default:
                return UseableItem.Rock;
        }
    }

    public string GetSpecialTaunt()
    {
        _specialTauntReady = false;
        return _specialTaunt;
    }

    public bool HasSpecialTaunt()
    {
        return _specialTauntReady;
    }

    public bool IsDoneAfterResult(Result r)
    {
        _stage += (r == Result.Win ? 1 : 0);
        if(_stage==3 && r == Result.Win)
        {
            _specialTauntReady = true;
            _specialTaunt = "I'll have to derive your move from what I know of you.";
        }
        return _stage > 5;
    }

    
}
