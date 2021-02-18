using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgementManager : MonoBehaviour
{
    public enum JudgementCategory
    {
        Negative,
        Zero,
        Less,
        Greater,
        Max
    }

    [System.Serializable]
    public struct MoneyLevel
    {
        public int money;
        public JudgementCategory category;
        public string text;
    }

    public List<MoneyLevel> levels;
    public Text JudgementText;
    public Animator JudgementAnimation;

    public void Reset()
    {
        JudgementAnimation.SetTrigger("Reset");
    }

    public void Judge(int money)
    {
        levels.Sort((MoneyLevel a, MoneyLevel b) =>
        {
            if (a.money > b.money)
                return 1;
            else if (a.money < b.money)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });


        for(int i = 0; i<levels.Count; i++)
        {
            if (levels[i].money > money)
            {
                Present(levels[i]);
                return;
            }
        }

        Present(levels[levels.Count-1]);
    }

    void Present(MoneyLevel level)
    {
        switch (level.category)
        {
            case JudgementCategory.Negative:
                JudgementText.text = level.text;
                JudgementAnimation.SetTrigger("Negative");        
                break;
            case JudgementCategory.Zero:
                JudgementText.text = level.text;
                JudgementAnimation.SetTrigger("Zero");
                break;
            case JudgementCategory.Less:
                JudgementText.text = "At least you've got "+level.text+" money.";
                JudgementAnimation.SetTrigger("Less");
                break;
            case JudgementCategory.Greater:
                JudgementText.text = "You've got that "+level.text+" money!";
                JudgementAnimation.SetTrigger("Greater");
                break;
            case JudgementCategory.Max:
                JudgementText.text = level.text;
                JudgementAnimation.SetTrigger("Max");
                break;
                
        }
        
    }
}
