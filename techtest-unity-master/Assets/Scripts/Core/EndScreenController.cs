using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] Text _moneyText;
    [SerializeField] Text _recordText;

    [SerializeField] Button _replayButton;
    [SerializeField] Button _quitButton;

    [SerializeField] JudgementManager _judgement;

    public delegate void OnReplayAction();
    public event OnReplayAction OnReplay;

    private int _money;    
    private LinkedListNode<GameUpdate> _iterator;

    void OnEnable()
    {
        _iterator = SessionData.Instance.Updates.First;
        _money = SessionData.Instance.InitialCoins;

        _judgement.Reset();

        _recordText.text = "";
        _moneyText.text = "$" + _money;

        _replayButton.interactable = false;
        _quitButton.interactable = false;

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
            _moneyText.text = "$" + _money;

            switch (currentUpdate.drawResult)
            {
                case Result.Win:
                    _recordText.text += "W";
                    break;
                case Result.Lose:
                    _recordText.text += "L";
                    break;
                case Result.Draw:
                    _recordText.text += "T";
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
        _judgement.Judge(_money);
        _replayButton.interactable = true;
        _quitButton.interactable = true;
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
