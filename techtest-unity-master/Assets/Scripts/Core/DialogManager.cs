using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] Text Dialog;

    public void Clear()
    {
        Dialog.text = "";
    }

    public void PostDialog(string line, System.Action OnDialogComplete = null)
    {
        Clear();
        StartCoroutine(printDialog(line, OnDialogComplete));
    }

    IEnumerator printDialog(string line, System.Action OnDialogComplete = null)
    {
        for (int i = 0; i < line.Length; i++) {
            Dialog.text += line[i];
            yield return null;
        }

        if(OnDialogComplete!=null)
            OnDialogComplete();
    }
}
