using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunctions 
{
    public static int GetLastPlayerMove()
    {
        return (int)SessionData.Instance.Moves.LastMove; 
    }

    public static int GetWeightedGuess()
    {
        int guess = Random.Range(0, SessionData.Instance.Updates.Count);
        MoveTracker moves = SessionData.Instance.Moves;
        if (guess < moves.Rocks)
        {
            return (int)UseableItem.Rock;
        }
        else if(guess < moves.Rocks + moves.Papers)
        {
            return (int)UseableItem.Paper;
        }
        else
        {
            return (int)UseableItem.Scissors;
        }
    }


}
