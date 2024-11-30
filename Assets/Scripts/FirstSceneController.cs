using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{   
    // Action part :
    // arrow action manager
    public ArrowActionManager arrowActionManager;
    // tracking arrow action manager
    public TrackingArrowActionManager trackingArrowManager;
    // tracking arrow need it
    public Vector3 destination;
    public Vector3 placeOfDepatrure;

    // controller of the scene view
    public Camera maincamera;
    public Camera sidecamera;

    // symbol of game state
    bool game_start = true;
    bool game_over = false;

    // all about score
    ScoreRecorder score_recorder;
    int round = 0;
    private int[] targetScore;

    // every round has shoot arrow num
    private int arrow_num = 0;

    //
    Factory factory;
    // 
    string gameMessage;

    // arrow num in the scene
    // normal arrow
    private List<GameObject> arrow_queue1 = new List<GameObject>();
    // tracking arrow
    private List<GameObject> arrow_queue2 = new List<GameObject>();
    // bow and arrow and target
    public GameObject bow;
    public GameObject arrow;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject trackingArrow;

    public GameObject player;

    public bool isAiming = false;

    // lock
    bool isChecking = false;

    public Vector3 cameraOffset;
    void Start()
    {
        // director
        SSDirector director = SSDirector.GetInstance();
        // action manager
        arrowActionManager = gameObject.AddComponent<ArrowActionManager>() as ArrowActionManager;
        trackingArrowManager = gameObject.AddComponent<TrackingArrowActionManager>() as TrackingArrowActionManager;
        // scorerecorder
        score_recorder = Singleton<ScoreRecorder>.Instance;
        //factory
        factory = Singleton<Factory>.Instance;
        // load resources
        LoadResources();
        this.game_start = true;
        this.game_over = false;
        // targetscore
        targetScore = new int[10];
        targetScore[0] = 15;
        for(int i = 1; i < 8; i++)
        {
            targetScore[i] = targetScore[i - 1] + 15+ 1 ;
        }
        score_recorder.target_score = targetScore[0];

        // camera 
        maincamera = Camera.main;
        sidecamera = GameObject.FindGameObjectWithTag("Side_Camera").GetComponent<Camera>();
        // firstscenecontroller
        director.currentSceneController = this;
        cameraOffset = Camera.main.transform.localPosition - new Vector3(50, 2, 52);
    }

    void Update()
    {
        
        if (game_start)
        {
            float offsetX = 0f;
            float offsetY = 0f;

            // when press w | s move in y
            // when press a | d move in x 
            if (Input.GetKey(KeyCode.W))
            {
                offsetY += 3f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                offsetY -= 3f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                offsetX += 3f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                offsetX -= 3f;
            }

            // Move !!!
            Move(offsetX, offsetY);

            AimMode();

            // 
            if (Input.GetKeyDown(KeyCode.I))
            {
                SwitchCamera();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                MakeSideCameraShow();
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
            
            if (!isChecking)
            {
                isChecking = true;
                Invoke("ResetArrowSpawnFlag", 1.8f);
                CheckGameStatus();
            }
            
        }
    }

    public void Move(float offsetX, float offsetY)
    {
        // controller out of range which we can move 

        if (bow.transform.position.x > 60)
        {
            bow.transform.position = new Vector3(60, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.x < 43)
        {
            bow.transform.position = new Vector3(43, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y < 1)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, 1, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y > 10)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, 10, bow.transform.position.z);
            return;
        }
        offsetX *= Time.deltaTime;
        offsetY *= Time.deltaTime;

        bow.transform.Translate(offsetX,offsetY,0);

        UpdateCameraPosition();
        UpdatePlayerPosition(offsetX);
    }

    private void UpdateCameraPosition()
    {
        
        if (maincamera != null && bow != null)
        {
            maincamera.transform.position = bow.transform.position + cameraOffset;
        }
    }
    private void UpdatePlayerPosition(float offsetX)
    {
        if(player.transform.position.x <40)
        {
            Debug.Log("wrong");
        }
        if (player != null && bow != null)
        {
            player.transform.position = bow.transform.position +new Vector3(0, -2, -1.78f) +new Vector3(offsetX, 0 ,0);
        }
    }
    public void Shoot()
    {
        if ((game_start) && arrow_num <= 9)
        {
            // The last three arrows are tracking arrows
            this.destination = GetDestination();
            if (arrow_num >= 7)
            {
                this.destination = GetDestination();
                Debug.Log("des = " + destination);
                trackingArrow = factory.GetArrow(ArrowType.TrackingArow);
                trackingArrow.tag = "1";
                // initialize pos 
                this.placeOfDepatrure = bow.transform.Find("arrow").position;
                trackingArrow.transform.position = bow.transform.Find("arrow").position; // + new Vector3(0,0.3f,0);
                // add it to the queue
                arrow_queue2.Add(trackingArrow);
                //for(int i = 0; i < arrow_queue2.Count; i++)
                //{
                //    arrow_queue1[i].tag = "1";
                //}
                // initialize trackingArrowManager
                // force is a z direction force
                Vector3 force = new Vector3(0, 0, 20);
                trackingArrowManager.ArrowFly(trackingArrow, force);
            }
            else
            {
                arrow = factory.GetArrow(ArrowType.Arrow);
                // initialize pos 
                arrow.transform.position = bow.transform.Find("arrow").position;
                arrow.tag = "1";
                // add it to the queue
                arrow_queue1.Add(arrow);
                // initialize trackingArrowManager
                // force is a z direction force
                Vector3 force = new Vector3(0, 0, 20);
                arrowActionManager.ArrowFly(arrow, force);
            }
            Debug.Log("instance" + arrow.gameObject.GetInstanceID());
            // when u shoot the arrow u can use is less 
            score_recorder.arrow_num--;
            // arrow in scene increase
            arrow_num++;

            ClearArrow();
        }
    }

    public int GetScore()
    {
        return score_recorder.GetScore();
    }
    public int GetTargetScore()
    {
        return score_recorder.target_score;
    }
    // get left arrow num
    public int GetResidumNum()
    {
        return score_recorder.arrow_num;
    }
    public void Restart()
    {
        // reset attribute
        game_start = true;
        game_over = false;
        score_recorder.arrow_num = 10;
        score_recorder.score = 0;
        score_recorder.target_score = 15;
        round = 0;
        arrow_num = 0;
        ClearArrow();
        arrow_queue1.Clear();
        arrow_queue2.Clear();
        Debug.Log($"game_start: {game_start}, game_over: {game_over}");

    }
    public void StartGame()
    {
        game_start = true;
        
    }
    // when u click right enter this mode
    public void AimMode()
    {
        // keep pressing mouse right button
        if (Input.GetMouseButton(1))
        {
            if (!isAiming)
            {
                // change the symbol of AimMode
                isAiming = true;
                maincamera.fieldOfView /= 1.5f;
                sidecamera.fieldOfView /= 1.5f;
            }
        }
        else
        {
            if(isAiming)
            {
                // out of the mode
                isAiming=false;
                maincamera.fieldOfView *= 1.5f;
                sidecamera.fieldOfView *= 1.5f;
            }
        }
    }
    public bool GameOver()
    {
        return game_over;
    }
    public void LoadResources()
    {
        // instantiate bow prefab
        bow = Instantiate(Resources.Load("Prefabs/Crossbow", typeof(GameObject))) as GameObject;
        // instantiate 3 target prefabs
        target1 = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject))) as GameObject;
        target2 = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject))) as GameObject;
        target3 = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject))) as GameObject;
        // set pos
        bow.gameObject.transform.position = new Vector3(50, 2, 52);
        target1.transform.position = new Vector3(45, 3, 65);
        target2.transform.position = new Vector3(50, 3, 65);
        target3.transform.position = new Vector3(55, 3, 65);

        // load player
        player = Instantiate(Resources.Load("Prefabs/Armature", typeof(GameObject))) as GameObject;
        player.transform.position = new Vector3(50, 0, 50.22f);
    }
    public void FreeArrow(ArrowType type)
    {
        // according to different state to use different freearrow para
        factory.FreeArrow(arrow, type);
    }
    public void SwitchCamera()
    {
        if (maincamera.enabled)
        {
            maincamera.enabled = false;
            sidecamera.enabled = true;
        }
        else
        {
            maincamera.enabled = true;
            sidecamera.enabled = false;
        }
    }
    public void SetPlaceOfDeparture()
    {
        this.placeOfDepatrure = bow.transform.position;
    }

    public Vector3 GetDestination()
    {
        if(bow.transform.position.x > 52.5)
        {
            return new Vector3(55, 3, 65);
        }
        else if(bow.transform.position.x < 47.5)
        {
            return new Vector3(45, 3, 65);
        }
        else
        {
            return new Vector3(50, 3, 65);
        }
        
    }

    public void CheckGameStatus()
    {
        if (score_recorder.arrow_num <= 0 && score_recorder.score >= score_recorder.target_score)
        {
            round++;
            arrow_num = 0;
            score_recorder.target_score = targetScore[round];
            score_recorder.arrow_num = 10;
            return;
        }
        if (score_recorder.arrow_num <=0 && score_recorder.score < score_recorder.target_score)
        {
            ClearAllArrowsByCloneName();
            game_start = false;
            game_over = true;
            
            return;
        }
        
    }
    public void SetMessage(string gameMessage)
    {
        this.gameMessage = gameMessage;
    }

    public void ClearArrow()
    {
        for (int i = 0; i < arrow_queue1.Count; i++)
        {
            factory.FreeArrow(arrow_queue1[i], ArrowType.Arrow);
            Destroy(arrow_queue1[i], 2f);

        }
        for (int i = 0; i < arrow_queue2.Count; i++)
        {
            factory.FreeArrow(arrow_queue2[i], ArrowType.TrackingArow);
            Destroy(arrow_queue2[i], 2f);

        }
        arrow_queue1.Clear();
        arrow_queue2.Clear();
    }


    void ClearAllArrowsByCloneName()
    {
        // 获取场景中所有对象
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Arrow(Clone)" || obj.name == "TrackingArrow(Clone)")
            {
                Destroy(obj, 2f);
            }
        }
        arrow_queue1.Clear();
        arrow_queue2.Clear();
    }

    void ResetArrowSpawnFlag()
    {
        isChecking = false;
    }

    public void MakeSideCameraShow()
    {
        if (sidecamera.enabled == false)
        {
            sidecamera.enabled = true;
        }
        else
        {
            sidecamera.enabled = false;
        }

        if(sidecamera.rect == new Rect(0, 0, 1, 1))
        {
            sidecamera.rect = new Rect(0.7f, 0f, 0.3f, 0.3f);
        }
        else
        {
            sidecamera.rect = new Rect(0, 0, 1, 1);
        }
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        GUIStyle bigStyle = new GUIStyle();
        bigStyle.normal.textColor = Color.white;
        bigStyle.fontSize = 50;

        GUIStyle over_style = new GUIStyle();
        over_style.normal.textColor = new Color(1, 1, 1);
        over_style.fontSize = 25;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float rightQuarterX = screenWidth * 0.75f;
        float elementWidth = 200f;
        float elementHeight = 50f;

        float labelWidth = 400; // 标签宽度
        float labelHeight = 50; // 标签高度
        float labelX = (Screen.width - labelWidth) / 2; // 水平居中
        float labelY = 10; // 距离屏幕顶部10像素

        // 显示标签
        GUI.Label(new Rect(labelX, labelY, labelWidth, labelHeight), "WASD 控制   左键射击   右键瞄准   I 键切换视角", bigStyle);
        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4 - 100, elementWidth, elementHeight), "Arrow - World", bigStyle);


        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4, elementWidth, elementHeight), "Points: " + score_recorder.score, style);

        GUI.Label(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 4 + 50, elementWidth, elementHeight), gameMessage, style);

        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 - 100, elementWidth, 40), "Start game"))
        {
            StartGame();
        }

        if (GUI.Button(new Rect(rightQuarterX - elementWidth / 2, screenHeight / 2 - 50, elementWidth, 40), "Restart"))
        {
            Restart();
           
        }
        if (!game_start)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "game over", over_style);
        }
        
    }
}
