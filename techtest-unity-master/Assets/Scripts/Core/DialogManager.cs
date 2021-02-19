using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialog;

    public void Clear()
    {
        dialog.text = "";
    }

    public void PostDialog(string line, System.Action OnDialogComplete = null)
    {
        Clear();
        StartCoroutine(PrintDialog(line, OnDialogComplete));
    }

    IEnumerator PrintDialog(string line, System.Action OnDialogComplete = null)
    {
        for (int i = 0; i < line.Length; i++) {
            dialog.text += line[i];
            yield return null;
        }

        if(OnDialogComplete!=null)
            OnDialogComplete();
    }
}