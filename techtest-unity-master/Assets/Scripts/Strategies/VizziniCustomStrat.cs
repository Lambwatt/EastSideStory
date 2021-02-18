using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VizziniCustomStrat : IStrategy
{
    int _stage = 0;

    public UseableItem draw()
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

    public bool isDoneAfterResult(Result r)
    {
        _stage += (r == Result.Won ? 1 : 0);
        return _stage > 5;
    }
}
