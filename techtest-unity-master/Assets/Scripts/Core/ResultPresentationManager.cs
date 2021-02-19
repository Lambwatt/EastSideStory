using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ResultPresentationManager : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] float _animTime;
    [SerializeField] Text _result;

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
        StartCoroutine(WaitForAnimation(OnAnimationComplete));
    }

    IEnumerator WaitForAnimation(System.Action OnAnimationComplete = null)
    {
        yield return new WaitForSeconds(_animTime);
        if (OnAnimationComplete != null)
        {
            OnAnimationComplete();
        }
    }
}
