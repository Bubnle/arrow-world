using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArrowActionManager : SSActionManager, ISSActionCallback
{
    private TrackingArrowAction trackingArrowAction;
    public FirstSceneController controller;
    void Start()
    {
        controller = (FirstSceneController)SSDirector.GetInstance().currentSceneController;
        // initialize the arrow action manager 
        controller.trackingArrowManager = this;
    }

    public void ArrowFly(GameObject trackingArrow, Vector3 force)
    {
        // initialize the trackingArrowAction
        trackingArrowAction = TrackingArrowAction.GetTrackingArrowAction(controller.placeOfDepatrure, controller.destination, force, trackingArrow);
        // make it fly 
        this.RunAction(trackingArrow, trackingArrowAction, this);
    }


    public void SSActionEvent(SSAction source,
       SSActionEventType events = SSActionEventType.Competed,
       int intParam = 0,
       string strParam = null,
       Object objectParam = null)
    {
        controller.FreeArrow(ArrowType.TrackingArow);
    }
}
