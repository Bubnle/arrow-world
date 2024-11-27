using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingArrowAction : SSAction
{
    // the place of departure
    public Vector3 placeOfDeparture;

    // to achieve the trackingarrow we must have the destination of the arrow
    public Vector3 destination;

    // the force is the initial force to make sure the arrow go to target
    public Vector3 force;

    // the force can guide arrow to the destination
    public float guidanceForce;

    // caluate need mass
    float mass;

    // to get the arrowaction which i want
    public static TrackingArrowAction GetTrackingArrowAction(Vector3 placeOfDeparture, Vector3 destination, Vector3 force, GameObject trackingArrow)
    {
        TrackingArrowAction action = ScriptableObject.CreateInstance<TrackingArrowAction>();
        // initialize
        action.placeOfDeparture = placeOfDeparture;
        action.destination = destination;
        action.force = new Vector3(0, 0, 20);
        action.guidanceForce = 50;
        action.MgameObject = trackingArrow;
        Rigidbody rb = action.MgameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            action.mass = rb.mass;
        }
        else
        {
            Debug.LogError("wrong gameobject not be initial");
        }
        return action;
    }
    public override void Start()
    {
        float velocityz = force.magnitude;
        // make it parents is null to sperate from factory
        this.MgameObject.transform.parent = null;

        this.MgameObject.GetComponent<Rigidbody>().useGravity = false;
        

        Vector3 direction = destination - this.MgameObject.transform .position;

        float distancex = destination.x - this.MgameObject.transform.position.x;
        float distancey = destination.y - this.MgameObject.transform.position.y;
        float distancez = destination.z - this.MgameObject.transform.position.z;

        //Debug.Log("x:" + distancex + "   y:" + distancey + "   z" + distancez);
        float time = distancez / velocityz;

        float velocityx = distancex / time;
        float velocityy = distancey / time;
        
        Vector3 ultimatevelocity = new Vector3(velocityx, velocityy, velocityz);
        //Debug.Log("vel = " + ultimatevelocity);
        this.MgameObject.GetComponent<Rigidbody>() .velocity = ultimatevelocity;
    }
    //  u can upgrade the trackingArrow algrithm
    public override void Update()    { }
    public override void FixedUpdate()
    {
        if (MgameObject == null)
        {
            Debug.Log("is null mgameobject");
            return;
        }

        Rigidbody rb = MgameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("is null rb");
            return;
        }

                
    }




    //public override void FixedUpdate()
    //{
    //    if (MgameObject == null)
    //    {
    //        Debug.Log("is null mgameobject");
    //        return;
    //    }

    //    Rigidbody rb = MgameObject.GetComponent<Rigidbody>();
    //    if (rb == null)
    //    {
    //        Debug.Log("is null rb");
    //        return;
    //    }
    //    // 获取目标方向
    //    Vector3 directionToTarget = (destination - transform.position).normalized;

    //    // 当前速度
    //    Vector3 currentVelocity = rb.velocity;

    //    // 减小 Z 方向速度比例
    //    currentVelocity.z *= 0.1f;  // 调整比例为 50%，可根据需要修改
    //    rb.velocity = currentVelocity;

    //    // 增加目标方向上的新速度
    //    float fixedSpeed = 10f;  // 固定的基础速度
    //    rb.velocity += directionToTarget * fixedSpeed;

    //    //currentVelocity = rb.velocity;
    //    //currentVelocity.z *= 0.1f;
    //    //currentVelocity.x *= 3;

    //    rb.velocity = currentVelocity;

    //    if ((destination - transform.position).magnitude < 10f)
    //    {
    //        transform.position = destination;
    //    }
    //    // 让物体面朝目标
    //    transform.LookAt(destination);

    //    Debug.Log("Adjusted Velocity: " + rb.velocity);
    //}


}
