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
    //    // ��ȡĿ�귽��
    //    Vector3 directionToTarget = (destination - transform.position).normalized;

    //    // ��ǰ�ٶ�
    //    Vector3 currentVelocity = rb.velocity;

    //    // ��С Z �����ٶȱ���
    //    currentVelocity.z *= 0.1f;  // ��������Ϊ 50%���ɸ�����Ҫ�޸�
    //    rb.velocity = currentVelocity;

    //    // ����Ŀ�귽���ϵ����ٶ�
    //    float fixedSpeed = 10f;  // �̶��Ļ����ٶ�
    //    rb.velocity += directionToTarget * fixedSpeed;

    //    //currentVelocity = rb.velocity;
    //    //currentVelocity.z *= 0.1f;
    //    //currentVelocity.x *= 3;

    //    rb.velocity = currentVelocity;

    //    if ((destination - transform.position).magnitude < 10f)
    //    {
    //        transform.position = destination;
    //    }
    //    // �������泯Ŀ��
    //    transform.LookAt(destination);

    //    Debug.Log("Adjusted Velocity: " + rb.velocity);
    //}


}
