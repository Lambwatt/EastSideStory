using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Personality", order = 1)]
public class Persona : ScriptableObject
{
    //The underscores here are inconsistent, but the data will be lost if they are renamed.
    [SerializeField] string _id;
    public string id => _id;

    [SerializeField] int _minRounds;
    public int minRounds => _minRounds;

    [SerializeField] Sprite _portrait;
    public Sprite portrait => _portrait;

    [SerializeField] string _name;
    public string name => _name;

    [SerializeField] string _intro;
    public string intro => _intro;

    [SerializeField] string _outro;
    public string outro => _outro;

    [SerializeField] string[] _winTaunts;
    [SerializeField] string[] _loseTaunts;
    [SerializeField] string[] _tieTaunts;

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
