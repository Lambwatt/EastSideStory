﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ResultPresentationManager : MonoBehaviour
{
    public Animator _animator;
    public float _animTime;
    public Text _result;

    public void HandleResult(Result r, System.Action OnAnimationComplete = null)
    {
        string resultText = "UNASSIGNED"; //Easy bug to spot.
        switch (r)
        {
            case Result.Win:
                resultText = "YOU WIN!";
                break;
            case Result.Lose:
                resultText = "YOU LOST!";
                break;
            case Result.Draw:
                resultText = "DRAW!";
                break;
        }

        _result.text = resultText;
        _animator.SetTrigger(r.ToString());
        StartCoroutine(Common.WaitThenCallAction(_animTime, OnAnimationComplete));
    }
}
