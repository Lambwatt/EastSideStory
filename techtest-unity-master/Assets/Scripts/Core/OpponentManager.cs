using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentManager : MonoBehaviour
{
    [SerializeField] Image Portrait;
    [SerializeField] Text Speech;

    [SerializeField] List<Personality> Personas;

    Opponent _activeOpponent;
    OpponentFactory _factory;

    public void intialize()
    {
        _factory = new OpponentFactory(Personas);
        ChooseOpponent();
    }

    void ChooseOpponent()
    {
        _activeOpponent = _factory.ChooseOpponent();
        Portrait.sprite = _activeOpponent.GetPortrait();
        Speech.text = _activeOpponent.GetIntro();
    }

    public UseableItem GetHand()
    {
        return _activeOpponent.Draw();
    }

    public void HandleResult(Result result, System.Action OnOpponentSettled)
    {
        if (_activeOpponent.isDoneAfterResult(result))
        {
            //Just swap opponents here
            Speech.text = _activeOpponent.GetOutro();
            StartCoroutine(SwapOpponents(OnOpponentSettled));
        }
        else
        {
            OnOpponentSettled();
        } 
    }

    IEnumerator SwapOpponents(System.Action OnSwapComplete)
    {
        yield return new WaitForSeconds(2);
        ChooseOpponent();
        Speech.text = _activeOpponent.GetIntro();
        OnSwapComplete();
    }
}
