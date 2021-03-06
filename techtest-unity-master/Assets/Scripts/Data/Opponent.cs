﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bundles th active persona with the active strategy
public class Opponent
{
    IStrategy _strategy;
    Persona _persona;

    public Opponent(IStrategy strategy, Persona persona)
    {
        _strategy = strategy;
        _persona = persona;
    }

    public UseableItem Draw()
    {
        return _strategy.Draw();
    }

    public string getSpecialTaunt()
    {
        return _strategy.GetSpecialTaunt();
    }

    public bool hasSpecialTaunt()
    {
        return _strategy.HasSpecialTaunt();
    }

    public bool isDoneAfterResult(Result result)
    {
        return _strategy.IsDoneAfterResult(result);
    }

    public Sprite GetPortrait()
    {
        return _persona.GetPortrait();
    }

    public string GetName()
    {
        return _persona.GetName();
    }

    public string GetIntro()
    {
        return _persona.GetIntro();
    }

    public string GetOutro()
    {
        return _persona.GetOutro();
    }

    public string GetTaunt(Result result)
    {
        return _persona.GetTaunt(result);
    }
}
