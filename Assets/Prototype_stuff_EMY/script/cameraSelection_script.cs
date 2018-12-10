using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSelection_script : MonoBehaviour {

    public GameObject camera_selection_camera;
    public GameObject CameraV1;
    public GameObject CameraV2;
    public GameObject CameraV3;
    public GameObject buttonV1;
    public GameObject buttonV2;
    public GameObject buttonV3;

    void Start () {
        camera_selection_camera.SetActive(true);
        CameraV1.SetActive(false);
        CameraV2.SetActive(false);
        CameraV3.SetActive(false);
    }
    


    public void pressButtonV1()
    {
        camera_selection_camera.SetActive(false);
        CameraV1.SetActive(true);
        //CameraV2.SetActive(false);
        //CameraV3.SetActive(false);
        //turn off buttons
        buttonV1.SetActive(false);
        buttonV2.SetActive(false);
        buttonV3.SetActive(false);
    }

    public void pressButtonV2()
    {
        camera_selection_camera.SetActive(false);
        //CameraV1.SetActive(false);
        CameraV2.SetActive(true);
        //CameraV3.SetActive(false);
        //turn off buttons
        buttonV1.SetActive(false);
        buttonV2.SetActive(false);
        buttonV3.SetActive(false);
    }

    public void pressButtonV3()
    {
        camera_selection_camera.SetActive(false);
        //CameraV1.SetActive(false);
        //CameraV2.SetActive(false);
        CameraV3.SetActive(true);
        //turn off buttons
        buttonV1.SetActive(false);
        buttonV2.SetActive(false);
        buttonV3.SetActive(false);
    }

}
