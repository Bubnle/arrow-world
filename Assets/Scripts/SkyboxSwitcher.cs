using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    public Material[] skyboxes;  // ���ڴ�Ų�ͬ����պв���
    private int currentSkyboxIndex = 0;
    public Camera camera;
    void Start()
    {   
        camera = Camera.main;
        
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
    }

    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space))  
        {
            currentSkyboxIndex++;
            if (currentSkyboxIndex >= skyboxes.Length)
            {
                currentSkyboxIndex = 0;  
            }

            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    void SetSkyboxToAllCameras()
    {
        camera.clearFlags = CameraClearFlags.Skybox;  
        camera.backgroundColor = Color.black; 
        
        RenderSettings.skybox = skyboxes[currentSkyboxIndex]; 
        DynamicGI.UpdateEnvironment();  
    }
}
