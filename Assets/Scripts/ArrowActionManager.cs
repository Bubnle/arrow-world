using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowActionManager : SSActionManager, ISSActionCallback
{
    private ArrowAction arrowAction;
    public FirstSceneController controller;
    void Start()
    {
        controller = (FirstSceneController)SSDirector.GetInstance().currentSceneController;
        // initialize the arrow action manager 
        controller.arrowActionManager = this;  
    }

    public void ArrowFly(GameObject arrow, Vector3 force)
    {
        // initialize the arrowAction
        arrowAction = ArrowAction.GetArrowAction(force);
        // make it fly 
        this.RunAction(arrow, arrowAction, this);
    }


    public void SSActionEvent(SSAction source,
       SSActionEventType events = SSActionEventType.Competed,
       int intParam = 0,
       string strParam = null,
       Object objectParam = null)
    {
        controller.FreeArrow(ArrowType.Arrow);
    }

}
