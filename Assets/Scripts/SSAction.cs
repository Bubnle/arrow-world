using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
    // members

    public bool enable;

    public bool destroy;

    public GameObject MgameObject { get; set;}

    public Transform transform { get; set; }

    public ISSActionCallback callback { get; set; }
    
    // To protected SSAction to make sure other people can not override SSAction
    protected SSAction() { }
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
    public virtual void FixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
