﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentManager : MonoBehaviour
{
    public Image Portrait;
    public DialogManager Speech;
    public Text Name;

    public List<Persona> Personas;
    public Animator PortraitAnimator;

    Opponent _activeOpponent;
    OpponentFactory _factory; 

    public void intialize()
    {
        _factory = new OpponentFactory(Personas); 
    }

    public void ChooseFirstOpponent(System.Action OnOpponentChosen = null)
    {
        PortraitAnimator.SetTrigger("Reset");
        ChooseOpponent();
        Speech.Clear();
        PlayEnterAnimation(() =>
        {
            Speech.PostDialog(_activeOpponent.GetIntro(), OnOpponentChosen);
        });
    }

    

    public UseableItem GetHand()
    {
        return _activeOpponent.Draw();
    }

    public void HandleResult(Result result, System.Action OnOpponentSettled)
    {
        if (_activeOpponent.isDoneAfterResult(result))
        {
            Speech.PostDialog( _activeOpponent.GetOutro() ,()=>{
                PlayExitAnimation(() => { SwapOpponents(OnOpponentSettled); });
            });
        }
        else
        {
            if (_activeOpponent.hasSpecialTaunt())
                Speech.PostDialog(_activeOpponent.getSpecialTaunt());
            else
                Taunt(result);

            OnOpponentSettled();
        } 
    }

    void ChooseOpponent()
    {
        _activeOpponent = _factory.ChooseOpponent();
        Portrait.sprite = _activeOpponent.GetPortrait();       
        Name.text = _activeOpponent.GetName();
    }

    private void SwapOpponents(System.Action OnSwapComplete)
    {
        ChooseOpponent();
        Speech.Clear();
        PlayEnterAnimation(() => {
            Speech.PostDialog(_activeOpponent.GetIntro(), () =>
            {
                OnSwapComplete();
            });
        });
        
    }

    private void Taunt(Result r)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            string taunt = _activeOpponent.GetTaunt(r);
            if (taunt != null && taunt.Length > 0)
            {
                Speech.PostDialog(taunt);
            }
        }
    }

    private void PlayEnterAnimation(System.Action OnAnimationComplete = null)
    {
        PortraitAnimator.SetTrigger("In");
        StartCoroutine(Common.WaitThenCallAction(Constants.WAIT_TIME, OnAnimationComplete));
    }

    private void PlayExitAnimation(System.Action OnAnimationComplete = null)
    {
        PortraitAnimator.SetTrigger("Out");
        StartCoroutine(Common.WaitThenCallAction(Constants.WAIT_TIME, OnAnimationComplete));
    }
}
