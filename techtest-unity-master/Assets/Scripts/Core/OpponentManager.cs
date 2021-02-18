using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentManager : MonoBehaviour
{
    [SerializeField] Image Portrait;
    [SerializeField] Text Speech;
    [SerializeField] Text Name;

    [SerializeField] List<Personality> Personas;
    [SerializeField] Animator PortraitAnimator;

    Opponent _activeOpponent;
    OpponentFactory _factory; 

    public void intialize()
    {
        _factory = new OpponentFactory(Personas);
        ChooseOpponent();
        Speech.text = "";
        PlayEnterAnimation(() =>
        {
            Speech.text = _activeOpponent.GetIntro();
        });
    }

    void ChooseOpponent()
    {
        _activeOpponent = _factory.ChooseOpponent();
        Portrait.sprite = _activeOpponent.GetPortrait();       
        Name.text = _activeOpponent.GetName();
    }

    public UseableItem GetHand()
    {
        return _activeOpponent.Draw();
    }

    public void HandleResult(Result result, System.Action OnOpponentSettled)
    {
        if (_activeOpponent.isDoneAfterResult(result))
        {
            Speech.text = _activeOpponent.GetOutro();
            PlayExitAnimation(()=> { SwapOpponents(OnOpponentSettled); });
        }
        else
        {
            if (_activeOpponent.hasSpecialTaunt())
                Speech.text = _activeOpponent.getSpecialTaunt();
            else
                Taunt(result);

            OnOpponentSettled();
        } 
    }

    void SwapOpponents(System.Action OnSwapComplete)
    {
        //yield return new WaitForSeconds(2);
        ChooseOpponent();
        Speech.text = "";
        PlayEnterAnimation(() => {
            Speech.text = _activeOpponent.GetIntro();
            OnSwapComplete();
        });
        
    }

    void Taunt(Result r)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            string taunt = _activeOpponent.GetTaunt(r);
            if (taunt != null && taunt.Length > 0)
            {
                Speech.text = taunt;
            }
        }
    }

    void PlayEnterAnimation(System.Action OnAnimationComplete = null)
    {
        PortraitAnimator.SetTrigger("In");
        StartCoroutine(WaitForAnimation(1.2f, OnAnimationComplete));
    }

    void PlayExitAnimation(System.Action OnAnimationComplete = null)
    {
        PortraitAnimator.SetTrigger("Out");
        StartCoroutine(WaitForAnimation(1.2f, OnAnimationComplete));
    }

    IEnumerator WaitForAnimation(float animationTime, System.Action OnAnimationComplete = null)
    {
        yield return new WaitForSeconds(animationTime);
        if(OnAnimationComplete!=null)
            OnAnimationComplete();
    }

}
