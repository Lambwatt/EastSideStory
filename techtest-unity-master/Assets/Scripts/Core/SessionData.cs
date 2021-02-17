using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData
{
    private static SessionData _instance = null;

    public Player Player { get; private set; }
    public LinkedList<GameUpdate> Updates {get; private set;}

    public int InitialCoins { get; private set; } = 50;
    public MoveTracker Moves { get; private set; }

    private SessionData()
    {
        //intialize();
    }

    public static SessionData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SessionData();
            }
            return _instance;
        }
    }

    public SessionData intialize(Player p)
    {
        Player = p;
        InitialCoins = Player.GetCoins();
        Updates = new LinkedList<GameUpdate>();
        //Reset anything else
        Moves = new MoveTracker();
        return _instance;
    }

    public void SavePlayerData()
    {
        PlayerSaveManager.UpdateData(Player.GetData()); 
    }

    public int GetMoney()
    {
        return Player.GetCoins();
    }

    public void AddGameUpdate(GameUpdate update)
    {
        Moves.AddMove(update.resultPlayer);
        Updates.AddLast(update);
    }
}
