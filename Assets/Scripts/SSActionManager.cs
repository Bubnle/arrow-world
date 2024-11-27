using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    //use hash map to score SSAction
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    //the list waiting to be added
    private List<SSAction> waitingAdd = new List<SSAction>();
    //the list waiting to be deleted
    private List<int> waitingDelete = new List<int>();

    // these method call every frame or every 2 frames
    protected void Update()
    {
        //将waitingAdd中的动作保存
        foreach (SSAction ac in waitingAdd)
        {
            // use action's id as action's key
            // action it self is value
            actions[ac.GetInstanceID()] = ac;
        }
        // when we add successfully clear the list
        waitingAdd.Clear();

        //run the actions
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            // if ac shoule be destory add it into delete List
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.Update();
            }
        }

        // delete these actions
        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        waitingDelete.Clear();
    }
    protected void FixedUpdate()
    {
        //将waitingAdd中的动作保存
        foreach (SSAction ac in waitingAdd)
        {
            // use action's id as action's key
            // action it self is value
            actions[ac.GetInstanceID()] = ac;
        }
        // when we add successfully clear the list
        waitingAdd.Clear();

        //run the actions
        foreach (KeyValuePair<int, SSAction> kv in actions)
        {
            SSAction ac = kv.Value;
            // if ac shoule be destory add it into delete List
            if (ac.destroy)
            {
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if (ac.enable)
            {
                ac.FixedUpdate();
            }
        }

        // delete these actions
        foreach (int key in waitingDelete)
        {
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        waitingDelete.Clear();
    }

    // initialize action and add it into waitingAdd List
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback manager)
    {   
        // set gameObject which is SSAction's member
        action.MgameObject = gameObject;

        // set transform which is SSAction's member
        // meanwhile it is corresponding member's transform 
        action.transform = gameObject.transform;

        // set SSAction member which realize interface
        // named ISSActionCallback
        // because to unit iterface only two modes ActionManager realize it 
        action.callback = manager;
        waitingAdd.Add(action);

        // call the corresponding start fun to make initialize gameobject
        action.Start();
        action.Update();
        action.FixedUpdate();
    }

}
