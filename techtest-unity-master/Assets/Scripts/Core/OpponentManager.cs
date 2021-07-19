using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentManager : MonoBehaviour
{
    [SerializeField] Image _portrait;
    [SerializeField] DialogManager _speech;
    [SerializeField] Text _name;

    [SerializeField] List<Persona> _personas;
    [SerializeField] Animator _portraitAnimator;

    Opponent _activeOpponent;
    OpponentFactory _factory; 

    public void intialize()
    {
        _factory = new OpponentFactory(_personas); 
    }

    public void ChooseFirstOpponent(System.Action OnOpponentChosen = null)
    {
        _portraitAnimator.SetTrigger("Reset");
        ChooseOpponent();
        _speech.Clear();
        PlayEnterAnimation(() =>
        {
            _speech.PostDialog(_activeOpponent.Intro, OnOpponentChosen);
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
            _speech.PostDialog( _activeOpponent.Outro, ()=>{
                PlayExitAnimation(() => { SwapOpponents(OnOpponentSettled); });
            });
        }
        else
        {
            if (_activeOpponent.hasSpecialTaunt())
                _speech.PostDialog(_activeOpponent.getSpecialTaunt());
            else
                Taunt(result);

            OnOpponentSettled();
        } 
    }

    void ChooseOpponent()
    {
        _activeOpponent = _factory.ChooseOpponent();
        _portrait.sprite = _activeOpponent.Portrait;       
        _name.text = _activeOpponent.Name;
    }

    private void SwapOpponents(System.Action OnSwapComplete)
    {
        ChooseOpponent();
        _speech.Clear();
        PlayEnterAnimation(() => {
            _speech.PostDialog(_activeOpponent.Intro, () =>
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
                _speech.PostDialog(taunt);
            }
        }
    }

    private void PlayEnterAnimation(System.Action OnAnimationComplete = null)
    {
        _portraitAnimator.SetTrigger("In");
        StartCoroutine(Common.WaitThenCallAction(Constants.WAIT_TIME, OnAnimationComplete));
    }

    private void PlayExitAnimation(System.Action OnAnimationComplete = null)
    {
        _portraitAnimator.SetTrigger("Out");
        StartCoroutine(Common.WaitThenCallAction(Constants.WAIT_TIME, OnAnimationComplete));
    }
}
