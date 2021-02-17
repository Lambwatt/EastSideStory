using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCounter
{
    public int Rocks { get; private set; } = 0;
    public int Papers { get; private set; } = 0;
    public int Scissors { get; private set; } = 0;

    public void AddMove(UseableItem move)
    {
        switch (move)
        {
            case UseableItem.Rock:
                Rocks++;
                break;
            case UseableItem.Paper:
                Papers++;
                break;
            case UseableItem.Scissors:
                Scissors++;
                break;
        }
    }
}
