using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Personality", order = 1)]
public class Persona : ScriptableObject
{
    [SerializeField] string _id;
    [SerializeField] int _minRounds;
    [SerializeField] Sprite _portrait;
    [SerializeField] string _name;
    [SerializeField] string _intro;
    [SerializeField] string _outro;
    [SerializeField] string[] _winTaunts;
    [SerializeField] string[] _loseTaunts;
    [SerializeField] string[] _tieTaunts;

    public int GetMinRounds()
    {
        return _minRounds;
    }

    public string GetId()
    {
        return _id;
    }

    public Sprite GetPortrait()
    {
        return _portrait;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetIntro()
    {
        return _intro;
    }

    public string GetOutro()
    {
        return _outro;
    }

    public string GetTaunt(Result result)
    {
        string[] tauntList = _winTaunts;

        //Note that results are always relative to the player;
        switch (result)
        {
            case Result.Lose:
                tauntList = _winTaunts;
                break;
            case Result.Win:
                tauntList = _loseTaunts;
                break;
            case Result.Draw:
                tauntList = _tieTaunts;
                break;
        }

        if (tauntList.Length > 0)
        {
            return tauntList[Random.Range(0, tauntList.Length)];
        }
        else
        {
            return "";
        }
    }
}
