using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common 
{
    public static IEnumerator WaitThenCallAction(float time, System.Action OnPauseComplete)
    {
        yield return new WaitForSeconds(time);
        if (OnPauseComplete != null)
        {
            OnPauseComplete();
        }
    }
}