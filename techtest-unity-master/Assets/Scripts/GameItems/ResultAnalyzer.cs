using UnityEngine;
using System.Collections;

public enum Result
{
	Win,
	Lose,
	Draw
}

public class ResultAnalyzer
{
    public static Result GetResultState(UseableItem playerHand, UseableItem enemyHand)
    {
        return (Result)((((playerHand + 3 - enemyHand) % 3) + 2) % 3);       
    }

    #region -- Saved for posterity --
    //public static void TestReplacementFunction()
    //{
    //    int discrepencies = 0;
    //    foreach(UseableItem p in System.Enum.GetValues(typeof(UseableItem))){
    //        foreach (UseableItem e in System.Enum.GetValues(typeof(UseableItem))){
    //            discrepencies += (GetResultState(p, e) == OldGetResultState(p, e)) ? 0 : 1;
    //        }
    //    }
    //    Debug.Log("Found " + discrepencies + " discrepencies.");
    //}
    #endregion
}