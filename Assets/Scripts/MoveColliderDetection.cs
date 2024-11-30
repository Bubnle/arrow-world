using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveColliderDetection : MonoBehaviour
{
    public ParticleSystem collisionParticle;
    // Start is called before the first frame update
    // scene controller
    public FirstSceneController controller;
    // score recorder
    public ScoreRecorder scoreRecorder;

    private void OnTriggerEnter(Collider arrow_head)
    {
        // it is parent of collider arrow_head
        // and it only can use transform to get parent

        Transform arrow = arrow_head.gameObject.transform.parent;
        if (arrow != null)
        {
            // set velocity 0 and can get rid of influence when they meet
            arrow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            arrow.GetComponent<Rigidbody>().useGravity = true;
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

            // particle ???
            if(collisionParticle == null)
            {
                Debug.LogError("ww");
            }
            collisionParticle.transform.position = arrow_head.transform.position;
            collisionParticle.Play();

            // record
            scoreRecorder.Record(10);

        }
    }
}
