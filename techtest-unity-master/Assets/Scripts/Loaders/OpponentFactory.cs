using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFactory
{
    Dictionary<string, Personality> _personas;
    HashSet<string> _pastOpponents;

    public OpponentFactory(List<Personality> personas)
    {
        _personas = new Dictionary<string, Personality>();
        foreach(Personality p in personas)
        {
            _personas.Add(p.GetId(), p);
        }

        _pastOpponents = new HashSet<string>();
    }

    public Opponent ChooseOpponent()
    {
        int roundsPlayed = SessionData.Instance.Updates.Count;
        List<string> matches = new List<string>(_personas.Count - _pastOpponents.Count);
        foreach(Personality p in _personas.Values)
        {
            if (p.GetMinRounds() <= roundsPlayed && !_pastOpponents.Contains(p.GetId()))
            {
                matches.Add(p.GetId());
            }
        }

        string selectionKey = matches[Random.Range(0, matches.Count)];
        _pastOpponents.Add(selectionKey);

        if (_pastOpponents.Count == _personas.Count)
        {
            _pastOpponents.Clear();
        }

        return new Opponent(ConstructStrategy(selectionKey), _personas[selectionKey]);
    }

    //This is where the magic happens
    public IStrategy ConstructStrategy(string key)
    {
        switch (key)
        {
            case "NATE_SILVER":
                return new SingleFunctionWithOffset(AIFunctions.GetWeightedGuess, Constants.WINNING_OFFSET);
            case "EEYORE":
                return new SingleFunctionWithOffset(AIFunctions.GetWeightedGuess, Constants.LOSING_OFFSET);
            case "BETH_HARMON":
                return new SingleFunctionWithOffset(AIFunctions.GetLastPlayerMove, Constants.WINNING_OFFSET);
            case "BARON_HARKONNEN":
                return new SingleFunctionWithOffset(AIFunctions.GetLastPlayerMove, Constants.LOSING_OFFSET);
            case "CATWOMAN":
                return new SingleFunctionWithOffset(AIFunctions.GetLastPlayerMove, 0);
            case "PETER_GRIFFIN":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Rock, UseableItem.Paper, UseableItem.Scissors });
            case "REVEREND_LOVEJOY":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Paper, UseableItem.Scissors });
            case "GEORGE_R_R_MARTIN":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Rock, UseableItem.Scissors });
            case "IVAN_DOROSCHUK":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Rock, UseableItem.Paper });
            case "EDWARD_SCISSORHANDS":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Scissors });
            case "J_JONAH_JAMESON":
                return new RandomFromSubset(new UseableItem[] { UseableItem.Paper });
            default:
            case "KID_ROCK":
                return new RandomFromSubset(new UseableItem[]{UseableItem.Rock});
        }
    }
}
