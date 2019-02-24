using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(cam1.activeSelf == false)
            {
                Debug.Log("shift");
                cam1.SetActive(true);
                cam2.SetActive(false);
                
            }

            else
            //if (cam1.activeSelf == true)
            {
                Debug.Log("shift2");
                cam2.SetActive(true);
                cam1.SetActive(false);
                
               
            }
        }
    }

}
