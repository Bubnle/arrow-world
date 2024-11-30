using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object
{
    // only one instance in whole grogram
    private static SSDirector instance;
    // realize the ISceneController can be gotten and set through director!!!
    public ISceneController currentSceneController { get; set; }

    public static SSDirector GetInstance()
    {
        if(instance == null)
        {
            instance = new SSDirector();
        }
        return instance;
    }
}
