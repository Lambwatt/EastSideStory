using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrategy 
{
    UseableItem Draw();
    bool IsDoneAfterResult(Result r);
    bool HasSpecialTaunt();
    string GetSpecialTaunt();
}