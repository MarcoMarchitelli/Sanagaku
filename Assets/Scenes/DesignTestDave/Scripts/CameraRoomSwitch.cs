using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(cam1.activeSelf == true)
            {
                Debug.Log("shift");
                cam1.SetActive(false);
                cam2.SetActive(true);
                
            }

            if (cam2.activeSelf == true)
            {
                Debug.Log("shift2");
                cam2.SetActive(false);
                cam1.SetActive(true);
                
               
            }
        }
    }

}
