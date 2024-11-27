using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAction : SSAction
{   
    // because ArrowAction inherit SSAction
    // it has the member which inherit from SSAction

    // it is the arrow's initial speed 
    public Vector3 force ;
    public override void Update() { }
    public static ArrowAction GetArrowAction(Vector3 force)
    {
        ArrowAction action = ScriptableObject.CreateInstance<ArrowAction>();
        // to create an arrowaction which u need aiming to direction that u want
        action.force = new Vector3(0, 0, 20);
        return action;
    }
    public override void Start()
    {   
        // get out of the control of factory
        this.MgameObject.transform.parent = null;
        // init
        force = new Vector3 (0, 0, 20);
        this.MgameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.MgameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        Debug.Log("Start successfully");
    }

    public override void FixedUpdate()
    {
        // if it is out of range
        if (this.transform.position.z >150 || this.transform.position.y<0 
            || this.transform.position.x<-30 || this.transform.position.x>30 )
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
}
