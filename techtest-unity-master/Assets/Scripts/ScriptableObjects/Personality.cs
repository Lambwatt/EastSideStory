using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Personality", order = 1)]
public class Personality : ScriptableObject
{
    [SerializeField] string _id;
    [SerializeField] Sprite _portrait;
    [SerializeField] string _name;
    [SerializeField] string _intro;
    [SerializeField] string _outro;
    [SerializeField] string[] _winTaunts;
    [SerializeField] string[] _loseTaunts;
    [SerializeField] string[] _tieTaunts;
}
