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
    public Text judgementText;
    public Animator judgementAnimation;

    public void Reset()
    {
        judgementAnimation.SetTrigger("Reset");
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
                judgementText.text = level.text;
                judgementAnimation.SetTrigger("Negative");        
                break;
            case JudgementCategory.Zero:
                judgementText.text = level.text;
                judgementAnimation.SetTrigger("Zero");
                break;
            case JudgementCategory.Less:
                judgementText.text = "At least you've got "+level.text+" money.";
                judgementAnimation.SetTrigger("Less");
                break;
            case JudgementCategory.Greater:
                judgementText.text = "You've got that "+level.text+" money!";
                judgementAnimation.SetTrigger("Greater");
                break;
            case JudgementCategory.Max:
                judgementText.text = level.text;
                judgementAnimation.SetTrigger("Max");
                break;
                
        }
        
    }
}
