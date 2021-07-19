using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpCustomStrat : IStrategy
{
    private int _stage = 0;
    private UseableItem _favourite;
    private bool _specialTauntReady = false;
    private string _specialTaunt;

    public UseableItem Draw()
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
        if (_stage == 0)
        {
            if(r == Result.Lose)
            {
                _favourite = (UseableItem)((AIFunctions.GetLastPlayerMove() + Constants.WINNING_OFFSET)%3);
                _specialTauntReady = true;
                _specialTaunt = "I played " + _favourite + ". I won. It was the best win! No one thought I could do it. None of them played " + _favourite + ". Only Trump!";
                _stage = 1;
            }
        }
        else
        {
            _stage += r == Result.Win ? 1 : 0;
        }
        return _stage > 9;
    }
}
