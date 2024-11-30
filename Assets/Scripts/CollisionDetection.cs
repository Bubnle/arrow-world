using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   // scene controller
    public FirstSceneController controller;
    // score recorder
    public ScoreRecorder scoreRecorder;

    void Start()
    {   
        // get the firstscene controller if it is null would not throw an exception
        controller = SSDirector.GetInstance().currentSceneController as FirstSceneController;
        // when we use this Singleton Pattern we can always get the same ScoreRecorder
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
    }
    // this fun will be called 
    // when gameobject which contain collider component knocks into other
    // object which also has collider component
    private void OnTriggerEnter(Collider arrow_head)
    {
        // it is parent of collider arrow_head
        // and it only can use transform to get parent

        Transform arrow = arrow_head.gameObject.transform.parent;
        if(arrow != null)
        {
            // set velocity 0 and can get rid of influence when they meet
            arrow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            arrow.tag = "2";
            // arrow_head can not be seen 
            arrow_head.gameObject.SetActive(false);

            // play hit audio
            AudioSource audio = arrow.GetComponent<AudioSource>();
            if (audio != null && audio.clip != null)
            {
                audio.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or Audio Clip is missing!");
            }
            if(this.gameObject.GetComponent<RingData>() == null)
            {
                Debug.Log("’“≤ªµΩª∑");
            }
            scoreRecorder.Record(this.gameObject);
            
        }
    }
}
