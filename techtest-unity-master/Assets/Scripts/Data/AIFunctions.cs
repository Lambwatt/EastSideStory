using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunctions 
{
    public static int GetLastPlayerMove()
    {
        return (int)SessionData.Instance.Moves.lastMove; 
    }

    public static int GetWeightedGuess()
    {
        int guess = Random.Range(0, SessionData.Instance.Updates.Count);
        MoveTracker moves = SessionData.Instance.Moves;
        if (guess < moves.rocks)
        {
            return (int)UseableItem.Rock;
        }
        else if(guess < moves.rocks + moves.papers)
        {
            return (int)UseableItem.Paper;
        }
        else
        {
            return (int)UseableItem.Scissors;
        }
    }

    public static int FullyRandom()
    {
        return (int)Random.Range(0, 3);
    }
}
