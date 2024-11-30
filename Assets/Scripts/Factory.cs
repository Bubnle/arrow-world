using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArrowType
{
    Arrow,
    TrackingArow
}


public class Factory : MonoBehaviour
{
    // the prefabs
    public GameObject arrow;
    public GameObject trackingArrow;

    // use hash map to store different type of arrow
    private Dictionary<ArrowType, GameObject> arrow_prefabs = new Dictionary<ArrowType, GameObject>();

    // use hash map to stor different type of used arrow pools
    private Dictionary<ArrowType, List<GameObject>> used_arrow_Pools = new Dictionary<ArrowType, List<GameObject>>();

    // use hash map to store different type of free arrow pools
    private Dictionary<ArrowType, Queue<GameObject>> free_arrow_Pools = new Dictionary<ArrowType, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        // init prefabs
        arrow_prefabs[ArrowType.Arrow] = arrow;
        arrow_prefabs[ArrowType.TrackingArow] = trackingArrow;

        // init used pools
        used_arrow_Pools[ArrowType.Arrow] = new List<GameObject>();
        used_arrow_Pools[ArrowType.TrackingArow] = new List<GameObject>();

        // init free pools 
        free_arrow_Pools[ArrowType.Arrow] = new Queue<GameObject>();
        free_arrow_Pools[ArrowType.TrackingArow] = new Queue<GameObject>();

    }

    //// instantiate Arrow prefab
    //arrow = Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;
    //// instantiate trackingArrow prefab
    //trackingArrow = Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;

    // to get free or create new usable arrow
    public GameObject GetArrow(ArrowType type)
    {
        GameObject tarrow;
        // if there is no free arrow in corresponding type 
        if (free_arrow_Pools[type].Count == 0)
        {
            // instantiate prefab into real one
            if(type == ArrowType.Arrow)
            {
                tarrow = Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;
            }
            else
            {
                tarrow = Instantiate(Resources.Load("Prefabs/TrackingArrow", typeof(GameObject))) as GameObject;
            }
            
        }
        // else use the arrow in correponding type
        else
        {
            // use free pool arrow 
            tarrow = free_arrow_Pools[type].Dequeue();

            // put it into use pools
            used_arrow_Pools[type].Add(tarrow);

            // set it state is active
            tarrow.SetActive(true);
        }

        // Debug message 
        //Debug.Log("Create Arrow successfuly!");
        return tarrow;
    }

    //
    public void FreeArrow(GameObject arrow, ArrowType type)
    {
        // debug
        // find it in corresponding list 
        foreach (GameObject cur_arrow in used_arrow_Pools[type])
        {   
            // campare id with cur_arrow
            if(cur_arrow.GetInstanceID()== arrow.GetInstanceID())
            {
                Debug.Log("Free success");
                // set it state 
                arrow.SetActive(false);

                // remove from used list
                used_arrow_Pools[type].Remove(cur_arrow);

                // add to free queue
                free_arrow_Pools[type].Enqueue(cur_arrow);

                break;
            }
        }
    }
}
