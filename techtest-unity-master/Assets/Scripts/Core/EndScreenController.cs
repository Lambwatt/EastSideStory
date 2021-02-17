using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    public Text MoneyText;
    public Text RecordText;

    public Button ReplayButton;
    public Button QuitButton;

    int _money;
    
    LinkedListNode<GameUpdate> _iterator;

    public delegate void OnReplayAction();
    public event OnReplayAction OnReplay;

    void OnEnable()
    {
        _iterator = SessionData.Instance.Updates.First;
        _money = SessionData.Instance.InitialCoins;
        RecordText.text = "";
        MoneyText.text = "$" + _money;

        ReplayButton.interactable = false;
        QuitButton.interactable = false;

        StartCoroutine(ShowResults());
    }

    IEnumerator ShowResults()
    {
        bool done = _iterator == null;
        while (!done)
        {
            yield return new WaitForSeconds(0.1f);
            GameUpdate currentUpdate = _iterator.Value;
            _money += currentUpdate.coinsAmountChange;
            MoneyText.text = "$" + _money;

            switch (currentUpdate.drawResult)
            {
                case Result.Won:
                    RecordText.text += "W";
                    break;
                case Result.Lost:
                    RecordText.text += "L";
                    break;
                case Result.Draw:
                    RecordText.text += "T";
                    break;
            }
            
            if (_iterator.Next != null)
            {
                _iterator = _iterator.Next;
            }
            else
            {
                done = true;
            }
        }

        //Handle fancy stuff at the end
        ReplayButton.interactable = true;
        QuitButton.interactable = true;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Replay()
    {
        if (OnReplay != null)
        {
            OnReplay();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ERROR: Restart event not set.");
        }

    }

}
