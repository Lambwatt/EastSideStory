using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrategy 
{
    UseableItem draw();
    bool isDoneAfterResult(Result r);
}
