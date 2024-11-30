using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SSActionEventType: int
{
    Started = 1 ,
    Competed = 2 
}
public interface ISSActionCallback
{
    void SSActionEvent(SSAction source,
        SSActionEventType events = SSActionEventType.Competed,
        int intParam = 0,
        string strParam = null,
        Object objectParam = null);
}
