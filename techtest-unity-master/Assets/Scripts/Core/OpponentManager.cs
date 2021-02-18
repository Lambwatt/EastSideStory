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
            //Just swap opponents here
            Speech.text = _activeOpponent.GetOutro();
            StartCoroutine(SwapOpponents(OnOpponentSettled));
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

    IEnumerator SwapOpponents(System.Action OnSwapComplete)
    {
        yield return new WaitForSeconds(2);
        ChooseOpponent();
        Speech.text = _activeOpponent.GetIntro();
        OnSwapComplete();
    }

    void Taunt(Result r)
    {
        if (Random.Range(0, 2) == 0)
        {
            string taunt = _activeOpponent.GetTaunt(r);
            if (taunt != null && taunt.Length > 0)
            {
                Speech.text = taunt;
            }
        }
    }
}
