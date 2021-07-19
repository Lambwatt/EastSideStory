using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bundles th active persona with the active strategy
public class Opponent
{
    public Sprite Portrait => _persona.portrait;
    public string Name => _persona.name;
    public string Intro => _persona.intro;
    public string Outro => _persona.outro;

    private IStrategy _strategy;
    private Persona _persona;

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

    public string GetTaunt(Result result)
    {
        return _persona.GetTaunt(result);
    }
}
