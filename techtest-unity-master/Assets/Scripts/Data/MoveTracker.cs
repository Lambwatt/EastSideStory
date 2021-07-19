using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTracker
{
    [SerializeField] int _rocks = 0;
    public int rocks => _rocks;

    [SerializeField] int _papers = 0;
    public int papers => _papers;

    [SerializeField] int _scissors = 0;
    public int scissors => _scissors;

    [SerializeField] UseableItem _lastMove;
    public UseableItem lastMove => _lastMove;

    public void AddMove(UseableItem move)
    {
        _lastMove = move;
        switch (move)
        {
            case UseableItem.Rock:
                _rocks++;
                break;
            case UseableItem.Paper:
                _papers++;
                break;
            case UseableItem.Scissors:
                _scissors++;
                break;
        }
    }
}
