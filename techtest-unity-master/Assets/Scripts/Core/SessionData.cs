using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData
{
    private static SessionData _instance = null;

    public Player Player { get; private set; }

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
        return _instance;
    }

    public int GetMoney()
    {
        return Player.GetCoins();
    }
}
